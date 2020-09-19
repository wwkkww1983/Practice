using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Enum;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Model.Param.OrganizationManage;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Service.FiberManage;
using Caist.Framework.Service.PeopleManage;
using Caist.Framework.Service.PointManage;
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
        private DeviceService deviceService = new DeviceService();
        private RegionService regionService = new RegionService();
        private FiberService fiberService = new FiberService();
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

            TempList.ForEach((n) =>
            {
                //正则匹配点位 根据点位匹配数据

                if (n.linkContent.Contains("井下实时人员数量")) //人员信息联动
                {
                    string result = "";
                    List<PublicPeopleRealTime> data = null;
                    //是否有实时数据接口
                    if (!string.IsNullOrEmpty(Util.GlobalContext.SystemConfig.PeopleRealTime))
                    {
                        result = HttpHelper.HttpPost(Util.GlobalContext.SystemConfig.PeopleRealTime, "");
                        if (!string.IsNullOrEmpty(result))
                            data = JsonHelper.ToObject<List<PublicPeopleRealTime>>(result);
                    }
                    else  //数据库读取
                    {
                        data = regionService.GetPeopleRealTime().Result;
                    }
                    if (data != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("井下实时人员数量：{0}人 \r\n", data.Count);
                        sb.AppendFormat("编 号   姓 名      职位           位  置\r\n");
                        foreach (var item in data)
                        {
                            item.TypeOfWork = !string.IsNullOrEmpty(item.TypeOfWork) ? item.TypeOfWork : "普工";
                            #region 处理文字长度
                            if ((4 - item.PepoleName.Length) != 0)
                            {
                                var length = 4 - item.PepoleName.Length;
                                for (int i = 0; i < length; i++)
                                {
                                    item.PepoleName += "  ";
                                }
                            }

                            if ((5 - item.TypeOfWork.Length) != 0)
                            {
                                var length = 5 - item.TypeOfWork.Length;
                                for (int i = 0; i < length; i++)
                                {
                                    item.TypeOfWork += "  ";
                                }
                            }
                            #endregion
                            sb.AppendFormat("{0}   {1}   {2}     {3}\r\n", item.PepoleNumber, item.PepoleName,
                                item.TypeOfWork, item.StationAddress);
                        }
                        n.linkContent = sb.ToString();
                    }
                }
                else  //plc采集的系统
                {
                    MatchCollection Matches = Regex.Matches(n.linkContent, @"(?<=\{\{)[^}]*(?=\}\})", RegexOptions.Multiline);
                    SystemDataParam param = new SystemDataParam();
                    foreach (Match NextMatch in Matches)
                    {
                        var dict = NextMatch.ToString();

                        if (dict.Contains("Value"))  //光纤
                        {
                            var dicts = dict.Split("-");
                            var value = GetFiberValue(dicts[0].ToString(), dicts[1].ToString());
                            n.linkContent = n.linkContent.Replace("{{" + NextMatch.Value + "}}", value!=null ? value.Result : "--");
                        }
                        else if (dict.Contains("枢")) //供配电
                        {

                        }
                        //针对PLC采集点表数据
                        else if (NextMatch.ToString().Contains("-DB"))
                        {
                            var TabName = deviceService.GetSysDataTableName(NextMatch.Value);
                            if (TabName.Result != null)
                            {
                                param.DictId = NextMatch.Value;
                                param.TabName = TabName.Result.TabName;
                                //开始获取数据
                                var data = viewManipulateModelBLL.GetPublishList(param);
                                if (data.Result != null && data.Result.Result != null) //如果又数据的话
                                {
                                    var entity = data.Result.Result;
                                    var value = ConvertData(entity);
                                    //替换当前行中已经获取到的点位数据
                                    n.linkContent = n.linkContent.Replace("{{" + param.DictId + "}}", value);
                                }
                                else
                                {
                                    n.linkContent = n.linkContent.Replace("{{" + param.DictId + "}}", "--"); //如果没有获取到数据直接返回--
                                }
                            }
                        }
                    }
                }

                PublishList.Add(n);

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

        /// <summary>
        /// 根据数据类型转换显示数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static string ConvertData(SystemDataEntity entity)
        {
            string value;
            if (IsInt(entity.DictValue))
            {
                value = entity.DictValue;
            }
            else if (IsNumeric(entity.DictValue))
            {
                value = Convert.ToDouble(entity.DictValue).ToString("f2");
            }
            else if (IsBool(entity.DictValue))
            {
                value = Convert.ToBoolean(entity.DictValue) ? "是" : "否";
            }
            else
            {
                value = entity.DictValue;
            }

            return value;
        }
        /// <summary>
        /// 根据区域和点位获取对应值
        /// </summary>
        /// <param name="AreaName"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        private  async  Task<string> GetFiberValue(string AreaName,string dict)
        {

            var Entity =  await fiberService.GetFiberAreaData(AreaName);
            string value = "0";
            switch (dict)
            {
                case "AverageValue":
                    value = Entity.AverageValue;
                    break;
                case "MaxValue":
                    value = Entity.MaxValue;
                    break;
                case "MinValue":
                    value = Entity.MinValue;
                    break;
                default:
                    break;
            }
            return value;
        }


        //输出信息发布数据txt文件到指定得ftp服务器
        private bool WriteDirAndUplpadFtp(string path, string filename, string content)
        {
            bool status = false;

            try    //如果有ftp服务器需要上传到ftp服务器，如果没有，抛出异常，并且只保存到指定磁盘目录
            {

                if (GlobalContext.SystemConfig.FTPMode == FTPMode.Ftp)
                {

                    #region  ftp先写到本地磁盘在上传
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
                    #endregion
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
                    status = ftpStatus == FtpStatus.Success;
                }
                else if (GlobalContext.SystemConfig.FTPMode == FTPMode.UNC)
                {
                    FileShareHelper.connectState();
                    status = FileShareHelper.WriteFiles(filename, content);
                }
                else  //直接走的磁盘路径发布模式
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
                    status = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write(ex);
            }
            return status;
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
        static bool IsInt(string s)
        {
            int v;
            if (int.TryParse(s, out v))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool IsNumeric(string s)
        {
            double v;
            if (double.TryParse(s, out v))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool IsBool(string s)
        {
            bool v;
            if (bool.TryParse(s, out v))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
