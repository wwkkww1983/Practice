using Caist.Framework.Entity.Shuibenghistory;
using Caist.Framework.Model.Param.Shuibenghistory;
using Caist.Framework.Service.Shuibenghistory;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.Shuibenghistory
{
   public class ShuibengMonitorBLL
    {
        private readonly ShuibengMonitorService ShuibengMonitorService = new ShuibengMonitorService();
        #region 获取数据
        public async Task<TData<List<ShuibengMonitorEntity>>> GetSecurityInfoList(ShuibengMonitorParam param)
        {
            var obj = new TData<List<ShuibengMonitorEntity>>();
            var list = await ShuibengMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
        #region 实时获取数据
        public async Task<TData<List<ShuibengMonitorEntity>>> GetSecurityInfoListr(ShuibengMonitorParam param)
        {
            var obj = new TData<List<ShuibengMonitorEntity>>();
            var list = await ShuibengMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
