using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.FiberManage;
using Caist.Framework.Business.Jushanhistory;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Model.Param.Jushanhistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.PSDemo.Controllers
{
    [Area("DrainagesystemManage")]
    public class JushanController : Controller
    {
        private JushanMonitorBLL jushanMonitorBLL = new JushanMonitorBLL();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetjushanTempLists([FromBody]JushanMonitorParam param)
        {
            var obj = await jushanMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
        public async Task<string> GetjushanTempListsr([FromBody]JushanMonitorParam param)
        {
            var obj = await jushanMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
    }
}
