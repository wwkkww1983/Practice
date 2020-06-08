using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.FiberManage;
using Caist.Framework.Business.yafenghistory;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Model.yafenghistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.YFJDemo.Controllers
{
    [Area("PressurefanManage")]
    public class DefaultController : Controller
    {
        private yafengMonitorBLL yafengMonitorBLL = new yafengMonitorBLL();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetyafengTempLists([FromBody]yafengMonitorParam param)
        {
            var obj = await yafengMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
    }
}
  