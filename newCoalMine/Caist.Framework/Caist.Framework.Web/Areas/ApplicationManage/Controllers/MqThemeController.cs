﻿using Caist.Framework.Business.ApplicationManage;
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
    public class MqThemeController : BaseController
    {
        private MqThemeBLL mqThemeBLL = new MqThemeBLL();

        #region 视图功能
        [AuthorizeFilter("application:mqtheme:view")]
        public IActionResult MqThemeIndex()
        {
            return View();
        }

        public async Task<IActionResult> MqThemeForm()
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
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<MqThemeEntity> obj = await mqThemeBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await mqThemeBLL.GetMaxSort();
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
    }
}