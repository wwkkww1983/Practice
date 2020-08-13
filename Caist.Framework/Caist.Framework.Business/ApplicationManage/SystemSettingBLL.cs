using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using Caist.Framework.Util;
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
            if (obj.Result != null)
            {
                obj.Result.ForEach(n => {
                    if (!n.SystemImage.Contains("http"))
                    {
                        n.SystemImage = GlobalContext.SystemConfig.WebUrI + n.SystemImage;
                    }
                    if (n.SystemModel!=null && !n.SystemModel.Contains("http"))
                    {
                        n.SystemModel = GlobalContext.SystemConfig.WebUrI + n.SystemModel;
                    }
                });
            }
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SystemSettingEntity>>> GetPageList(SystemSettingListParam param, Pagination pagination)
        {
            TData<List<SystemSettingEntity>> obj = new TData<List<SystemSettingEntity>>();
            obj.Result = await systemSettingService.GetPageList(param, pagination);
            if (obj.Result!=null)
            {
                obj.Result.ForEach(n => {
                    if (!n.SystemImage.Contains("http"))
                    {
                        n.SystemImage = GlobalContext.SystemConfig.WebUrI + n.SystemImage;
                    }
                    if (n.SystemModel != null && !n.SystemModel.Contains("http"))
                    {
                       n.SystemModel = GlobalContext.SystemConfig.WebUrI + n.SystemModel;
                    }
                });
            }
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
            if (obj.Result!=null)
            {
                if (!obj.Result.SystemImage.Contains("http"))
                {
                    obj.Result.SystemImage = GlobalContext.SystemConfig.WebUrI + obj.Result.SystemImage;
                   
                }
                if (obj.Result.SystemModel != null && !obj.Result.SystemModel.Contains("http"))
                {
                    obj.Result.SystemModel = GlobalContext.SystemConfig.WebUrI + obj.Result.SystemModel;
                }
            }
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
