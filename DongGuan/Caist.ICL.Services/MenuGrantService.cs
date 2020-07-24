using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Services
{
    public class MenuGrantService : BaseService<System_MenuGrant>
    {
        public MenuGrantService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "SELECT  a.[Id] , a.[Menu_Id],a.[grant_query],a.[grant_create],a.[grant_edit],a.[grant_delete],a.[grant_print],a.[RoleId],b.[Menu_Name],b.[Menu_Url],b.[Menu_Pid],c.RoleCode FROM[ICLDB].[dbo].[Sys_Menu_Grant] a left join[dbo].[System_Menu] b on a.Menu_Id=b.Id left join[dbo].[System_Role] c on a.RoleId=c.id";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }
        public List<object> GetUserMenus(string id)
        {
            var sql = "SELECT  a.[Id] , a.[Menu_Id],a.[grant_query],a.[grant_create],a.[grant_edit],a.[grant_delete],a.[grant_print],a.[RoleId],b.[Menu_Name],b.[Menu_Url],b.[Menu_Icon],b.[Menu_Pid],c.RoleCode FROM[ICLDB].[dbo].[Sys_Menu_Grant] a left join[dbo].[System_Menu] b on a.Menu_Id=b.Id left join[dbo].[System_Role] c on a.RoleId=c.id where RoleId='" + id+"'";
            var data = UnitOfWork.Sql<object>(sql);
            return data;
        }
        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "SELECT a.[Id],a.RoleId,c.RoleName,a.[Menu_Id],case a.[grant_query] when 0 then '否' else '是' end as grant_query ,case a.[grant_create] when 0 then '否' else '是' end as [grant_create],case a.[grant_edit] when 0 then '否' else '是' end as [grant_edit] ,case a.[grant_delete] when 0 then '否' else '是' end [grant_delete],case a.[grant_print] when 0 then '否' else '是' end [grant_print],b.Menu_Name FROM[ICLDB].[dbo].[Sys_Menu_Grant] a left join System_Menu b on a.Menu_Id=b.Id left join [dbo].[System_Role] c on a.RoleId=c.Id";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
