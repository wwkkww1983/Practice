using Caist.Framework.Entity.Jushanhistory;
using Caist.Framework.Model.Param.Jushanhistory;
using Caist.Framework.Service.Jushanhistory;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.Jushanhistory
{
   public class JushanMonitorBLL
    {
        private readonly JushanMonitorService JushanMonitorService = new JushanMonitorService();
        #region 获取数据
        public async Task<TData<List<JushanMonitorEntity>>> GetSecurityInfoList(JushanMonitorParam param)
        {
            var obj = new TData<List<JushanMonitorEntity>>();
            var list = await JushanMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
        #region 获取数据
        public async Task<TData<List<JushanMonitorEntity>>> GetSecurityInfoListr(JushanMonitorParam param)
        {
            var obj = new TData<List<JushanMonitorEntity>>();
            var list = await JushanMonitorService.GetSecurityInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
