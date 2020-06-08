using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.Pidaihistory;
using Caist.Framework.Model.Param.Pidaihistory;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Pidaihistory
{
   public class PidaiMonitorService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<PidaiMonitorEntity>> GetSecurityInfoList(PidaiMonitorParam param)
        {
            var expression = ListFilter(param);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select top(1000) 
            dict_Id as dictId,dict_Id as dictId,dict_Value as dictValue,create_Time as createTime
            from [dbo].[mk_plc_pidai_values]");
            strSql.Append("where  DATEDIFF(HOUR,getdate(),[create_Time]) <=1");
            var list = await this.BaseRepository().FindList<PidaiMonitorEntity>(strSql.ToString());
            return list.OrderBy(p => p.createTime).ToList();
        }

        #endregion

        #region 私有方法
        private Expression<Func<PidaiMonitorEntity, bool>> ListFilter(PidaiMonitorParam param)
        {
            var expression = LinqExtensions.True<PidaiMonitorEntity>();
            //if (param != null)
            //{
            //}
            return expression;
        }
        #endregion
    }
}



