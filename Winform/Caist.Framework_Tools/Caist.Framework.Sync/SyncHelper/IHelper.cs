using System.Data;

namespace SyncDataAccess
{
    public interface IHelper
    {
        DataTable GetDataTable(string sql, string connstr);
        bool InsertData(string sql, string connstr);
    }
}
