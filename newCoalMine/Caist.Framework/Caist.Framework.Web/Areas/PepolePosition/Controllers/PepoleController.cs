using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.PepolePosition.Controllers
{
    [Area("PepolePosition")]
    public class PepoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}