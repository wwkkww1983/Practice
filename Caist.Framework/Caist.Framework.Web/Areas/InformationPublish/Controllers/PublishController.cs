using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.InformationPublish.Controllers
{
    [Area("InformationPublish")]
    public class PublishController : BaseController
    {
        private InformationPublishBLL InformationPublishBLL = new InformationPublishBLL();
        #region 视图页面
        [AuthorizeFilter("InformationPublish:Publish:view")]
        public IActionResult PublishIndex()
        {
            return View();
        }
        #endregion
        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("InformationPublish:Publish:search")]
        public async Task<IActionResult> GetListJson(InformationPublishParam param)
        {
            TData<List<InformationPublishEntity>> obj = await InformationPublishBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("InformationPublish:Publish:search")]
        public async Task<IActionResult> GetPageListJson(InformationPublishParam param, Pagination pagination)
        {
            TData<List<InformationPublishEntity>> obj = await InformationPublishBLL.GetPageList(param,pagination);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("InformationPublish:Publish:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await InformationPublishBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}