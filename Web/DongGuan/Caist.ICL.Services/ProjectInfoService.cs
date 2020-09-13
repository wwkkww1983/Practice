using Caist.ICL.Core.Dao;
using Caist.ICL.Models;
using System.Collections.Generic;

namespace Caist.ICL.Services
{/// <summary>
 /// 构造服务方法
 /// </summary>
    public class ProjectInfoService : BaseService<Project_Info>
    {
        public ProjectInfoService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select Id,ProjectName from Project_Info";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }
        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select *,case [State] when 0 then '未施工' when 1 then '施工中' when 2 then '完工' when 3 then '停工' end StateStatus from Project_Info ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
