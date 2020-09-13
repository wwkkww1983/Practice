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
    public class FiberMarkBLL
    {
        private readonly FiberMarkService Service = new FiberMarkService();

        #region 获取数据
        public async Task<TData<List<FiberMark>>> GetList()
        {
            var obj = new TData<List<FiberMark>>();
        
            var list = await Service.GetList();
            obj.Result = list;
            obj.TotalCount = list.Count;
            obj.Tag = 1;
            return obj;
        }

      

        public async Task<TData<FiberMark>> GetEntity(long id)
        {
            TData<FiberMark> obj = new TData<FiberMark>();
            obj.Result = await Service.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<FiberMark>> GetEntity(string i)
        {
            TData<FiberMark> obj = new TData<FiberMark>();
            obj.Result = await Service.GetEntity(i);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(FiberMark entity)
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
