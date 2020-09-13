using Caist.Framework.Entity.PointManage;
using Caist.Framework.Model.Param.PointManage;
using Caist.Framework.Service.PointManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.PointManage
{
    public class DeviceBLL
    {
        private DeviceService  deviceService = new DeviceService();

        #region 获取数据
        public async Task<TData<List<DeviceEntity>>> GetList(DeviceListParam param)
        {
            TData<List<DeviceEntity>> obj = new TData<List<DeviceEntity>>();
            obj.Result = await deviceService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DeviceEntity>>> GetPageList(DeviceListParam param, Pagination pagination)
        {
            TData<List<DeviceEntity>> obj = new TData<List<DeviceEntity>>();
            obj.Result = await deviceService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DeviceEntity>>> GetPageContentList(DeviceListParam param, Pagination pagination)
        {
            TData<List<DeviceEntity>> obj = new TData<List<DeviceEntity>>();
            obj.Result = await deviceService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<DeviceEntity>> GetEntity(long id)
        {
            TData<DeviceEntity> obj = new TData<DeviceEntity>();
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
        public async Task<TData<string>> SaveForm(DeviceEntity entity)
        {
            TData<string> obj = new TData<string>();
            entity.ParentId = string.Format("0");
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

        #region 私有方法
        #endregion
    }
}
