/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/5/21 17:53:50
*说明：			字典类型表 - BaseInfo_Dictype 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;

namespace Caist.ICL.Services
{
    public class Dic_TypeService : BaseService<Dic_Type>
    {
        public Dic_TypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select Id,TypeName from Dic_Type ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }

        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select * from Dic_Type ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
