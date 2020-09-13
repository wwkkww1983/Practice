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
    public class SensorController : BaseController
    {
        private readonly SensorBLL server = new SensorBLL();
        #region 视图功能
        [AuthorizeFilter("application:Sensor:view")]
        public IActionResult SensorIndex()
        {

            return View();
        }

        public async Task<IActionResult> SensorForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("application:Sensor:search")]
        public async Task<IActionResult> GetListJson(SensorParam param)
        {
            TData<List<SensorEntity>> obj = await server.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("application:Sensor:search")]
        public async Task<IActionResult> GetPageListJson(SensorParam param, Pagination pagination)
        {
            TData<List<SensorEntity>> obj = await server.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<SensorEntity> obj = await server.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("application:Sensor:add,application:Sensor:edit")]
        public async Task<IActionResult> SaveFormJson(SensorEntity entity)
        {
            TData<string> obj = await server.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("application:Sensor:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await server.DeleteForm(ids);
            return Json(obj);
        }
        #endregion

    }
}