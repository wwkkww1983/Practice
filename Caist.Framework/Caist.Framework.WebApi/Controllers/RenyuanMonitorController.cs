using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Renyuanhistory;
using Caist.Framework.Model.Param.Renyuanhistory;
using Caist.Framework.Service.Renyuanhistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RenyuanMonitorController : ControllerBase
    {
        private readonly RenyuanMonitorBLL renyuanMonitorBLL = new RenyuanMonitorBLL();
        private RenyuanMonitorService renyuanMonitor = new RenyuanMonitorService();

        //#region 获取数据

        /// <summary>
        /// 获取Choucai数据
        /// </summary>
        /// <param name="param">SecurityMonitorParam</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityInfoList([FromQuery] RenyuanMonitorParam param)
        {
            var obj = await renyuanMonitor.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }

    }
}