using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.ApplicationManage
{
    public class LedDeviceService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<LedDeviceEntity>> GetList(LedDeviceParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<LedDeviceEntity>> GetPageList(LedDeviceParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<LedDeviceEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<LedDeviceEntity>(id);
        }
        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(id) FROM mk_mq_theme");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }

        #endregion

        #region 提交数据
        public async Task SaveForm(LedDeviceEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<LedDeviceEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<LedDeviceEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<LedDeviceEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<LedDeviceEntity, bool>> ListFilter(LedDeviceParam param)
        {


            var expression = LinqExtensions.True<LedDeviceEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.DeviceName))
                {
                    expression = expression.And(t => t.DeviceName.Contains(param.DeviceName));
                }

                if (!string.IsNullOrEmpty(param.StartTime.ParseToString()))
                {
                    expression = expression.And(t => t.BaseModifyTime >= param.StartTime);
                }
                if (!string.IsNullOrEmpty(param.EndTime.ParseToString()))
                {
                    param.EndTime = (param.EndTime.Value.ToString("yyyy-MM-dd") + " 23:59:59").ParseToDateTime();
                    expression = expression.And(t => t.BaseModifyTime <= param.EndTime);
                }
            }
            return expression;
        }
        #endregion
    }
}
