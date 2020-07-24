using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.FiberManage;
using Caist.Framework.Business.Pidaihistory;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Model.Param.Pidaihistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.PDDemo.Controllers
{
    [Area("BeltManage")]
    public class PDController : Controller
    {
        private PidaiMonitorBLL pidaiMonitorBLL = new PidaiMonitorBLL();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetpidaiTempLists([FromBody]PidaiMonitorParam param)
        {
            var obj = await pidaiMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
        public async Task<string> GetpidaiTempListsr([FromBody]PidaiMonitorParam param)
        {
            var obj = await pidaiMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
    }
}
