using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// 获取报警预案详情
        /// </summary>
        /// <param name="AlarmField">mk_alarm_plan.alarm_field</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetAlarmPlanInfo([FromQuery] long? AlarmField)
        {
            var obj = await BojingMonitor.GetAlarmPlanInfo(AlarmField);
            return obj.RemoveNullValue();
        }

        /// <summary>
        /// 区域环境数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetAreaAlarmInfo()
        {
            var obj = await BojingMonitor.GetAreaAlarmInfo();
            return obj.RemoveNullValue();

        }
    }
}