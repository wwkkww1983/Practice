using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Services
{
    public class LayingpathService : BaseService<Laying_Path>
    {
        public LayingpathService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select * from Laying_Path";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }
        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select a.[Id], a.[SName], a.[ProjectId], a.[CableType], a.[flength], a.[StartAddress], a.[StartPoint_X], a.[StartPoint_Y], a.[StartPoint_Z], a.[EndAddress], a.[EndPoint_X], a.[EndPoint_Y],a.[EndPoint_Z], a.[fremark] , a.[CreateId], a.[CreateUser], a.[CreateTime], a.[UpdateId], a.[UpdateUser], a.[UpdateTime], a.[Delteted], a.[Radius] , a.[PullValue], a.[SideValue], a.[IsQualified], a.[Solutions], b.[SectionCode],c.ProjectName from Laying_Path a left join (select id,SectionCode from [dbo].[Section]) b on a.SectionID=b.Id left join  (select Id,ProjectName from [dbo].[Project_Info]) c on a.ProjectID=c.Id ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
