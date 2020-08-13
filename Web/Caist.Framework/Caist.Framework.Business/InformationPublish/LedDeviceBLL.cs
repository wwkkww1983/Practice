using Caist.Framework.Cache;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Business.ApplicationManage
{
    public class LedDeviceBLL
    {
        private LedDeviceService ledDeviceService = new LedDeviceService();

        #region 获取数据
        public async Task<TData<List<LedDeviceEntity>>> GetList(LedDeviceParam param)
        {
            var obj = new TData<List<LedDeviceEntity>>();
            List<LedDeviceEntity> list = await ledDeviceService.GetList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LedDeviceEntity>>> GetPageList(LedDeviceParam param, Pagination pagination)
        {
            TData<List<LedDeviceEntity>> obj = new TData<List<LedDeviceEntity>>();
            obj.Result = await ledDeviceService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

       
        public async Task<TData<LedDeviceEntity>> GetEntity(long id)
        {
            TData<LedDeviceEntity> obj = new TData<LedDeviceEntity>();
            obj.Result = await ledDeviceService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await ledDeviceService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(LedDeviceEntity entity)
        {
            TData<string> obj = new TData<string>();
            await ledDeviceService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await ledDeviceService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
