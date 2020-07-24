using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.PeopleManage;
using Caist.Framework.Model.PeopleManage;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.PepolePosition.Controllers
{
    [Area("PepolePosition")]
    public class PepoleController : Controller
    {
        private RegionBLL regionBLL = new RegionBLL();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> PersonnelList([FromBody]RegionParam param)
        {
            var obj = await regionBLL.PersonnelList(param);
            return obj.RemoveNullValue();
        }
        public async Task<string> PersonnelListr([FromBody]RegionParam param)
        {
            var obj = await regionBLL.PersonnelList(param);
            return obj.RemoveNullValue();
        }
    }
}