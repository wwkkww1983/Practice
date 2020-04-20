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

        ///// <summary>
        ///// 各分区信息 及 分区报警
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<TData<List<string>>> SectionInfo([FromQuery] FiberParam param)
        //{
        //    var obj = await regionBLL.GetPeopleTrackList(param);
        //    return obj;
        //}

        ///// <summary>
        ///// 人员位置信息
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<TData<List<SimplePositionEntity>>> SimelPeoplePoition([FromBody] RegionParam param)
        //{
        //    var obj = await regionBLL.SimelPeoplePoition(param);
        //    return obj;
        //}


        ///// <summary>
        ///// 人员信息
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<TData<List<PeopleInfoEntity>>> GetPeopleInfo([FromBody] RegionParam param)
        //{
        //    var obj = await regionBLL.GetPeopleInfo(param);
        //    return obj;
        //}
        ///// <summary>
        ///// 区域人员汇总信息
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<TData<List<PeopleInfoEntity>>> PeopleInfo([FromBody] RegionParam param)
        //{
        //    var obj = await regionBLL.PeopleInfo(param);
        //    return obj;
        //}
        //#endregion
    }
}