using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.ApplicationManage.Controllers
{
    [Area("ApplicationManage")]
    public class ViewFunctionController : BaseController
    {
        private ViewFunctionBLL viewFunctionBLL = new ViewFunctionBLL();

        #region 视图功能
        [AuthorizeFilter("application:viewfunction:view")]
        public IActionResult ViewFunctionIndex()
        {
            return View();
        }

        public IActionResult ViewFunctionForm()
        {
            return View();
        }

        public IActionResult ViewFunctionChoose()
        {
            return View();
        }
        public IActionResult ViewFunctionIcon()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("application:viewfunction:search,application:systemsetting:search")]
        public async Task<IActionResult> GetListJson(ViewFunctionListParam param)
        {
            TData<List<ViewFunctionEntity>> obj = await viewFunctionBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("application:viewfunction:search")]
        public async Task<IActionResult> GetPageListJson(ViewFunctionListParam param, Pagination pagination)
        {
            TData<List<ViewFunctionEntity>> obj = await viewFunctionBLL.GetPageContentList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<ViewFunctionEntity> obj = await viewFunctionBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await viewFunctionBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("application:viewfunction:add,application:viewfunction:edit")]
        public async Task<IActionResult> SaveFormJson(ViewFunctionEntity entity)
        {
            TData<string> obj = await viewFunctionBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("application:viewfunction:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await viewFunctionBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}