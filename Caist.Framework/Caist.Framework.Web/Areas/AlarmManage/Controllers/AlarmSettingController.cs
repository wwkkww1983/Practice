using Caist.Framework.Business.AlarmManage;
using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Model.Param.AlarmManage;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.AlarmManage.Controllers
{
    [Area("AlarmManage")]
    public class AlarmSettingController : BaseController
    {
        private AlarmSettingBLL alarmSetting = new AlarmSettingBLL();

        #region 视图功能
        [AuthorizeFilter(" alarmgsetting:alarmgsetting:view")]
        public IActionResult AlarmSettingIndex()
        {
            return View();
        }

        public async Task<IActionResult> AlarmSettingForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("alarmgsetting:alarmgsetting:search")]
        public async Task<IActionResult> GetListJson(AlarmSettingListParam param)
        {
            TData<List<AlarmSettingsEntity>> obj = await alarmSetting.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("alarmgsetting:alarmgsetting:search")]
        public async Task<IActionResult> GetPageListJson(AlarmSettingListParam param, Pagination pagination)
        {
            TData<List<AlarmSettingsEntity>> obj = await alarmSetting.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<AlarmSettingEntity> obj = await alarmSetting.GetEntity(id);
            return Json(obj);
        }

        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("alarmgsetting:alarmgsetting:add,alarmgsetting:alarmgsetting:edit")]
        public async Task<IActionResult> SaveFormJson(AlarmSettingEntity entity)
        {
            TData<string> obj = await alarmSetting.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("alarmgsetting:alarmgsetting:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await alarmSetting.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}