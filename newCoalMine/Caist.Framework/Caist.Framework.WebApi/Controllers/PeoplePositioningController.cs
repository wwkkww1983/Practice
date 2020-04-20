using Caist.Framework.Business.PeopleManage;
using Caist.Framework.Model.PeopleManage;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //TODO:上线时记得去掉权限注释
    //[AuthorizeFilter]
    public class PeoplePositioningController : ControllerBase
    {
        private RegionBLL regionBLL = new RegionBLL();

        #region 获取数据

        /// <summary>
        /// 根据点位获取实时人员列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> PersonnelList([FromBody] RegionParam param)
        {
            var obj = await regionBLL.PersonnelList(param);
            return obj.RemoveNullValue();
        }

        /// <summary>
        /// 相应点位的人数统计
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> PeopleCounting([FromBody] RegionParam param)
        {
            var obj = await regionBLL.PeopleCounting(param);
            return obj.RemoveNullValue();
        }


        /// <summary>
        /// 人员信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> GetPeopleInfo([FromBody] RegionParam param)
        {
            var obj = await regionBLL.GetPeopleInfo(param);
            return obj.RemoveNullValue();
        }
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
        #endregion
    }
}