using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ViewParamenterController : ControllerBase
    {
        private ViewParamenterBLL viewParamenterBLL = new ViewParamenterBLL();


        #region 获取数据
        [HttpGet]
        public async Task<TData<List<ViewParamenterEntity>>> GetListJson([FromQuery] ViewParamenterListParam param)
        {
            TData<List<ViewParamenterEntity>> obj = await viewParamenterBLL.GetList(param);
            return obj;
        }

        [HttpGet]
        public async Task<TData<List<ViewParamenterEntity>>> GetPageListJson([FromQuery] ViewParamenterListParam param, [FromQuery] Pagination pagination)
        {
            TData<List<ViewParamenterEntity>> obj = await viewParamenterBLL.GetPageList(param, pagination);
            return obj;
        }

        [HttpGet]
        public async Task<TData<ViewParamenterEntity>> GetFormJson([FromQuery] long id)
        {
            TData<ViewParamenterEntity> obj = await viewParamenterBLL.GetEntity(id);
            return obj;
        }

        #endregion
    }
}