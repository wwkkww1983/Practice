/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/4/30 13:30:04
*说明：			设备部署表 - Device_Deployment 
*****************************************************************************************************/
using Caist.ICL.Core.Dao;
using Caist.ICL.Models;
using System.Collections.Generic;

namespace Caist.ICL.Services
{
    public class Device_DeploymentService : BaseService<Device_Deployment>
    {
        public Device_DeploymentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<object> GetItem(string orderBy, string where, params object[] args)
        {
            var sql = "select * from Device_Deployment ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.Sql<object>(sql, args);
            return data;
        }

        public List<object> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        {
            var sql = "select * from Device_Deployment ";
            if (!string.IsNullOrEmpty(where))
                sql += " where " + where;
            if (!string.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;
            var data = UnitOfWork.SqlPage<object>(page, rows, out total, sql, args);
            return data;
        }
    }
}
