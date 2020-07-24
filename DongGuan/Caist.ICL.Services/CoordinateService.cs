/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/4/30 13:30:04
*说明：			地理坐标 - Coordinate 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;

namespace Caist.ICL.Services
{
    public class CoordinateService : BaseService<Coordinate>
    {
        public CoordinateService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select * from Coordinate ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }

        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select * from Coordinate ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
