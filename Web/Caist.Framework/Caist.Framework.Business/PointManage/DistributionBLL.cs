using Caist.Framework.Entity.PointManage;
using Caist.Framework.Model.Param.PointManage;
using Caist.Framework.Service.PointManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.PointManage
{
    public class DistributionBLL
    {
        private DistributionService deviceService = new DistributionService();

        #region 获取数据
        public async Task<TData<List<DistributionEntity>>> GetList(DistributionParam param)
        {
            TData<List<DistributionEntity>> obj = new TData<List<DistributionEntity>>();
            obj.Result = await deviceService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DistributionEntity>>> GetPageList(DistributionParam param, Pagination pagination)
        {
            TData<List<DistributionEntity>> obj = new TData<List<DistributionEntity>>();
            obj.Result = await deviceService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DistributionEntity>>> GetPageContentList(DistributionParam param, Pagination pagination)
        {
            TData<List<DistributionEntity>> obj = new TData<List<DistributionEntity>>();
            obj.Result = await deviceService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<DistributionEntity>> GetEntity(long id)
        {
            TData<DistributionEntity> obj = new TData<DistributionEntity>();
            obj.Result = await deviceService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await deviceService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(DistributionEntity entity)
        {
            TData<string> obj = new TData<string>();
            await deviceService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await deviceService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}