using Caist.Framework.Business.AlarmManage;
using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Model.Param.AlarmManage;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.AlarmManage.Controllers
{
    [Area("AlarmManage")]
    public class AlarmReccordController : BaseController
    {
        private AlarmReccordBLL alarm = new AlarmReccordBLL();

        #region 视图功能
        [AuthorizeFilter("alarmrecord:alarmrecord:view")]
        public IActionResult AlarmReccordIndex()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("alarmrecord:alarmrecord:search")]
        public async Task<IActionResult> GetListJson(ALarmReccordListParam param)
        {
            TData<List<AlarmRecordEntity>> obj = await alarm.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("alarmrecord:alarmrecord:search")]
        public async Task<IActionResult> GetPageListJson(ALarmReccordListParam param, Pagination pagination)
        {
            TData<List<AlarmRecordEntity>> obj = await alarm.GetPageList(param, pagination);
            return Json(obj);
        }

        #endregion
    }
}