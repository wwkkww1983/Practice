using Caist.ICL.Core;
using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Caist.ICL.Services
{
    public class Basic_LocusService : BaseService<Basic_Locus>
    {
        public Basic_LocusService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select * from Project_Info ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }
        public List<object> GetItemJisuan(string orderBy, string where, params object[] args)
        {
            var sql = @"select a.[Id],a.[SectionId],a.[GisPoint], a.[LinkPoint]
                ,a.[PointFeature],a.[Point_X],a.[Point_Y],a.[Point_Z],a.[GroundHeight],a.[LineHeight]
                ,a.[SectionSize],a.[Material],a.[BuryType],a.[remark],a.[CreateId],a.[CreateUser],a.[CreateTime]
                ,a.[UpdateId],a.[UpdateUser],a.[UpdateTime]
                ,a.[ProjectId],b.ProjectName,a.EquipmentTypeId from Basic_Locus a left join [dbo].[Project_Info] b on a.ProjectId=b.Id ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }
        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = @"select a.[Id],a.[SectionId],a.[GisPoint], a.[LinkPoint]
                ,a.[PointFeature],a.[Point_X],a.[Point_Y],a.[Point_Z],a.[GroundHeight],a.[LineHeight]
                ,a.[SectionSize],a.[Material],a.[BuryType],a.[remark],a.[CreateId],a.[CreateUser],a.[CreateTime]
                ,a.[UpdateId],a.[UpdateUser],a.[UpdateTime]
                ,a.[ProjectId],b.ProjectName,a.EquipmentTypeId from Basic_Locus a left join [dbo].[Project_Info] b on a.ProjectId=b.Id";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            else
                sql += "order by a.[CreateTime]";
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
