/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/5/21 17:53:49
*说明：			字典条目表 - BaseInfo_DicItem 
*****************************************************************************************************/
using Caist.ICL.Core.Dao;
using Caist.ICL.Models;
using System.Collections.Generic;

namespace Caist.ICL.Services
{
    public class Dic_ContentService : BaseService<Dic_Content>
    {
        public Dic_ContentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select * from Dic_Content ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }

        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select a.Id,a.[DicTypeContent],b.[TypeName],a.[CreateUser],a.[CreateTime],a.[UpdateUser],a.[UpdateTime],a.[Sort] from Dic_Content a left join [dbo].[Dic_Type] b on a.DicType_ID=b.id";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
