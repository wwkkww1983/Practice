using Caist.Framework.Business.FiberManage;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //TODO:上线时记得去掉权限注释
    //[AuthorizeFilter]
    public class FiberController : ControllerBase
    {
        private FiberBLL fiberBLL = new FiberBLL();

        //#region 获取数据

        /// <summary>
        /// 获取区域光纤温度
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetFiberInfoList([FromQuery] FiberParam param)
        {
            var obj = await fiberBLL.GetFiberInfoList(param);
            return obj.RemoveNullValue();
        }
    }
}