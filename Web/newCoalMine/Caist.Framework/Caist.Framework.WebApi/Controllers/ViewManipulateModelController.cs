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
    public class ViewManipulateModelController : ControllerBase
    {
        private ViewManipulateModelBLL viewManipulateModelBLL = new ViewManipulateModelBLL();

        #region 获取数据
        [HttpGet]
        public async Task<TData<List<ViewManipulateModelEntity>>> GetListJson([FromQuery] ViewManipulateModelListParam param)
        {
            TData<List<ViewManipulateModelEntity>> obj = await viewManipulateModelBLL.GetList(param);
            return obj;
        }

        [HttpGet]
        public async Task<TData<List<ViewManipulateModelEntity>>> GetPageListJson([FromQuery] ViewManipulateModelListParam param, [FromQuery] Pagination pagination)
        {
            TData<List<ViewManipulateModelEntity>> obj = await viewManipulateModelBLL.GetPageList(param, pagination);
            return obj;
        }

        [HttpGet]
        public async Task<TData<ViewManipulateModelEntity>> GetFormJson([FromQuery] long id)
        {
            TData<ViewManipulateModelEntity> obj = await viewManipulateModelBLL.GetEntity(id);
            return obj;
        }

        #endregion
    }
}