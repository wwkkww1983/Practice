using Caist.Framework.WebApi.Handle;
using Caist.Framework.Business.Security;
using Caist.Framework.Entity.SubStation;
using Caist.Framework.Model.SubStation;
using Caist.Framework.Util.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [AuthorizeFilter]
    public class SubstationController : ControllerBase
    {
        private SubStationBLL substationBLL = new SubStationBLL();

        /// <summary>
        /// 获取供电站信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TData<List<SubStationEntity>>> SubStationInfo([FromQuery] SubStationParam param)
        {
            var obj = await substationBLL.GetSubStationInfoList(param);
            return obj;
        }
    }
}