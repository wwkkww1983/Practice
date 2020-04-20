using Caist.Framework.Business.PointManage;
using Caist.Framework.Entity.PointManage;
using Caist.Framework.Model.Param.PointManage;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.PointManage.Controllers
{
    [Area("PointManage")]
    public class DeviceController : BaseController
    {
        private DeviceBLL deviceBLL = new DeviceBLL();

        #region 视图功能
        [AuthorizeFilter("device:device:view")]
        public IActionResult DeviceIndex()
        {
            return View();
        }

        public async Task<IActionResult> DeviceForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("device:device:search")]
        public async Task<IActionResult> GetListJson(DeviceListParam param)
        {
            TData<List<DeviceEntity>> obj = await deviceBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("device:device:search")]
        public async Task<IActionResult> GetPageListJson(DeviceListParam param, Pagination pagination)
        {
            TData<List<DeviceEntity>> obj = await deviceBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<DeviceEntity> obj = await deviceBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await deviceBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("device:device:add,device:device:edit")]
        public async Task<IActionResult> SaveFormJson(DeviceEntity entity)
        {
            TData<string> obj = await deviceBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("device:device:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await deviceBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}