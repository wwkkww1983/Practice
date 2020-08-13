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
    public class ViewControlModelController : ControllerBase
    {
        private ViewControlModelBLL viewControlModelBLL = new ViewControlModelBLL();

        #region 获取数据
        [HttpGet]
        public async Task<TData<List<ViewControlModelEntity>>> GetListJson([FromQuery] ViewControlModelListParam param)
        {
            TData<List<ViewControlModelEntity>> obj = await viewControlModelBLL.GetList(param);
            return obj;
        }

        [HttpGet]
        public async Task<TData<List<ViewControlModelEntity>>> GetPageListJson([FromQuery] ViewControlModelListParam param, [FromQuery] Pagination pagination)
        {
            TData<List<ViewControlModelEntity>> obj = await viewControlModelBLL.GetPageList(param, pagination);
            return obj;
        }

        [HttpGet]
        public async Task<TData<ViewControlModelEntity>> GetFormJson([FromQuery] long id)
        {
            TData<ViewControlModelEntity> obj = await viewControlModelBLL.GetEntity(id);
            return obj;
        }

        #endregion
    }
}