using Caist.Framework.Cache;
using Caist.Framework.Cache.Factory;
using Caist.Framework.Entity;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.ApplicationManage
{
    public class InformationTemplateBLL
    {
        #region 获取数据
        /// <summary>
        /// 模板缓存数据
        /// </summary>
        private static string _TemplateContent;
        public string TemplateContent
        {
            get
            {
                if (!string.IsNullOrEmpty(_TemplateContent))
                {
                    return _TemplateContent;
                }
                else
                {
                    _TemplateContent = CacheFactory.Cache().GetCache<string>("TemplateCache");
                    return _TemplateContent;
                }
            }
            set
            {
                _TemplateContent = value;
            }
        }
        public async Task<TData<List<PublishContentPage>>> GetPageList(InformationPublishParam param, Pagination pagination)
        {

            using (StreamReader sr = new StreamReader(GlobalContext.SystemConfig.InformationPublishTemplatePath, Encoding.UTF8))
            {
                TemplateContent = sr.ReadToEnd();
            }
            CacheFactory.Cache().AddCache("TemplateCache", TemplateContent);

            TData<List<PublishContentPage>> obj = new TData<List<PublishContentPage>>();
            obj.Result = JsonHelper.ToObject<List<PublishContentPage>>(TemplateContent);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<PublishContent>> GetEntity(int id)
        {
            TData<PublishContent> obj = new TData<PublishContent>();
            if (TemplateContent.HasValue())
            {
                obj.Result = JsonHelper.ToObject<List<PublishContent>>(TemplateContent)[id];
                obj.Tag = 1;
            }
            else
            {
                obj.Tag = 0;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(PublishContent entity, int Index, int Add)
        {
            TData<string> obj = new TData<string>();
            List<PublishContent> List = JsonHelper.ToObject<List<PublishContent>>(TemplateContent);
            if (Index.HasValue() && Index >= 0)
            {

                //List[Index].deviceUID = entity.deviceUID;
                //List[Index].linkContent = entity.linkContent;
                List[Index] = entity;
                SaveTemplate(List);
            }
            else
            {
                //TODO:这里暂时默认给到这些数据，后续如果需要可编辑修改的再加到前端页面
                if (string.IsNullOrEmpty(entity.fontColor))
                {
                    entity.fontColor = "1";
                    entity.fontName = "1";
                    entity.fontSize = "0";
                    entity.moveSpeed = "1";
                    entity.showMode = "1";
                    entity.stopTime = "1";
                    entity.attribute = "1";
                }
                List.Insert(Add - 1, entity);
                SaveTemplate(List);
            }

            obj.Result = "100";
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> DeleteForm(int Index)
        {
            TData<string> obj = new TData<string>();
            if (Index.HasValue() && Index >= 0)
            {
                List<PublishContent> List = JsonHelper.ToObject<List<PublishContent>>(TemplateContent);
                List.RemoveAt(Index);
                SaveTemplate(List);
            }
            obj.Result = "100";
            obj.Tag = 1;
            return obj;
        }
        private void SaveTemplate(List<PublishContent> List)
        {

            if (System.IO.File.Exists(Path.GetFullPath(GlobalContext.SystemConfig.InformationPublishTemplatePath)))
            {
                File.Delete(Path.GetFullPath(GlobalContext.SystemConfig.InformationPublishTemplatePath));
            }
            FileStream fs = new FileStream(GlobalContext.SystemConfig.InformationPublishTemplatePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(JsonConvert.SerializeObject(List));
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        #endregion
    }
}
