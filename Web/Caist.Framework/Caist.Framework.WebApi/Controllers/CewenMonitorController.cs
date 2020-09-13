using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Cewenhistory;
using Caist.Framework.Model.Param.Cewenhistory;
using Caist.Framework.Service.Cewenhistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CewenMonitorController : ControllerBase
    {
        private readonly CewenMonitorBLL cewenMonitorBLL = new CewenMonitorBLL();
        private CewenMonitorService cewenMonitor = new CewenMonitorService();

        //#region 获取数据

        /// <summary>
        /// 获取Chouwen数据
        /// </summary>
        /// <param name="param">SecurityMonitorParam</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityInfoList([FromQuery] CewenMonitorParam param)
        {
            var obj = await cewenMonitor.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }

    }
}