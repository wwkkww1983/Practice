using Caist.ICL.Core.Dao;
using Caist.ICL.Models;
using System.Collections.Generic;

namespace Caist.ICL.Services
{
    public class MenuService:BaseService<System_Menu>
    {
        public MenuService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "SELECT  [Id],[Menu_Name] FROM[ICLDB].[dbo].[System_Menu]";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }
        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "SELECT  a.[Id], a.[Menu_Name],a.[Menu_Url],b.Menu_Name as Menu_PName,a.Menu_Pid,a.Menu_Icon FROM[ICLDB].[dbo].[System_Menu] a left join (SELECT  [Id],[Menu_Name] FROM[ICLDB].[dbo].[System_Menu]) b on a.Menu_Pid=b.Id";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
