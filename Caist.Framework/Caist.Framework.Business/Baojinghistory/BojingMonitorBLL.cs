using Caist.Framework.Entity.Baojinghistory;
using Caist.Framework.Model.Param.Baojinghistory;
using Caist.Framework.Service.Baojinghistory;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.Baojinghistory
{
   public class BojingMonitorBLL
    {
        private readonly BojingMonitorService bojingMonitorService = new BojingMonitorService();
        #region 获取数据
        public async Task<TData<List<BojingMonitorEntity>>> GetSecurityInfoList(BojingMonitorParam param)
        {
            var obj = new TData<List<BojingMonitorEntity>>();
            var list = await bojingMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
