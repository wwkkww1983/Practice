using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.EventRecordManage;
using Caist.Framework.Entity.PointManage;
using Caist.Framework.Model.Param.EventRecordManage;
using Caist.Framework.Model.Param.OrganizationManage;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business
{
   public class SystemDataService: RepositoryFactory
    {

        #region 获取数据

        /// <summary>
        /// 根据系统ID获取系统历史数据表名
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TData<List<DeviceEntity>>> GetSystemTableNameList(SystemDataParam param)
        {
            TData<List<DeviceEntity>> obj = new TData<List<DeviceEntity>>();
            var strSql = new StringBuilder();
            strSql.Append(@"select tab_name as TabName  from mk_device where 1=1");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(param.SystemId.ParseToString()))
            {
                strSql.Append(" AND system_id=@SystemId");
                parameter.Add(DbParameterExtension.CreateDbParameter("@SystemId", param.SystemId));
            }

            var data = await this.BaseRepository().FindList<DeviceEntity>(strSql.ToString(), parameter.ToArray());
            obj.Result = data.ToList();
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        /// <summary>
        /// 获取指定系统的数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TData<List<SystemDataEntity>>> GetSystemDataList(SystemDataParam param)
        {

            TData<List<SystemDataEntity>> obj = new TData<List<SystemDataEntity>>();
            var strSql = new StringBuilder();
            strSql.Append(string.Format(@"select a.dict_Id as DictId,
                                a.dict_Value as DictValue,
                                a.create_Time as CreateTime,
                                b.manipulate_model_name as DictName,
                                d.system_name as SystemName
                        from {0} a
                        left join mk_view_manipulate_model b on a.dict_id  = b.manipulate_model_mark
                        left join mk_view_function c on b.view_function_id = c.id
                        left join mk_system_setting d on c.system_setting_id = d.id where Instruct_type = -1 ", param.TabName));
            var parameter = new List<DbParameter>();
            if (param!=null)
            {
                
                if (!string.IsNullOrEmpty(param.DictId.ParseToString()))
                {
                    strSql.Append(" AND a.dict_Id =@DictId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@DictId", param.DictId));
                }

                if (param.StartDate.HasValue)
                {
                    strSql.Append(" AND a.create_Time >=@StartDate");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@StartDate", param.StartDate));
                }
                if (param.EndDate.HasValue)
                {
                    strSql.Append(" AND a.create_time<=@EndDate");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@EndDate", param.EndDate));
                }
            }
            var data = await this.BaseRepository().FindList<SystemDataEntity>(strSql.ToString(), parameter.ToArray());
            obj.Result = data.ToList();
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region
        /// <summary>
        /// 通风能耗平均值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<double> GetTFEnergy()
        {
            var sql = new StringBuilder();
            sql.Append(string.Format(@"
					select sum(value) value from(
						select exp(sum(log(value))) value from ( select   avg(CAST(dict_Value AS decimal)) value from mk_plc_jushan_values  where dict_Value<>'' and
						(dict_id='192.168.20.11-DB16.DBD0' or dict_Id='192.168.20.11-DB16.DBD8'
						or dict_id='192.168.20.1-DB16.DBD0' or dict_Id='192.168.20.1-DB16.DBD8')
						union all
						select  avg(CAST(dict_Value AS decimal)) as value  from mk_plc_jushan_values  where dict_Value<>'' and
						(dict_id='192.168.20.11-DB16.DBD4' or dict_Id='192.168.20.11-DB16.DBD12'
						or dict_id='192.168.20.1-DB16.DBD4' or dict_Id='192.168.20.1-DB16.DBD12') ) as data
					union all
						select exp(sum(log(value))) value from ( select   avg(CAST(dict_Value AS decimal)) value from mk_plc_tongfeng_values  where  dict_Value<>'' and
						(dict_id='192.168.20.16-DB30.DBD20' or dict_Id='192.168.20.16-DB31.DBD20'
						or dict_id='192.168.20.18-DB30.DBD20' or dict_Id='192.168.20.18-DB31.DBD20')
						union all
						select  avg(CAST(dict_Value AS decimal)) as value  from mk_plc_jushan_values  where  dict_Value<>'' and
						(dict_id='192.168.20.16-DB30.DBD28' or dict_Id='192.168.20.16-DB31.DBD28'
						or 	dict_id='192.168.20.18-DB30.DBD28' or dict_Id='192.168.20.18-DB31.DBD28')) as data) tb"));

           var  data =  await this.BaseRepository().FindObject<Energy>(sql.ToString());

            return data != null ? data.value.HasValue ? data.value.Value : 0 : 0;
        }
        /// <summary>
        /// 压风能耗平均值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<double> GetYFEnergy()
        {

            var sql = new StringBuilder();
            sql.Append(string.Format(@"
						select exp(sum(log(value))) value from ( 
							select   avg(CAST(dict_Value AS decimal)) value from mk_plc_yafeng_values  where 
							dict_Id='192.168.20.20-DB4.DBD36' or dict_Id='192.168.20.20-DB4.DBD36'
							union all
							select  avg(CAST(dict_Value AS decimal)) as value  from mk_plc_yafeng_values  where 
							dict_id='192.168.20.20-DB16.DBD348' or dict_Id='192.168.20.20-DB16.DBD408') tb"));

            var data = await this.BaseRepository().FindObject<Energy>(sql.ToString());

            return data != null ? data.value.HasValue ? data.value.Value : 0 : 0;
        }
        /// <summary>
        /// 皮带能耗平均值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<double> GetPDEnergy()
        {

            var sql = new StringBuilder();
            sql.Append(string.Format(@"
						select exp(sum(log(value))) value from ( 
							select   avg(CAST(dict_Value AS decimal)) value from mk_plc_yafeng_values  where 
							dict_Id='192.168.20.21-DB17.DB4' or dict_Id='192.168.20.22-DB17.DBD4'
							or dict_Id='192.168.20.23-DB17.DBD4' or dict_Id='192.168.20.23-DB18.DBD4'
						union all
							select  avg(CAST(dict_Value AS decimal)) as value  from mk_plc_yafeng_values  where 
								dict_Id='192.168.20.21-DB17.DB0' or dict_Id='192.168.20.22-DB17.DBD0'
							or dict_Id='192.168.20.23-DB17.DBD0' or dict_Id='192.168.20.23-DB18.DBD0') tb"));

            var data = await this.BaseRepository().FindObject<Energy>(sql.ToString());

            return data != null ? data.value.HasValue ? data.value.Value : 0 : 0;
        }
        #endregion
    }
}
