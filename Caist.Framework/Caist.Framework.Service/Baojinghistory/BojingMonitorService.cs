using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.Baojinghistory;
using Caist.Framework.Model.Param.Baojinghistory;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Baojinghistory
{
   public class BojingMonitorService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<BojingMonitorEntity>> GetSecurityInfoList(BojingMonitorParam param)
        {
            var expression = ListFilter(param);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select count(tongji.system_name) as Value,tongji.system_name as Name from 
           (select mk_alarm_record.alarm_id,mk_alarm_record.alarm_reason,mk_alarm_settings.system_models,
           mk_alarm_settings.broadcast_content,mk_system_setting.system_name from mk_alarm_record left join mk_alarm_settings
           on mk_alarm_record.alarm_id = mk_alarm_settings.id 
           left join  mk_system_setting on mk_alarm_settings.system_models =  mk_system_setting.id) tongji group by tongji.system_name");

            var list = await this.BaseRepository().FindList<BojingMonitorEntity>(strSql.ToString());

            return list.OrderBy(p => p.Value).ToList();
        }

        #endregion

        #region 私有方法
        private Expression<Func<BojingMonitorEntity, bool>> ListFilter(BojingMonitorParam param)
        {
            var expression = LinqExtensions.True<BojingMonitorEntity>();
            return expression;
        }
        #endregion
    }
}
