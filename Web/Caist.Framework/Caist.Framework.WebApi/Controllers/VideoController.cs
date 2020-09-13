using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly VideoBLL videoBLL = new VideoBLL();

        //#region 获取数据
        /// <summary>
        /// 获取所有摄像头地址
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetVideoInfoList([FromQuery] VideoListParam param)
        {
            var obj = await videoBLL.GetList(param);
            return obj.RemoveNullValue();
        }
    }
}