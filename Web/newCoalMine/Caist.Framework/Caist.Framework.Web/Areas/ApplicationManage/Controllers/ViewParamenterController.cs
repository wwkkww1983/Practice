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
    public class ViewParamenterController : BaseController
    {
        private ViewParamenterBLL viewParamenterBLL = new ViewParamenterBLL();

        #region 视图功能
        [AuthorizeFilter("application:viewparamenter:view")]
        public IActionResult ViewParamenterIndex()
        {
            return View();
        }

        public async Task<IActionResult> ViewParamenterForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("application:viewparamenter:search")]
        public async Task<IActionResult> GetListJson(ViewParamenterListParam param)
        {
            TData<List<ViewParamenterEntity>> obj = await viewParamenterBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("application:viewparamenter:search")]
        public async Task<IActionResult> GetPageListJson(ViewParamenterListParam param, Pagination pagination)
        {
            TData<List<ViewParamenterEntity>> obj = await viewParamenterBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<ViewParamenterEntity> obj = await viewParamenterBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await viewParamenterBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("application:viewparamenter:add,application:viewparamenter:edit")]
        public async Task<IActionResult> SaveFormJson(ViewParamenterEntity entity)
        {
            TData<string> obj = await viewParamenterBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("application:viewparamenter:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await viewParamenterBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}