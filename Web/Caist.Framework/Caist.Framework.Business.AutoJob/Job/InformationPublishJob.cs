using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using FluentFTP;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Caist.Framework.Business.AutoJob
{
    public class InformationPublishJob : IJobTask
    {
        private ViewManipulateModelBLL viewManipulateModelBLL = new ViewManipulateModelBLL();
        private InformationPublishBLL InformationPublishBLL = new InformationPublishBLL();
        private readonly FtpClient ftpClient = new FtpClient(GlobalContext.SystemConfig.FTPServer,
            new NetworkCredential(GlobalContext.SystemConfig.FTPUser, GlobalContext.SystemConfig.FTPPwd));
        private readonly string TemplatePath = GlobalContext.SystemConfig.InformationPublishTemplatePath;
        private readonly string LocalPath = GlobalContext.SystemConfig.InformationPublishPath;
        /// <summary>
        /// 信息发布任务
        /// </summary>
        /// <returns></returns>
        public async Task<TData> Start()
        {
            TData obj = new TData();
            //获取模板中大屏配置信息
            //获取屏幕UID
            //根据模板匹配需要查询出的 皮带、水泵、瓦斯（人员）相关数据
            //获取到数据后将数据填充到模板中需要替换的数据
            //将组装的大屏数据格式json输出到 信息发布路径提供给矿山LED电子屏采集数据显示
            string TemplateContent = "";
            using (StreamReader sr = new StreamReader(TemplatePath, Encoding.UTF8))
            {
                TemplateContent = sr.ReadToEnd();
            }

            List<PublishContent> TempList = new List<PublishContent>();
            if (TemplateContent != "")
            {
                TempList = JsonHelper.ToObject<List<PublishContent>>(TemplateContent);
            }
            else
            {
                obj.Tag = 0;
                obj.Message = "获取信息发布模板内容为空";
                return obj;
            }
            //记录本次任务已发布信息
            List<PublishContent> PublishList = new List<PublishContent>();
            //提取出模板中需要匹配的PLC数据块
            //主斜井皮带.电压(V)
            //主斜井皮带.电流(A)
            //根据系统模块.模块名称 查询到数据后替换模板中的内容数据值
            TempList.ForEach((n) =>
            {
                MatchCollection Matches = Regex.Matches(n.linkContent, @"(?<=\{\{)[^}]*(?=\}\})", RegexOptions.Multiline);
                ViewManipulateModelListParam param = new ViewManipulateModelListParam();
                foreach (Match NextMatch in Matches)
                {
                    if (!NextMatch.ToString().Contains("井下实时信息"))
                    {
                        //key:主斜井皮带   value: 电流(A)
                        string[] dict = NextMatch.ToString().Split('.');
                        //判断是为重复的视图项
                        if (!string.IsNullOrEmpty(param.ViewName) && param.ViewName != dict[0])
                        {
                            param.ViewName += "," + dict[0];
                        }
                        else
                        {
                            param.ViewName = dict[0];
                        }
                        //判断是否为重复的数据项
                        if (!string.IsNullOrEmpty(param.ManipulateModelName) && param.ManipulateModelName != dict[1])
                        {
                            param.ManipulateModelName += "," + dict[1];
                        }
                        else
                        {
                            param.ManipulateModelName = dict[1];
                        }
                    }
                }
                if (!string.IsNullOrEmpty(param.ViewName))
                {
                    //根据模板中配置的PLC节点查询出节点最新数据
                    var List = viewManipulateModelBLL.GetPublishList(param);
                    TData<List<ViewManipulateModelEntity>> dataList = List.Result;
                    if (dataList.Result != null && dataList.Result.Count > 0)
                    {
                        foreach (var data in dataList.Result)
                        {
                            //将模板中匹配得如： {{主斜井皮带.电压(V)}} 替换为最新得数值  ，其它得不做修改
                            n.linkContent = n.linkContent.Replace("{{" + data.ViewName + "." + data.ManipulateModelName + "}}", data.ViewValue.HasValue() ? data.ViewValue : "0");
                        }
                        PublishList.Add(n);
                    }
                }

            });
            //获取当前输出文件名称
            string FileName = "LinkContent_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

            if (WriteDirAndUplpadFtp(LocalPath, FileName, JsonConvert.SerializeObject(TempList)))
            {

                await InsertPublish(PublishList);
                obj.Tag = 1;
                obj.Message = "信息发布成功：" + TemplateContent;
            }
            else
            {
                obj.Tag = 0;
                obj.Message = "信息发布失败：" + TemplateContent;
            }

            return obj;
        }


        //输出信息发布数据txt文件到指定得ftp服务器
        private bool WriteDirAndUplpadFtp(string path, string filename, string content)
        {
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            if (System.IO.File.Exists(Path.GetFullPath(path + filename)))
            {
                File.Delete(Path.GetFullPath(path + filename));
            }
            FileStream fs = new FileStream(path + filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(content);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
            //建立连接
            ftpClient.Connect();
            if (ftpClient.IsConnected == false)
            {
                return false;
            }
            var ftpStatus = ftpClient.UploadFile(path + filename, filename);
            //断开连接
            ftpClient.Disconnect();
            //释放资源
            ftpClient.Dispose();
            return ftpStatus == FtpStatus.Success;
        }
        /// <summary>
        /// 数据插入数据库
        /// </summary>
        /// <param name="PublishList"></param>
        /// <returns></returns>
        private async Task InsertPublish(List<PublishContent> PublishList)
        {
            foreach (var item in PublishList)
            {
                InformationPublishEntity entity = new InformationPublishEntity();
                entity.DeviceUid = item.deviceUID;
                entity.LinkContent = item.linkContent;
                await InformationPublishBLL.SaveForm(entity);
            }
        }
    }
}
