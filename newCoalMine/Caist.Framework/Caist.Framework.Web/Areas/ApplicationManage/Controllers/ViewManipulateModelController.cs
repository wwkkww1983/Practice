using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.ApplicationManage.Controllers
{
    [Area("ApplicationManage")]
    public class ViewManipulateModelController : BaseController
    {
        private ViewManipulateModelBLL viewManipulateModelBLL = new ViewManipulateModelBLL();

        #region 视图功能
        [AuthorizeFilter("application:viewmanipulatemodel:view")]
        public IActionResult ViewManipulateModelIndex()
        {
            return View();
        }

        public async Task<IActionResult> ViewManipulateModelForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("application:viewmanipulatemodel:search")]
        public async Task<IActionResult> GetListJson(ViewManipulateModelListParam param)
        {
            TData<List<ViewManipulateModelEntity>> obj = await viewManipulateModelBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("application:viewmanipulatemodel:search")]
        public async Task<IActionResult> GetPageListJson(ViewManipulateModelListParam param, Pagination pagination)
        {
            TData<List<ViewManipulateModelEntity>> obj = await viewManipulateModelBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<ViewManipulateModelEntity> obj = await viewManipulateModelBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await viewManipulateModelBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("application:viewmanipulatemodel:add,application:viewmanipulatemodel:edit")]
        public async Task<IActionResult> SaveFormJson(ViewManipulateModelEntity entity)
        {
            TData<string> obj = await viewManipulateModelBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("application:viewmanipulatemodel:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await viewManipulateModelBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}