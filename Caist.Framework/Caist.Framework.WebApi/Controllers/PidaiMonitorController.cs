using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Pidaihistory;
using Caist.Framework.Model.Param.Pidaihistory;
using Caist.Framework.Service.Pidaihistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PidaiMonitorController : ControllerBase
    {

        private readonly PidaiMonitorBLL pidaiMonitorBLL = new PidaiMonitorBLL();
        private PidaiMonitorService pidaiMonitor = new PidaiMonitorService();

        //#region 获取数据

        /// <summary>
        /// 获取pidai数据
        /// </summary>
        /// <param name="param">SecurityMonitorParam</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityInfoList([FromQuery] PidaiMonitorParam param)
        {
            var obj = await pidaiMonitor.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }

    }
}