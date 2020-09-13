using Caist.Framework.Business.SystemManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Model.Param.SystemManage;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DataDictController : ControllerBase
    {
        private DataDictDetailBLL dataDictDetailBLL = new DataDictDetailBLL();
        //#region 获取数据
        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetList([FromQuery] DataDictDetailListParam param)
        {
            var obj = await dataDictDetailBLL.GetList(param);
            return obj.RemoveNullValue();
        }
    }
}