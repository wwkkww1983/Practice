using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.AlarmManage;
using Caist.Framework.Business.Baojinghistory;
using Caist.Framework.Model.Param.AlarmManage;
using Caist.Framework.Model.Param.Baojinghistory;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Service.AlarmManage;
using Caist.Framework.Service.Baojinghistory;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
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
        private AlarmReccordService alarm = new AlarmReccordService();

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
        /// 告警记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetListAlarmReccordJson([FromQuery] ALarmReccordListParam param)
        {
            var obj = await alarm.GetList(param);
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
        /// 获取有报警预案的报警记录条数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetAlarmPlanCount()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await BojingMonitor.GetAlarmCount();
            obj.TotalCount = 1;
            obj.Tag = 1;
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

        /// <summary>
        /// 首页获取告警统计量曲线图
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetAlarmCure([FromQuery] ALarmReccordListParam param)
        {
            var SystemName = await alarm.GetAlarmSystemNameList();
            if (SystemName != null && SystemName.Count() > 0)
            {
                foreach (var item in SystemName)
                {
                    param.SystemId = item.SystemId;
                    var data = await alarm.GetAlarmCureList(param);
                    List<AlarmCount> newData = new List<AlarmCount>();
                    int hours = 24;

                    if (param.StartDate.Value.Date.Equals(DateTime.Now.Date))
                    {
                        hours = DateTime.Now.Hour+1; //显示到当前时间
                    }
                    for (int i = 0; i < hours; i++)
                    {
                        AlarmCount value = new AlarmCount();
                        var timeData = data.FindLast(n => Convert.ToDateTime(n.AlarmTime).Hour == i);
                        if (timeData != null)
                        {
                            value.Count = timeData.Count;

                        }
                        value.AlarmTime = param.StartDate.Value.AddHours(i).ToString("HH");
                        newData.Add(value);
                    }
                    SystemName[SystemName.IndexOf(item)].Data = newData;
                }
            }
            return SystemName.RemoveNullValue();
        }

    }
}