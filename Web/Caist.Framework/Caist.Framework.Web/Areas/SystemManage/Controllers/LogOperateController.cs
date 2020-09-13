using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Caist.Framework.Web.Controllers;
using Caist.Framework.Business.SystemManage;
using Caist.Framework.Entity.SystemManage;
using Caist.Framework.Model;
using Caist.Framework.Model.Param.SystemManage;
using Caist.Framework.Util.Model;

namespace Caist.Framework.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class LogOperateController : BaseController
    {
        private LogOperateBLL sysLogOperateBLL = new LogOperateBLL();

        #region 视图功能
        [AuthorizeFilter("system:logoperate:view")]
        public IActionResult LogOperateIndex()
        {
            return View();
        }

        public IActionResult LogOperateDetail()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:logoperate:search")]
        public async Task<IActionResult> GetListJson(LogOperateListParam param)
        {
            TData<List<LogOperateEntity>> obj = await sysLogOperateBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:logoperate:search")]
        public async Task<IActionResult> GetPageListJson(LogOperateListParam param, Pagination pagination)
        {
            TData<List<LogOperateEntity>> obj = await sysLogOperateBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<LogOperateEntity> obj = await sysLogOperateBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:logoperate:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await sysLogOperateBLL.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("system:logoperate:delete")]
        public async Task<IActionResult> RemoveAllFormJson()
        {
            TData obj = await sysLogOperateBLL.RemoveAllForm();
            return Json(obj);
        }
        #endregion
    }
}