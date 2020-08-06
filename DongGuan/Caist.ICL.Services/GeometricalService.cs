/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/5/21 17:53:50
*说明：			几何特征 - Geometrical 
*****************************************************************************************************/
using Caist.ICL.Core.Dao;
using Caist.ICL.Models;
using System.Collections.Generic;

namespace Caist.ICL.Services
{
    public class GeometricalService : BaseService<Geometrical>
    {
        public GeometricalService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select * from Geometrical ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }

        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select * from Geometrical ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
