using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Shuibenghistory;
using Caist.Framework.Model.Param.Shuibenghistory;
using Caist.Framework.Service.Shuibenghistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ShuibengMonitorController : ControllerBase
    {
        private readonly ShuibengMonitorBLL shuibengMonitorBLL = new ShuibengMonitorBLL();
        private ShuibengMonitorService ShuibengMonitor = new ShuibengMonitorService();

        //#region 获取数据

        /// <summary>
        /// 获取水泵数据
        /// </summary>
        /// <param name="param">SecurityMonitorParam</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityInfoList([FromQuery] ShuibengMonitorParam param)
        {
            var obj = await ShuibengMonitor.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }

    }
}