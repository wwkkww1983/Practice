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
    public class SystemSettingController : BaseController
    {
        private SystemSettingBLL systemSettingBLL = new SystemSettingBLL();

        #region 视图功能
        [AuthorizeFilter("application:application:view")]
        public IActionResult SystemSettingIndex()
        {
            return View();
        }

        public async Task<IActionResult> SystemSettingForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("application:application:search")]
        public async Task<IActionResult> GetListJson(SystemSettingListParam param)
        {
            TData<List<SystemSettingEntity>> obj = await systemSettingBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("application:application:search")]
        public async Task<IActionResult> GetPageListJson(SystemSettingListParam param, Pagination pagination)
        {
            TData<List<SystemSettingEntity>> obj = await systemSettingBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<SystemSettingEntity> obj = await systemSettingBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await systemSettingBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("application:application:add,application:application:edit")]
        public async Task<IActionResult> SaveFormJson(SystemSettingEntity entity)
        {
            TData<string> obj = await systemSettingBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("application:application:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await systemSettingBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}