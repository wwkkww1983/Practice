using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Cewenhistory;
using Caist.Framework.Business.PeopleManage;
using Caist.Framework.Entity.PeopleManage;
using Caist.Framework.Model.Param.Cewenhistory;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Service.Cewenhistory;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
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
        private FiberMarkBLL fiberMarkBLL = new FiberMarkBLL();

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

        [HttpGet]
        public async Task<string> GetFiberCure([FromQuery] CewenMonitorParam param)
        {
            var PointName = await cewenMonitor.GetPointNameList();
           
            if (PointName!=null && PointName.Count() > 0)
            {
                foreach (var item in PointName)
                {
                    param.DictId = item.PointName;
                    PointName[PointName.IndexOf(item)].value = await cewenMonitor.GetPointAverageList(param);
                }
            }
            return PointName.RemoveNullValue();
        }

        /// <summary>
        /// 获取图标标记坐标信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetMarkList()
        {
            var obj = await fiberMarkBLL.GetList();
            return obj.RemoveNullValue();
        }

        #region 提交数据
        /// <summary>
        /// 更新页面坐标
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TData<string>> SavePointFormJson([FromQuery] FiberMark entity)
        {
            if (!entity.Id.HasValue)
            {
                TData<string> data = new TData<string>();
                data.Message = "提交数据失败：ID不能为空";
                data.Tag = 0;
                return data;
            }
            var obj = await fiberMarkBLL.SaveForm(entity);
            return obj;
        }

        #endregion

    }
}