using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.SettingValueManage;
using Caist.Framework.Model.Param.SettingValueManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.SettingValueManage
{
    public class SettingValueService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<SettingValueEntity>> GetList(SettingValueListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<SettingValueEntity>(strSql.ToString(), filter.ToArray());
            return list.OrderBy(n => n.ParameterSort).ToList();
        }

        public async Task<List<SettingValueEntity>> GetPageList(SettingValueListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<SettingValueEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.OrderBy(n => n.ParameterSort).ToList();
        }

        public async Task<List<SettingValueEntity>> GetPageContentList(SettingValueListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<SettingValueEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<SettingValueEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<SettingValueEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(parameter_sort) FROM mk_setting_value");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(SettingValueEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<SettingValueEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<SettingValueEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<SettingValueEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(SettingValueListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"SELECT a.id as Id
                            ,a.parameter_name as ParameterName
                            ,a.parameter_Ip as ParameterIp
                            ,a.parameter_Port as ParameterPort
                            ,a.parameter_plc_type as ParameterPlcType
                            ,a.parameter_data_type as ParameterDataType
                            ,a.parameter_instructions as ParameterInstructions
                            ,a.parameter_value as ParameterValue
                            ,a.parameter_min_instructions as ParameterMinInstructions
                            ,a.parameter_min_value as ParameterMinValue
                            ,a.parameter_max_instructions as ParameterMaxInstructions
                            ,a.parameter_max_value as ParameterMaxValue
                            ,a.parameter_unit as ParameterUnit
                            ,a.parameter_setting_type as ParameterSettingType
                            ,a.parameter_sort as ParameterSort
                            ,a.parameter_type as ParameterType
							,a.parameter_controls as ParameterControls ");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM mk_setting_value a WHERE a.base_is_delete = 0 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ParameterName))
                {
                    strSql.Append(" AND a.parameter_name like @ParameterName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ParameterName", "%" + param.ParameterName + "%"));
                }
                if (!string.IsNullOrEmpty(param.ParameterIp))
                {
                    strSql.Append(" AND a.parameter_Ip = @ParameterIp");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ParameterIp", param.ParameterIp));
                }
                if (!string.IsNullOrEmpty(param.ParameterSettingType))
                {
                    strSql.Append(" AND a.parameter_setting_type = @ParameterSettingType");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ParameterSettingType", param.ParameterSettingType));
                }
            }
            return parameter;
        }
        #endregion
    }
}
