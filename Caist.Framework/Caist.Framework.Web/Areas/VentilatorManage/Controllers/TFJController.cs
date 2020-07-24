using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.FiberManage;
using Caist.Framework.Business.Tongfenghistory;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Model.Param.Tongfenghistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.TFJDemo.Controllers
{
    [Area("VentilatorManage")]
    public class TFJController : Controller
    {
        private TongfengMonitorBLL tongfengMonitorBLL = new TongfengMonitorBLL();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GettongfengTempLists([FromBody]TongfengMonitorParam param)
        {
            var obj = await tongfengMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
        public async Task<string> GettongfengTempListsr([FromBody]TongfengMonitorParam param)
        {
            var obj = await tongfengMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
    }
}