using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Services
{
    public class UserOfRolesService : BaseService<System_UserOfRoles>
    {
        public UserOfRolesService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select * from System_UserInfo";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }
        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select a.Id, a.UserID,a.RoleId,b.UserName,c.RoleName from System_UserOfRoles a left join System_UserInfo b on a.UserId=b.Id left join System_Role c on a.RoleId = c.Id";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }

    }
}
