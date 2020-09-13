using Caist.Framework.Entity.Security;
using Caist.Framework.Model.Security;
using Caist.Framework.Service.Security;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.Security
{
    public class SecurityMonitorBLL
    {
        private readonly SecurityMonitorService securityMonitorService = new SecurityMonitorService();

        #region 获取数据
        public async Task<TData<List<SecurityMonitorEntity>>> GetSecurityInfoList(SecurityMonitorParam param)
        {
            var obj = new TData<List<SecurityMonitorEntity>>();
            var list = await securityMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
