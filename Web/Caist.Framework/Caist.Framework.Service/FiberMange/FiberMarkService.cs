using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.PeopleManage;
using Caist.Framework.Model.PeopleManage;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.PeopleManage
{
    public class FiberMarkService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<FiberMark>> GetList()
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(strSql);
            var list = await this.BaseRepository().FindList<FiberMark>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }
        public async Task<FiberMark> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<FiberMark>(id);
        }
        public async Task<FiberMark> GetEntity(string i)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  a.id as Id,
                                    a.i,
                                    a.x,
                                    a.y,
                                    a.w,
                                    a.h ");
            strSql.Append(@" FROM  mk_fiber_mark a  ");
            strSql.AppendFormat(@" where i='{0}'", i);
            return await this.BaseRepository().FindObject<FiberMark>(strSql.ToString());
        }


        #endregion

        #region 提交数据
        public async Task SaveForm(FiberMark entity)
        {

            await entity.Modify();
            await this.BaseRepository().Update<FiberMark>(entity);
          
        }

        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(StringBuilder strSql)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.i,
                                    a.x,
                                    a.y,
                                    a.w,
                                    a.h ");
            strSql.Append(@" FROM  mk_fiber_mark a  ");
            var parameter = new List<DbParameter>();
        
            return parameter;
        }
        #endregion
    }

}
