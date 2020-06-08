using Caist.Framework.Business.Security;
using Caist.Framework.Model.Security;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SecurityMonitorController : ControllerBase
    {
        private readonly SecurityMonitorBLL securityMonitorBLL = new SecurityMonitorBLL();

        //#region 获取数据

        /// <summary>
        /// 获取安全检测列表
        /// </summary>
        /// <param name="param">SecurityMonitorParam</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityInfoList([FromQuery] SecurityMonitorParam param)
        {
            var obj = await securityMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }

    }
}