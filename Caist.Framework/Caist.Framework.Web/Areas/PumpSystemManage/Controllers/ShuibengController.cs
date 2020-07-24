using Caist.Framework.Business.FiberManage;
using Caist.Framework.Business.Shuibenghistory;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Model.Param.Shuibenghistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.PumpSystemManage.Controllers
{
    [Area("PumpSystemManage")]
    public class ShuibengController : Controller
    {
        private ShuibengMonitorBLL shuibengMonitorBLL = new ShuibengMonitorBLL();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GetShuibengTempLists([FromBody]ShuibengMonitorParam param)
        {
            var obj = await shuibengMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
        public async Task<string> GetShuibengTempListsr([FromBody]ShuibengMonitorParam param)
        {
            var obj = await shuibengMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
    }
}