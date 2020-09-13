using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Tongfenghistory;
using Caist.Framework.Model.Param.Tongfenghistory;
using Caist.Framework.Service.Tongfenghistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TongfengMonitorController : ControllerBase
    {
        private readonly TongfengMonitorBLL tongfengMonitorBLL = new TongfengMonitorBLL();
        private TongfengMonitorService tongfengMonitor = new TongfengMonitorService();

        //#region 获取数据

        /// <summary>
        /// 获取通风机数据
        /// </summary>
        /// <param name="param">SecurityMonitorParam</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityInfoList([FromQuery] TongfengMonitorParam param)
        {
            var obj = await tongfengMonitor.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }

    }
}