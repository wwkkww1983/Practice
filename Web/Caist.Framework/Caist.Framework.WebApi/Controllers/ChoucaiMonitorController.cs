using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Choucahistory;
using Caist.Framework.Model.Param.Choucahistory;
using Caist.Framework.Service.Choucahistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ChoucaiMonitorController : ControllerBase
    {
        private readonly ChoucaiMonitorBLL choucaiMonitorBLL = new ChoucaiMonitorBLL();
        private ChoucaiMonitorService choucaiMonitor = new ChoucaiMonitorService();

        //#region 获取数据

        /// <summary>
        /// 获取Choucai数据
        /// </summary>
        /// <param name="param">SecurityMonitorParam</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityInfoList([FromQuery] ChoucaiMonitorParam param)
        {
            var obj = await choucaiMonitor.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }

    }
}