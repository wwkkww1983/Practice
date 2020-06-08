using Caist.Framework.Entity.Renyuanhistory;
using Caist.Framework.Model.Param.Jushanhistory;
using Caist.Framework.Model.Param.Renyuanhistory;
using Caist.Framework.Service.Renyuanhistory;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.Renyuanhistory
{
   public class RenyuanMonitorBLL
    {

        private readonly RenyuanMonitorService RenyuanMonitorService = new RenyuanMonitorService();
        #region 获取数据
        public async Task<TData<List<RenyuanMonitorEntity>>> GetSecurityInfoList(RenyuanMonitorParam param)
        {
            var obj = new TData<List<RenyuanMonitorEntity>>();
            var list = await RenyuanMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
