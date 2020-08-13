using Caist.Framework.Entity.Security;
using Caist.Framework.Entity.SubStation;
using Caist.Framework.Model.Security;
using Caist.Framework.Model.SubStation;
using Caist.Framework.Service.Security;
using Caist.Framework.Service.SubStation;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.Security
{
    public class SubStationBLL
    {
        private readonly SubStationService subStationService = new SubStationService();

        #region 获取数据
        public async Task<TData<List<SubStationEntity>>> GetSubStationInfoList(SubStationParam param)
        {
            var obj = new TData<List<SubStationEntity>>();
            var list = await subStationService.GetSubStationInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
