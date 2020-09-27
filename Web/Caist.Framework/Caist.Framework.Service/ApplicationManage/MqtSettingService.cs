using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.ApplicationManage
{
    public class MqtSettingService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<MqtSettingEntity>> GetList(MqtSettingParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<MqtSettingEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<MqtSettingEntity>> GetPageList(MqtSettingParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<MqtSettingEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<MqtSettingEntity>> GetPageContentList(MqtSettingParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<MqtSettingEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<MqtSettingEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<MqtSettingEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(mq_stutas) FROM mk_mq_theme");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(MqtSettingEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<MqtSettingEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<MqtSettingEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<MqtSettingEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(MqtSettingParam param, StringBuilder strSql, bool bMqThemeContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.system_id as SystemId,
                                    mk_system_setting.system_nick_name as SystemName,
                                    a.code_type as CodeType,
                                    b.name as CodeName,
                                    a.code_point_setting as CodePointSetting,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.system_code as SystemCode,
                                    a.address_are_code as AddressAreCode,
                                    a.address_type_code as AddressTypeCode,
                                    a.address_device_code as AddressDeviceCode,
                                    a.device_code as DeviceCode,
                                    a.sensor_type_code as SensorTypeCode,
                                    a.value_length as ValueLength,
                                    a.decimal_places as DecimalPlaces,
                                    a.alarm_point as AlarmPoint,
                                    c.name as AddressTypeName 
                                    ");

            
            strSql.Append(@" FROM  mk_mqtt_code_setting a  ");
            strSql.Append(@" left join mk_system_setting on a.system_id = mk_system_setting.id ");
            strSql.Append(@" left join mk_mqtt_sensor_setting b on a.sensor_id = b.id ");
            strSql.Append(@" left join mk_mqtt_address_type c on a.address_type_code = c.code ");
            
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                strSql.Append(@" WHERE   a.base_is_delete = 0 ");
                if (!string.IsNullOrEmpty(param.CodeName)) //名称提供模糊匹配
                {

                    strSql.Append(" AND p.code_name like @CodeName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@CodeName", "%" + param.CodeName + "%"));

                }
                if (!string.IsNullOrEmpty(param.CodePointSetting))
                {
                    strSql.Append(" AND a.code_point_setting=@CodePointSetting");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@CodePointSetting", param.CodePointSetting));
                }
                if (!string.IsNullOrEmpty(param.SystemId))
                {
                    strSql.Append(" AND a.system_id=@SystemId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemId", param.SystemId));
                }
            }
            return parameter;
        }
        #endregion
    }
}
