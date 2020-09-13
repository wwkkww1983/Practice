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
    public class MqtSettingBLL
    {
        private MqtSettingService Service = new MqtSettingService();


        #region 获取数据
        public async Task<TData<List<MqtSettingEntity>>> GetList(MqtSettingParam param)
        {
            var obj = new TData<List<MqtSettingEntity>>();
            List<MqtSettingEntity> list = await Service.GetList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<MqtSettingEntity>>> GetPageList(MqtSettingParam param, Pagination pagination)
        {
            TData<List<MqtSettingEntity>> obj = new TData<List<MqtSettingEntity>>();
            obj.Result = await Service.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<MqtSettingEntity>>> GetPageContentList(MqtSettingParam param, Pagination pagination)
        {
            TData<List<MqtSettingEntity>> obj = new TData<List<MqtSettingEntity>>();
            obj.Result = await Service.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<MqtSettingEntity>> GetEntity(long id)
        {
            TData<MqtSettingEntity> obj = new TData<MqtSettingEntity>();
            obj.Result = await Service.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await Service.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(MqtSettingEntity entity)
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
