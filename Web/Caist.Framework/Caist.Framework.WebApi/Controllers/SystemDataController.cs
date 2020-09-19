using Caist.Framework.Business;
using Caist.Framework.Business.SystemManage;
using Caist.Framework.Entity.PointManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Model.Param.OrganizationManage;
using Caist.Framework.Model.Param.SystemManage;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using NPOI.HPSF;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SystemDataController : ControllerBase
    {
        private SystemDataService systemDataService = new SystemDataService();
        //#region 获取数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetList([FromQuery] SystemDataParam param)
        {
            var obj = await systemDataService.GetSystemTableNameList(param);
            if (obj != null && obj.Result != null && obj.Result.Count > 0)
            {
                param.TabName = obj.Result[0].TabName;
                var data = await systemDataService.GetSystemDataList(param);
                if (data != null && data.Result != null && data.Result.Count > 0)
                {
                    return data.RemoveNullValue();
                }
                else
                {
                    return data.RemoveNullValue();
                }
            }
            return obj.RemoveNullValue();
        }


        /// <summary>
        /// 系统能耗数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSystemEnergy()
        {
            var tf = await systemDataService.GetTFEnergy();
            var yf = await systemDataService.GetYFEnergy();
            var pd = await systemDataService.GetPDEnergy();
            List<SystemEnergy> data = new List<SystemEnergy>();
            data.Add(new SystemEnergy
            {
                
                SystemName = "通风机能耗",
                Value = Math.Round(tf * 60, 2),  //数据库中1条数据为1分钟。能耗以小时为单位，所以诚意60为千瓦时
                Unit = "Kw‧h"
            }); ;
            data.Add(new SystemEnergy
            {
                SystemName = "压风机能耗",
                Value = Math.Round(yf * 60, 2),  //数据库中1条数据为1分钟。能耗以小时为单位，所以诚意60为千瓦时
                Unit = "Kw‧h"
            });
            data.Add(new SystemEnergy
            {
                SystemName = "运输机能耗",
                Value = Math.Round(pd * 60, 2),  //数据库中1条数据为1分钟。能耗以小时为单位，所以诚意60为千瓦时
                Unit = "Kw‧h"
            });
            return data.RemoveNullValue();
        }
    }
}