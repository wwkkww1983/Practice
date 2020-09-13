using Caist.Framework.Business.SettingValueManage;
using Caist.Framework.Entity.SettingValueManage;
using Caist.Framework.Model.Param.SettingValueManage;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.SettingValueManage.Controllers
{
    [Area("SettingValueManage")]
    public class SettingValueController : BaseController
    {
        private SettingValueBLL settingValueBLL = new SettingValueBLL();

        #region 视图功能
        [AuthorizeFilter("settingvalue:settingvalue:view")]
        public IActionResult SettingValueIndex()
        {
            return View();
        }

        public async Task<IActionResult> SettingValueForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("settingvalue:settingvalue:search")]
        public async Task<IActionResult> GetListJson(SettingValueListParam param)
        {
            TData<List<SettingValueEntity>> obj = await settingValueBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("settingvalue:settingvalue:search")]
        public async Task<IActionResult> GetPageListJson(SettingValueListParam param, Pagination pagination)
        {
            TData<List<SettingValueEntity>> obj = await settingValueBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<SettingValueEntity> obj = await settingValueBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await settingValueBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("settingvalue:settingvalue:add,settingvalue:settingvalue:edit")]
        public async Task<IActionResult> SaveFormJson(SettingValueEntity entity)
        {
            TData<string> obj = await settingValueBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("settingvalue:settingvalue:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await settingValueBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}