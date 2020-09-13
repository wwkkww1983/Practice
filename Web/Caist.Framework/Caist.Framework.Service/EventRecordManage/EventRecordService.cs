using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.EventRecordManage;
using Caist.Framework.Model.Param.EventRecordManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.EventRecordManage
{
    public class EventRecordService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<EventRecordEntity>> GetList(EventRecordListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<EventRecordEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<EventRecordEntity>> GetPageList(EventRecordListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<EventRecordEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<EventRecordEntity>> GetPageContentList(EventRecordListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<EventRecordEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<EventRecordEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<EventRecordEntity>(id);
        }

        public async Task<List<EventRecordEntity>> GetExportList(EventRecordListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<EventRecordEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        #endregion

        #region 提交数据
        public async Task SaveForm(EventRecordEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<EventRecordEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<EventRecordEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<EventRecordEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(EventRecordListParam param, StringBuilder strSql)
        {
            strSql.Append(@"SELECT 
                            a.[id],a.[instruct_name] as InstructName,
                            a.[instruct_value] as InstructValue,
                            a.[Operator] Operator,
                            a.[Operator_time] as OperatorTime,
                            a.[Operation_model] as OperationModel,
                            a.system_Id as SystemId,
                            b.real_name as RealName,
                             
                            c.system_name as SystemName
                            FROM [mk_Event_Record] a ");
            strSql.Append(@" left join sys_user b on b.id = a.operator 
                             left join mk_system_setting c on c.id = a.system_Id");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                strSql.Append(" where 1=1 ");
                if (param.InstructName.HasValue())
                {
                    strSql.Append(" AND a.instruct_name=@InstructName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@InstructName",param.InstructName));
                }
                if (param.Operator.HasValue()) //操作人
                {
                    strSql.Append(" AND p.real_name=@Operator");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@Operator", param.Operator));
                }
                if (param.OperationModel.HasValue())
                {
                    strSql.Append(" AND a.Operation_model=@OperationModel");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@OperationModel", param.OperationModel));
                }
                if (param.SystemId.HasValue())
                {
                    strSql.Append(" AND a.system_Id=@SystemId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemId", param.SystemId));
                }
                if (param.StartDate.HasValue)
                {
                    //开始
                    strSql.Append(" AND a.Operator_time >= @StartDate");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@StartDate", param.StartDate));
                }
                if (param.EndDate.HasValue)
                {
                    //结束
                    param.EndDate = (param.EndDate.Value.ToString("yyyy-MM-dd") + " 23:59:59").ParseToDateTime();
                    strSql.Append(" AND a.Operator_time <= @EndTime");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@EndTime", param.EndDate));
                }
            }
            return parameter;
        }
        #endregion
    }
}
