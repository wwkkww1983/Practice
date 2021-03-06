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
    public class ViewControlModelController : BaseController
    {
        private ViewControlModelBLL viewControlModelBLL = new ViewControlModelBLL();

        #region 视图功能
        [AuthorizeFilter("application:viewControlmodel:view")]
        public IActionResult ViewControlModelIndex()
        {
            return View();
        }

        public async Task<IActionResult> ViewControlModelForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("application:viewControlmodel:search")]
        public async Task<IActionResult> GetListJson(ViewControlModelListParam param)
        {
            TData<List<ViewControlModelEntity>> obj = await viewControlModelBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("application:viewControlmodel:search")]
        public async Task<IActionResult> GetPageListJson(ViewControlModelListParam param, Pagination pagination)
        {
            TData<List<ViewControlModelEntity>> obj = await viewControlModelBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<ViewControlModelEntity> obj = await viewControlModelBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await viewControlModelBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("application:viewControlmodel:add,application:viewControlmodel:edit")]
        public async Task<IActionResult> SaveFormJson(ViewControlModelEntity entity)
        {
            TData<string> obj = await viewControlModelBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("application:viewControlmodel:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await viewControlModelBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}