using Caist.Framework.Entity;
using Caist.Framework.DataAccess;
using System.Text;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Caist.Framework.Business.ApplicationManage
{
    public class MqThemeBLL
    {
        public static async Task<List<MqThemeEntity>> GetList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"SELECT * FROM dbo.mk_mq_theme");

            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable =await conn.GetDataTableAsync(builder.ToString());
                return DataConvert.DataTableToList<MqThemeEntity>(dataTable).ToList();
            }
        }
    }
}
