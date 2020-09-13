using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.ApplicationManage
{
    public class SystemSettingBLL
    {
        private SystemSettingService systemSettingService = new SystemSettingService();

        #region 获取数据
        public async Task<TData<List<SystemSettingEntity>>> GetList(SystemSettingListParam param)
        {
            TData<List<SystemSettingEntity>> obj = new TData<List<SystemSettingEntity>>();
            obj.Result = await systemSettingService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SystemSettingEntity>>> GetPageList(SystemSettingListParam param, Pagination pagination)
        {
            TData<List<SystemSettingEntity>> obj = new TData<List<SystemSettingEntity>>();
            obj.Result = await systemSettingService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SystemSettingEntity>>> GetPageContentList(SystemSettingListParam param, Pagination pagination)
        {
            TData<List<SystemSettingEntity>> obj = new TData<List<SystemSettingEntity>>();
            obj.Result = await systemSettingService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<SystemSettingEntity>> GetEntity(long id)
        {
            TData<SystemSettingEntity> obj = new TData<SystemSettingEntity>();
            obj.Result = await systemSettingService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await systemSettingService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(SystemSettingEntity entity)
        {
            TData<string> obj = new TData<string>();
            await systemSettingService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await systemSettingService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
