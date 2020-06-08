using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.yafenghistory;
using Caist.Framework.Model.yafenghistory;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.yafenghistory
{
  public class yafengMonitorService: RepositoryFactory
    {
        #region 获取数据
        public  async Task<List<yafengMonitorEntity>> GetSecurityInfoList(yafengMonitorParam param)
        {
            var expression = ListFilter(param);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select top(1000) 
            dict_Id as dictId,dict_Id as dictId,dict_Value as dictValue,create_Time as createTime
            from [dbo].[mk_plc_yafeng_values]");
            strSql.Append("where  DATEDIFF(HOUR,getdate(),[create_Time]) <=1");
            var list = await this.BaseRepository().FindList<yafengMonitorEntity>(strSql.ToString());
            return list.OrderBy(p => p.createTime).ToList();
        }

        #endregion

        #region 私有方法
        private Expression<Func<yafengMonitorEntity, bool>> ListFilter(yafengMonitorParam param)
        {
            var expression = LinqExtensions.True<yafengMonitorEntity>();
            //if (param != null)
            //{
            //}
            return expression;
        }
        #endregion
    }
}
