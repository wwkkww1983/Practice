using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Choucahistory;
using Caist.Framework.Business.FiberManage;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Model.Param.Choucahistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.WSCCDemo.Controllers
{
    [Area("GasextractionManage")]
    public class WSCCController : Controller
    {
        private ChoucaiMonitorBLL choucaiMonitorBLL = new ChoucaiMonitorBLL();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetChoucaiTempLists([FromBody]ChoucaiMonitorParam param)
        {
            var obj = await choucaiMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
    }
}
