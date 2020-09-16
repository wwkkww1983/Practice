using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.ApplicationManage.Controllers
{
    [Area("ApplicationManage")]
    public class MqThemeController : BaseController
    {
        private MqThemeBLL mqThemeBLL = new MqThemeBLL();
        private MqtSettingBLL mqtSettingBLL = new MqtSettingBLL();
        private MqtAddressTypeBLL mqtAddressTypeBLL = new MqtAddressTypeBLL();
        private readonly SensorBLL server = new SensorBLL();
        #region 视图功能
        [AuthorizeFilter("application:mqtheme:view")]
        public IActionResult MqThemeIndex()
        {
            return View();
        }

        [AuthorizeFilter("application:mqtheme:view")]
        public IActionResult MqtSettingIndex()
        {
            return View();
        }


        [AuthorizeFilter("application:mqtheme:view")]
        public IActionResult MqtUploadIndex()
        {
            return View();
        }
        public async Task<IActionResult> MqThemeForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }


        public async Task<IActionResult> MqtSettingForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("application:mqtheme:search")]
        public async Task<IActionResult> GetListJson(MqThemeListParam param)
        {
            TData<List<MqThemeEntity>> obj = await mqThemeBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("application:mqtheme:search")]
        public async Task<IActionResult> GetPageListJson(MqThemeListParam param, Pagination pagination)
        {
            TData<List<MqThemeEntity>> obj = await mqThemeBLL.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("application:mqtheme:search")]
        public async Task<IActionResult> GetSettingPageListJson(MqtSettingParam param, Pagination pagination)
        {
            TData<List<MqtSettingEntity>> obj = await mqtSettingBLL.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("application:mqtheme:search")]
        public async Task<IActionResult> GetUploadPageListJson(MqtSettingParam param, Pagination pagination)
        {
            var mqt = mqThemeBLL.GetList(new MqThemeListParam()).Result.Result.FirstOrDefault();
            #region 请求mqtt已上传数据
            string result =  HttpHelper.HttpPost(Util.GlobalContext.SystemConfig.MqtUrl, 
                "mkCode="+ mqt.MqCollieryCode + "&systemCode="+param.SystemId+ "&key="+ Util.GlobalContext.SystemConfig.MqtKey);
            #endregion
            var data = JsonHelper.ToObject<MqtUploadResEntity>(result);
            TData<List<MqtUploadEntity>> obj = new TData<List<MqtUploadEntity>>();
            obj.Result = data.state ? data.data : null;
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return Json(obj);
        }


        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<MqThemeEntity> obj = await mqThemeBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetSettingFormJson(long id)
        {
            TData<MqtSettingEntity> obj = await mqtSettingBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await mqThemeBLL.GetMaxSort();
            return Json(obj);
        }



        [HttpGet]
        public async Task<IActionResult> GetAddressTypeListJson(MqtAddressTypeParam param)
        {
            TData<List<MqtAddressTypeEntity>> obj = await mqtAddressTypeBLL.GetList(param);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("application:mqtheme:add,application:mqtheme:edit")]
        public async Task<IActionResult> SaveFormJson(MqThemeEntity entity)
        {
            TData<string> obj = await mqThemeBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("application:mqtheme:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await mqThemeBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion

        #region  提交配置数据
        [HttpPost]
        [AuthorizeFilter("application:mqtheme:add,application:mqtheme:edit")]
        public async Task<IActionResult> SaveSettingFormJson(MqtSettingEntity entity)
        {
            var sensor = await server.GetEntity(entity.SensorId);
            entity.ValueLength = sensor.Result.ValueLength;
            entity.DecimalPlaces = sensor.Result.DecimalPlaces;
            entity.SensorTypeCode = sensor.Result.Code;
            TData<string> obj = await mqtSettingBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("application:mqtheme:delete")]
        public async Task<IActionResult> DeleteSettingFormJson(string ids)
        {
            TData obj = await mqtSettingBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}