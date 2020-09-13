using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ViewFunctionController : ControllerBase
    {
        private ViewFunctionBLL viewFunctionBLL = new ViewFunctionBLL();

        #region 获取数据
        [HttpGet]
        public async Task<TData<List<ViewFunctionEntity>>> GetListJson([FromQuery] ViewFunctionListParam param)
        {
            TData<List<ViewFunctionEntity>> obj = await viewFunctionBLL.GetList(param);
            return obj;
        }

        [HttpGet]
        public async Task<TData<List<ViewFunctionEntity>>> GetPageListJson([FromQuery] ViewFunctionListParam param, [FromQuery] Pagination pagination)
        {
            TData<List<ViewFunctionEntity>> obj = await viewFunctionBLL.GetPageContentList(param, pagination);
            return obj;
        }

        [HttpGet]
        public async Task<TData<ViewFunctionEntity>> GetFormJson([FromQuery] long id)
        {
            TData<ViewFunctionEntity> obj = await viewFunctionBLL.GetEntity(id);
            return obj;
        }

        #endregion
    }
}