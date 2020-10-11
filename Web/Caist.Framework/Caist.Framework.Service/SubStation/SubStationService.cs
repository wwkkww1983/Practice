using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.SubStation;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Model.SubStation;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.SubStation
{
    public class SubStationService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<SubStationEntity>> GetSubStationInfoList(SubStationParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.OrderBy(p => p.SysId).ToList();
        }

        /// <summary>
        /// 获取供配电指定指令最新值
        /// </summary>
        /// <param name="dictId">opc指令值</param>
        /// <returns></returns>
        public async Task<SubStationInfo> GetRealTimeData(string DictId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select top(1) dict_Id as DictId,dict_Value as DictValue,Instruct_type as InstructType,create_Time as CreateTime
                            from [dbo].[mk_substation] ");
            if (!string.IsNullOrEmpty(DictId))
            {
                strSql.AppendFormat(@" where dict_Id='{0}' ", DictId);
            }
            strSql.Append(@" order by create_Time desc");
            return await this.BaseRepository().FindObject<SubStationInfo>(strSql.ToString());
        }

        #endregion

        #region 私有方法
        private Expression<Func<SubStationEntity, bool>> ListFilter(SubStationParam param)
        {
            var expression = LinqExtensions.True<SubStationEntity>();
            if (param != null)
            {
                if (param.DictId.HasValue())
                {
                    expression = expression.And(t => t.DictId == param.DictId);
                }
            }
            return expression;
        }
        #endregion
    }
}
