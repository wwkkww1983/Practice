using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.SystemManage;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class LogbackgroundController : Controller
    {
        private LogbackgroundBLL logbackgroundBLL = new LogbackgroundBLL();
        #region 视图功能
        [AuthorizeFilter("system:Logbackground:view")]
        public IActionResult Logbackgroundex()
        {
            return View();
        }
        #endregion
    }
}