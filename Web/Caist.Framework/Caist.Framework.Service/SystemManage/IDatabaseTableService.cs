using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Util.Model;

namespace Caist.Framework.Service.SystemManage
{
    public interface IDatabaseTableService
    {
        Task<bool> DatabaseBackup(string database, string backupPath);
        Task<List<TableInfo>> GetTableList(string tableName);
        Task<List<TableInfo>> GetTablePageList(string tableName, Pagination pagination);
        Task<List<TableFieldInfo>> GetTableFieldList(string tableName);
    }
}
