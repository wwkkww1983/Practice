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
    public class SensorBLL
    {
        private SensorService Service = new SensorService();
        #region 获取数据
        public async Task<TData<List<SensorEntity>>> GetList(SensorParam param)
        {
            var obj = new TData<List<SensorEntity>>();
            List<SensorEntity> list = await Service.GetList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SensorEntity>>> GetPageList(SensorParam param, Pagination pagination)
        {
            TData<List<SensorEntity>> obj = new TData<List<SensorEntity>>();
            obj.Result = await Service.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SensorEntity>>> GetPageContentList(SensorParam param, Pagination pagination)
        {
            TData<List<SensorEntity>> obj = new TData<List<SensorEntity>>();
            obj.Result = await Service.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<SensorEntity>> GetEntity(long id)
        {
            TData<SensorEntity> obj = new TData<SensorEntity>();
            obj.Result = await Service.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }


        #endregion


        #region 提交数据
        public async Task<TData<string>> SaveForm(SensorEntity entity)
        {
            TData<string> obj = new TData<string>();
            await Service.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await Service.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
