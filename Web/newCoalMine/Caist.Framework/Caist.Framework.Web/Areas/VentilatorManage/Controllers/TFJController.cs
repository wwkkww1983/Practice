using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.FiberManage;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.TFJDemo.Controllers
{
    [Area("VentilatorManage")]
    public class TFJController : Controller
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