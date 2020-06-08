using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Baojinghistory;
using Caist.Framework.Model.Param.Baojinghistory;
using Caist.Framework.Service.Baojinghistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BaoJIngController : ControllerBase
    {

        private readonly BojingMonitorBLL BojingMonitorBLL = new BojingMonitorBLL();
        private BojingMonitorService BojingMonitor = new BojingMonitorService();

        //#region 获取数据

        /// <summary>
        /// 获取Chouwen数据
        /// </summary>
        /// <param name="param">SecurityMonitorParam</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityInfoList([FromQuery] BojingMonitorParam param)
        {
            var obj = await BojingMonitor.GetSecurityInfoList(param);
           
            return obj.RemoveNullValue();
        }

    }
}