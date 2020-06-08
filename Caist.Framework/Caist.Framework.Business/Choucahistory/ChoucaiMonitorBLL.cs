using Caist.Framework.Entity.Choucahistory;
using Caist.Framework.Model.Param.Choucahistory;
using Caist.Framework.Service.Choucahistory;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.Choucahistory
{
   public class ChoucaiMonitorBLL
    {
        private readonly ChoucaiMonitorService ChoucaiMonitorService = new ChoucaiMonitorService();
        #region 获取数据
        public async Task<TData<List<ChoucaiMonitorEntity>>> GetSecurityInfoList(ChoucaiMonitorParam param)
        {
            var obj = new TData<List<ChoucaiMonitorEntity>>();
            var list = await ChoucaiMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
