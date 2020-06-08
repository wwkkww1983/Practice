using Caist.Framework.Entity.Pidaihistory;
using Caist.Framework.Model.Param.Pidaihistory;
using Caist.Framework.Service.Pidaihistory;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.Pidaihistory
{
  public  class PidaiMonitorBLL
    {
        private readonly PidaiMonitorService PidaiMonitorService = new PidaiMonitorService();
        #region 获取数据
        public async Task<TData<List<PidaiMonitorEntity>>> GetSecurityInfoList(PidaiMonitorParam param)
        {
            var obj = new TData<List<PidaiMonitorEntity>>();
            var list = await PidaiMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}

