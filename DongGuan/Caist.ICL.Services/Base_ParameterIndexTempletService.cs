/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/5/21 17:53:49
*说明：			指标模版，包括测点指标和计算指标
    - Base_ParameterIndexTemplet 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;

namespace Caist.ICL.Services
{
    public class Base_ParameterIndexTempletService : BaseService<Base_ParameterIndexTemplet>
    {
        public Base_ParameterIndexTempletService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select Id,IndexName from Base_ParameterIndexTemplet ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }

        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select * from Base_ParameterIndexTemplet ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
