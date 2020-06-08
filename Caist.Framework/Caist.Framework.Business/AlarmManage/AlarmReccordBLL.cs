using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Model.Param.AlarmManage;
using Caist.Framework.Service.AlarmManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.AlarmManage
{
    public class AlarmReccordBLL
    {
        private AlarmReccordService reccordService = new AlarmReccordService();

        #region 获取数据
        public async Task<TData<List<AlarmRecordEntity>>> GetList(ALarmReccordListParam param)
        {
            TData<List<AlarmRecordEntity>> obj = new TData<List<AlarmRecordEntity>>();
            obj.Result = await reccordService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AlarmRecordEntity>>> GetPageList(ALarmReccordListParam param, Pagination pagination)
        {
            TData<List<AlarmRecordEntity>> obj = new TData<List<AlarmRecordEntity>>();
            obj.Result = await reccordService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AlarmRecordEntity>>> GetPageContentList(ALarmReccordListParam param, Pagination pagination)
        {
            TData<List<AlarmRecordEntity>> obj = new TData<List<AlarmRecordEntity>>();
            obj.Result = await reccordService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(AlarmRecordEditEntity entity)
        {
            TData<string> obj = new TData<string>();
            await reccordService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await reccordService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
