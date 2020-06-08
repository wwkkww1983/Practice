using Caist.Framework.Entity.Tongfenghistory;
using Caist.Framework.Model.Param.Tongfenghistory;
using Caist.Framework.Service.Tongfenghistory;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.Tongfenghistory
{
   public class TongfengMonitorBLL
    {
        private readonly TongfengMonitorService TongfengMonitorService = new TongfengMonitorService();
        #region 获取数据
        public async Task<TData<List<TongfengMonitorEntity>>> GetSecurityInfoList(TongfengMonitorParam param)
        {
            var obj = new TData<List<TongfengMonitorEntity>>();
            var list = await TongfengMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
