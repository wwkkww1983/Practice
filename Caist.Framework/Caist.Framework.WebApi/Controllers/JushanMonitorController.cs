using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Jushanhistory;
using Caist.Framework.Model.Param.Jushanhistory;
using Caist.Framework.Service.Jushanhistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class JushanMonitorController : ControllerBase
    {
        private readonly JushanMonitorBLL jushanMonitorBLL = new JushanMonitorBLL();
        private JushanMonitorService jushanMonitor = new JushanMonitorService();

        //#region 获取数据

        /// <summary>
        /// 获取Jushan数据
        /// </summary>
        /// <param name="param">SecurityMonitorParam</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityInfoList([FromQuery] JushanMonitorParam param)
        {
            var obj = await jushanMonitor.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }

    }
}