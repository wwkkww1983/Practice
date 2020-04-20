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
    public class SystemSettingController : ControllerBase
    {
        private SystemSettingBLL systemSettingBLL = new SystemSettingBLL();
        #region 获取数据
        [HttpGet]
        public async Task<TData<List<SystemSettingEntity>>> GetListJson([FromQuery] SystemSettingListParam param)
        {
            TData<List<SystemSettingEntity>> obj = await systemSettingBLL.GetList(param);
            return obj;
        }

        [HttpGet]
        public async Task<TData<List<SystemSettingEntity>>> GetPageListJson([FromQuery] SystemSettingListParam param, [FromQuery] Pagination pagination)
        {
            TData<List<SystemSettingEntity>> obj = await systemSettingBLL.GetPageList(param, pagination);
            return obj;
        }

        [HttpGet]
        public async Task<TData<SystemSettingEntity>> GetFormJson([FromQuery] long id)
        {
            TData<SystemSettingEntity> obj = await systemSettingBLL.GetEntity(id);
            return obj;
        }

        #endregion
    }
}