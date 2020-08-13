using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.InformationPublish.Controllers
{
    [Area("InformationPublish")]
    public class TemplateController : BaseController
    {
        private InformationTemplateBLL InformationTemplateBLL = new InformationTemplateBLL();
        #region 视图页面
        [AuthorizeFilter("InformationPublish:Template:view")]
        public IActionResult TemplateIndex()
        {
            return View();
        }


        public async Task<IActionResult> TemplateForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion
        #region 获取数据

        [HttpGet]
        [AuthorizeFilter("InformationPublish:Template:search")]
        public async Task<IActionResult> GetPageListJson(InformationPublishParam param, Pagination pagination)
        {
            TData<List<PublishContent>> obj = await InformationTemplateBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [AuthorizeFilter("InformationPublish:Template:search")]
        [HttpGet]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<PublishContent> obj = await InformationTemplateBLL.GetEntity(id);
            return Json(obj);
        }

        #endregion

        #region 提交数据

        [HttpPost]
        [AuthorizeFilter("InformationPublish:Template:add,InformationPublish:Template:edit")]
        public async Task<IActionResult> SaveFormJson(PublishContent entity,int Index,int Add)
        {
            TData<string> obj = await InformationTemplateBLL.SaveForm(entity,Index, Add);
            return Json(obj);
        }
        [HttpPost]
        [AuthorizeFilter("InformationPublish:Template:add,InformationPublish:Template:delete")]
        public async Task<IActionResult> DeleteFormJson(int Index)
        {
            TData<string> obj = await InformationTemplateBLL.DeleteForm(Index);
            return Json(obj);
        }

        #endregion
    }
}