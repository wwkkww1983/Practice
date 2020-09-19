using Caist.Framework.Cache;
using Caist.Framework.Entity.PeopleManage;
using Caist.Framework.Model.PeopleManage;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Service.PeopleManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.PeopleManage
{
    public class RegionBLL
    {
        private readonly RegionService regionService = new RegionService();
        private readonly PeopleCache peopleCache = new PeopleCache();

        #region 获取数据
        public async Task<TData<List<RegionPeopleNumEntity>>> PersonnelList(RegionParam param)
        {
            var obj = new TData<List<RegionPeopleNumEntity>>();
            var sql = await peopleCache.GetSQL();
            var list = await regionService.PersonnelList(param, sql);
            obj.Result = list;
            obj.TotalCount = list.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RegionPeopleNumEntity>>> PeopleCounting(RegionParam param)
        {
            var obj = new TData<List<RegionPeopleNumEntity>>();
            var list = await regionService.PeopleCounting(param);
            obj.Result = list;
            obj.TotalCount = list.Count;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<RegionPeopleNumEntity>>> GetPeopleInfo(RegionParam param)
        {
            TData<List<RegionPeopleNumEntity>> obj = new TData<List<RegionPeopleNumEntity>>();
            var list = await regionService.GetPeopleInfo(param);
            obj.Result = list;
            obj.TotalCount = list.Count;
            obj.Tag = 1;
            return obj;
        }
        //public async Task<TData<List<PeopleInfoEntity>>> PeopleInfo(RegionParam param)
        //{
        //    TData<List<PeopleInfoEntity>> obj = new TData<List<PeopleInfoEntity>>();
        //    var list = await regionService.PeopleInfo(param);
        //    obj.Result = list;
        //    obj.Tag = 1;
        //    return obj;
        //}
        /// <summary>
        /// 当天人员工作活动区域占比数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TData<List<PeopleArea>>> GetPeopleArea()
        {
            var obj = new TData<List<PeopleArea>>();
            var list = await regionService.GetPeopleArea();
            obj.Result = list;
            obj.TotalCount = list.Count;
            obj.Tag = 1;
            return obj;
        }

        /// <summary>
        /// 从历史表中获取最新人员定位数据
        /// </summary>
        /// <returns></returns>
        public async Task<TData<List<PublicPeopleRealTime>>> GetPeopleRealTime()
        {
            var obj = new TData<List<PublicPeopleRealTime>>();
            var list = await regionService.GetPeopleRealTime();
            obj.Result = list;
            obj.TotalCount = list.Count;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<RegionEntity>> GetEntity(long id)
        {
            TData<RegionEntity> obj = new TData<RegionEntity>();
            obj.Result = await regionService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(RegionEntity entity)
        {
            TData<string> obj = new TData<string>();
            await regionService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region 私有方法
        private List<RegionEntity> ListFilter(RegionParam param, List<RegionEntity> list)
        {
            //if (param != null)
            //{
            //    if (!string.IsNullOrEmpty(param.MqHost))
            //    {
            //        list = list.Where(p => p.MqHost.Contains(param.MqHost)).ToList();
            //    }
            //    if (!string.IsNullOrEmpty(param.MqUser))
            //    {
            //        list = list.Where(p => p.MqUser == param.MqUser).ToList();
            //    }
            //}
            return list;
        }
        #endregion
    }
}
