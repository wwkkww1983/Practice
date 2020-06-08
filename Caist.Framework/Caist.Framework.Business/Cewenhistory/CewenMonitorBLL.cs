using Caist.Framework.Entity.Cewenhistory;
using Caist.Framework.Model.Param.Cewenhistory;
using Caist.Framework.Service.Cewenhistory;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.Cewenhistory
{
   public class CewenMonitorBLL
    {
        private readonly CewenMonitorService CewenMonitorService = new CewenMonitorService();
        #region 获取数据
        public async Task<TData<List<CewenMonitorEntity>>> GetSecurityInfoList(CewenMonitorParam param)
        {
            var obj = new TData<List<CewenMonitorEntity>>();
            var list = await CewenMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
