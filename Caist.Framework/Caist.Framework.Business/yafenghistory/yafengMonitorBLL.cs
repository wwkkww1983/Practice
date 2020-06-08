using Caist.Framework.Entity.yafenghistory;
using Caist.Framework.Model.yafenghistory;
using Caist.Framework.Service.Security;
using Caist.Framework.Service.yafenghistory;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.yafenghistory
{
   public class yafengMonitorBLL
    {
        private readonly yafengMonitorService yafengMonitorService = new yafengMonitorService();
        #region 获取数据
        public async Task<TData<List<yafengMonitorEntity>>> GetSecurityInfoList(yafengMonitorParam param)
        {
            var obj = new TData<List<yafengMonitorEntity>>();
            var list = await yafengMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
