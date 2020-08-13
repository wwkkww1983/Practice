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
    public class MqThemeController : ControllerBase
    {
        private MqThemeBLL mqThemeBLL = new MqThemeBLL();

        #region 获取数据

        [HttpGet]
        public async Task<TData<List<MqThemeEntity>>> GetListJson([FromQuery] MqThemeListParam param)
        {
            TData<List<MqThemeEntity>> obj = await mqThemeBLL.GetList(param);
            return obj;
        }

        [HttpGet]
        public async Task<TData<List<MqThemeEntity>>> GetPageListJson([FromQuery] MqThemeListParam param, [FromQuery] Pagination pagination)
        {
            TData<List<MqThemeEntity>> obj = await mqThemeBLL.GetPageList(param, pagination);
            return obj;
        }

        [HttpGet]
        public async Task<TData<MqThemeEntity>> GetFormJson([FromQuery] long id)
        {
            TData<MqThemeEntity> obj = await mqThemeBLL.GetEntity(id);
            return obj;
        }

        #endregion
    }
}