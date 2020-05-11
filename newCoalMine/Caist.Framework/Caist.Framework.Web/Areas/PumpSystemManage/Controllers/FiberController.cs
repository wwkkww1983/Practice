using Caist.Framework.Business.FiberManage;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.PumpSystemManage.Controllers
{
    [Area("PumpSystemManage")]
    public class FiberController : Controller
    {
        private FiberBLL fiberBLL = new FiberBLL();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GetFiberTempLists([FromBody]FiberParam param)
        {
            var obj = await fiberBLL.GetFiberInfoList(param);
            return obj.RemoveNullValue();
        }
    }
}