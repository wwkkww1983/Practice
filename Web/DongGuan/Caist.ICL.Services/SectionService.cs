/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/5/21 17:53:50
*说明：			 - Section 
*****************************************************************************************************/
using Caist.ICL.Core.Dao;
using Caist.ICL.Models;
using System.Collections.Generic;

namespace Caist.ICL.Services
{
    public class SectionService : BaseService<Section>
    {
        public SectionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select Id,SectionCode from Section ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }

        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "SELECT  a.[Id] , a.[SectionCode] ,a.[ProjectID],b.ProjectName,a.[Distance],a.[Location],a.[State],a.[DefaultEquipmentDistance] ,a.[WarningSMS],a.[NowSection] ,a.[Phones] ,a.[SysCompanyId],a.[CreateId],a.[CreateUser],a.[CreateTime],a.[UpdateId] ,a.[UpdateUser] ,a.[UpdateTime] ,a.[asjcode],a.[Sazcod] ,a.[Asdcod] ,a.[Delteted] FROM [ICLDB].[dbo].[Section] a left join[dbo].[Project_Info] b on a.ProjectId=b.Id ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
        public List<object> GetSectionItems(string projectId)
        {
            var sql = $"select SectionId,SectionId as SectionText from ICLDB.dbo.Cable_Info where ProjectID='{projectId}' group by SectionId;";
            var data = UnitOfWork.Sql<object>(sql);
            return data;
        }
    }
}
