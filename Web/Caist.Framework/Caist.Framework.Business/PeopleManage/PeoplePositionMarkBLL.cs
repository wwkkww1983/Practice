using Caist.Framework.Cache;
using Caist.Framework.Entity.PeopleManage;
using Caist.Framework.Model.PeopleManage;
using Caist.Framework.Service.PeopleManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.PeopleManage
{
    public class PeoplePositionMarkBLL
    {
        private readonly PeoplePositionMarkService Service = new PeoplePositionMarkService();

        #region 获取数据
        public async Task<TData<List<PeoplePositionMarkEntity>>> GetList(RegionParam param)
        {
            var obj = new TData<List<PeoplePositionMarkEntity>>();
        
            var list = await Service.GetList(param);
            obj.Result = list;
            obj.TotalCount = list.Count;
            obj.Tag = 1;
            return obj;
        }

      

        public async Task<TData<PeoplePositionMarkEntity>> GetEntity(long id)
        {
            TData<PeoplePositionMarkEntity> obj = new TData<PeoplePositionMarkEntity>();
            obj.Result = await Service.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<PeoplePositionMarkEntity>> GetEntity(string i)
        {
            TData<PeoplePositionMarkEntity> obj = new TData<PeoplePositionMarkEntity>();
            obj.Result = await Service.GetEntity(i);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(PeoplePositionMarkEntity entity)
        {
            TData<string> obj = new TData<string>();
            await Service.SaveForm(entity);
            obj.Result = entity.i.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        #endregion

    }
}
