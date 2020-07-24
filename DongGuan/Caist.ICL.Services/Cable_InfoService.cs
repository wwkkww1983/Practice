/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/5/21 17:53:50
*说明：			电缆信息 - Cable_Info 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;

namespace Caist.ICL.Services
{
    public class Cable_InfoService : BaseService<Cable_Info>
    {
        public Cable_InfoService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select * from  Cable_Info";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }

        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select a.*,b.ProjectName,c.CableType, case when UserStatus=0 then '未使用' when UserStatus=1 then '使用中' end as CableStatus from Cable_Info a  left join  (select Id,ProjectName from [dbo].[Project_Info]) b on a.ProjectId=b.Id left join dbo.CableType c on a.CableTypeID = c.id";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
