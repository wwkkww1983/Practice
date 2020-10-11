using Caist.Framework.Business.PeopleManage;
using Caist.Framework.Entity.PeopleManage;
using Caist.Framework.Model.PeopleManage;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Caist.Framework.WebApi.Handle;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //TODO:上线时记得去掉权限注释
    [AuthorizeFilter]
    public class PeoplePositioningController : ControllerBase
    {
        private RegionBLL regionBLL = new RegionBLL();
        private PeoplePositionMarkBLL markBll = new PeoplePositionMarkBLL();

        #region 获取数据

        /// <summary>
        /// 获取人员定位标记坐标信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetMarkList([FromQuery] RegionParam param)
        {
            var obj = await markBll.GetList(param);
            return obj.RemoveNullValue();
        }


        /// <summary>
        /// 根据点位获取实时人员列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> PersonnelList([FromQuery] RegionParam param)
        {
            var obj = await regionBLL.PersonnelList(param);
            return obj.RemoveNullValue();
        }

        /// <summary>
        /// 相应点位的人数统计
        /// </summary>
        /// <param name="param"></param>
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
        /// <summary>
        /// 当天人员工作活动区域占比数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> PeopleArea()
        {
            var obj = await regionBLL.GetPeopleArea();

            return obj.RemoveNullValue();
        }

        #endregion

        #region 提交数据
        [HttpPost]
        public async Task<TData<string>> SaveFormJson([FromQuery] PeoplePositionMarkEntity entity)
        {
            if (!entity.Id.HasValue)
            {
                TData<string> data = new TData<string>();
                data.Message = "提交数据失败：ID不能为空";
                data.Tag = 0;
                return data;
            }
            var obj = await markBll.SaveForm(entity);
            return obj;
        }

        #endregion
    }
}