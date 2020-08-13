using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.InformationPublish.Controllers
{
    [Area("InformationPublish")]
    public class DeviceController : BaseController
    {
        private LedDeviceBLL ledDeviceBLL = new LedDeviceBLL();
        #region 视图页面
        [AuthorizeFilter("InformationPublish:Device:view")]
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
        [AuthorizeFilter("InformationPublish:Device:search")]
        public async Task<IActionResult> GetPageListJson(LedDeviceParam param, Pagination pagination)
        {
            TData<List<LedDeviceEntity>> obj = await ledDeviceBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [AuthorizeFilter("InformationPublish:Device:search")]
        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<LedDeviceEntity> obj = await ledDeviceBLL.GetEntity(id);
            return Json(obj);
        }
        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await ledDeviceBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据

        [HttpPost]
        [AuthorizeFilter("InformationPublish:Device:add,InformationPublish:Device:edit")]
        public async Task<IActionResult> SaveFormJson(LedDeviceEntity entity)
        {
            TData<string> obj = await ledDeviceBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("InformationPublish:Device:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await ledDeviceBLL.DeleteForm(ids);
            return Json(obj);
        }

        #endregion
    }
}