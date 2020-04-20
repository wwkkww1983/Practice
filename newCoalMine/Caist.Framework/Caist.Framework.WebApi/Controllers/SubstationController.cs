using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //[AuthorizeFilter]
    public class SubstationController : ControllerBase
    {
        //private RegionBLL regionBLL = new RegionBLL();

        //#region 获取数据

        ///// <summary>
        ///// 主页面位置信息
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<TData<List<RegionPeopleNumEntity>>> PositionInfo([FromQuery] RegionParam param)
        //{
        //    var obj = await regionBLL.GetList(param);
        //    return obj;
        //}

        ///// <summary>
        ///// 人员轨迹信息
        ///// </summary>
        ///// <param name="param"></param>
        ///// <param name="pagination"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<TData<List<string>>> GetPeopleLocus([FromBody] RegionParam param)
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