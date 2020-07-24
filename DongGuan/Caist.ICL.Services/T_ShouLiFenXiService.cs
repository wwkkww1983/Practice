using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Services
{
    public class T_ShouLiFenXiService : BaseService<T_ShouLiFenXi>
    {
        public T_ShouLiFenXiService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select * from T_ShouLiFenXi ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }
        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select * from T_ShouLiFenXi ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
