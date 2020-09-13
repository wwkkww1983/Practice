using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Model.Param.AlarmManage;
using Caist.Framework.Service.AlarmManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Business.AlarmManage
{
    public class AlarmSettingBLL
    {
        private AlarmSettingService AlarmSettingService = new AlarmSettingService();


        #region 获取数据
        public async Task<TData<List<AlarmSettingEntity>>> GetList(AlarmSettingListParam param)
        {
            TData<List<AlarmSettingEntity>> obj = new TData<List<AlarmSettingEntity>>();
            obj.Result = await AlarmSettingService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AlarmSettingEntity>>> GetPageList(AlarmSettingListParam param, Pagination pagination)
        {
            TData<List<AlarmSettingEntity>> obj = new TData<List<AlarmSettingEntity>>();
            obj.Result = await AlarmSettingService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AlarmSettingEntity>>> GetPageContentList(AlarmSettingListParam param, Pagination pagination)
        {
            TData<List<AlarmSettingEntity>> obj = new TData<List<AlarmSettingEntity>>();
            obj.Result = await AlarmSettingService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AlarmSettingEntity>> GetEntity(long id)
        {
            TData<AlarmSettingEntity> obj = new TData<AlarmSettingEntity>();
            obj.Result = await AlarmSettingService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(AlarmSettingEntity entity)
        {
            TData<string> obj = new TData<string>();
            await AlarmSettingService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await AlarmSettingService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        private List<AlarmSettingEntity> ListFilter(AlarmSettingListParam param, List<AlarmSettingEntity> list)
        {
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ManipulateModelName))
                {
                    list = list.Where(p => p.ManipulateModelName.Contains(param.ManipulateModelName)).ToList();
                }
                if (!string.IsNullOrEmpty(param.ViewManipulateId.ToString()))
                {
                    list = list.Where(p => p.ViewManipulateId == param.ViewManipulateId).ToList();
                }
            }
            return list;
        }
        #endregion
    }
}
