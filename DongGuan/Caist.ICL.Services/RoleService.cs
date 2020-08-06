using Caist.ICL.Core.Dao;
using Caist.ICL.Models;
using System.Collections.Generic;

namespace Caist.ICL.Services
{
    public class RoleService : BaseService<System_Role>
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select Id,RoleName from System_Role";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }
        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select * from System_Role";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
