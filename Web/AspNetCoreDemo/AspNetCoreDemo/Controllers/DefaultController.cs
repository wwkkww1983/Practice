using AspNetCoreDemo.BLL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemo.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IMapper _mapper;
        public DefaultController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var pepole = UserInfo.GetUserDetails();
            Pepole_E pe = _mapper.Map<Pepole_E>(pepole);
            ViewBag.Pepole = pe;
            return View();
        }
    }
}