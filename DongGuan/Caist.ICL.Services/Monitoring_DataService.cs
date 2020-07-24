/****************************************************************************************************
*创建人:			sty
*创建时间:		2019/5/28
*说明：			监测数据管理 - Monitoring_Data 
*****************************************************************************************************/
using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Services
{
    public class Monitoring_DataService : BaseService<Monitoring_Data>
    {
        public Monitoring_DataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {}
        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select * from Monitoring_Data";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }

        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select a.Id,a.[ftime],b.IndexName,a.[MonitorPointId],a.[fdata] ,a.[fremark],a.[CreateUser],a.[CreateTime],a.[UpdateUser],a.[UpdateTime] from Monitoring_Data a left join [dbo].[Base_ParameterIndexTemplet] b on a.PIndexId=b.id";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
