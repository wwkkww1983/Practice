using Caist.Framework.Entity.EventRecordManage;
using Caist.Framework.Model.Param.EventRecordManage;
using Caist.Framework.Service.EventRecordManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.EventRecordManage
{
    public class EventRecordBLL
    {
        private readonly EventRecordService eventService = new EventRecordService();

        #region 获取数据
        public async Task<TData<List<EventRecordEntity>>> GetList(EventRecordListParam param)
        {
            TData<List<EventRecordEntity>> obj = new TData<List<EventRecordEntity>>();
            obj.Result = await eventService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<EventRecordEntity>>> GetPageList(EventRecordListParam param, Pagination pagination)
        {
            TData<List<EventRecordEntity>> obj = new TData<List<EventRecordEntity>>();
            obj.Result = await eventService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<EventRecordEntity>>> GetPageContentList(EventRecordListParam param, Pagination pagination)
        {
            TData<List<EventRecordEntity>> obj = new TData<List<EventRecordEntity>>();
            obj.Result = await eventService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<EventRecordEntity>> GetEntity(long id)
        {
            TData<EventRecordEntity> obj = new TData<EventRecordEntity>();
            obj.Result = await eventService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(EventRecordEntity entity)
        {
            TData<string> obj = new TData<string>();
            await eventService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await eventService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
