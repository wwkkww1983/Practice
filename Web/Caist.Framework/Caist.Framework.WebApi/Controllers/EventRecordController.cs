using Caist.Framework.Business.EventRecordManage;
using Caist.Framework.Entity.EventRecordManage;
using Caist.Framework.Model.Param.EventRecordManage;
using Caist.Framework.Util.Model;
using Caist.Framework.WebApi.Handle;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [AuthorizeFilter]
    public class EventRecordController : ControllerBase
    {
        EventRecordBLL eventRecordBLL = new EventRecordBLL();

        #region 获取数据
        [HttpGet]
        public async Task<TData<List<EventRecordEntity>>> GetListJson([FromQuery] EventRecordListParam param)
        {
            TData<List<EventRecordEntity>> obj = await eventRecordBLL.GetList(param);
            return obj;
        }

        [HttpGet]
        public async Task<TData<List<EventRecordEntity>>> GetPageListJson([FromQuery] EventRecordListParam param, [FromQuery] Pagination pagination)
        {
            TData<List<EventRecordEntity>> obj = await eventRecordBLL.GetPageList(param, pagination);


            return obj;
        }

        [HttpGet]
        public async Task<TData<EventRecordEntity>> GetFormJson([FromQuery] long id)
        {
            TData<EventRecordEntity> obj = await eventRecordBLL.GetEntity(id);
            return obj;
        }

        /// <summary>
        /// 记录控制操作日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TData<string>> SaveFormJson([FromQuery] EventRecordEntity entity)
        {
           var obj =   await eventRecordBLL.SaveForm(entity);
           return obj;
        }

        #endregion
    }
}