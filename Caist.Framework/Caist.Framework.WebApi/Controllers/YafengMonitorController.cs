using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.yafenghistory;
using Caist.Framework.Model.yafenghistory;
using Caist.Framework.Service.yafenghistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class YafengMonitorController : ControllerBase
    {
        private readonly yafengMonitorBLL yafengMonitorBLL = new yafengMonitorBLL();
        private readonly yafengMonitorService yafengMonitor = new yafengMonitorService();
       
        #region 获取数据

        /// <summary>
        /// 获取yafeng历史数据
        /// </summary>
        /// <param name="param">yafengMonitorParam</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityInfoList([FromQuery] yafengMonitorParam param)
        {

            
            var obj = await yafengMonitor.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
        #endregion
    }
}