using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.SystemManage;
using Caist.Framework.Enum.SystemManage;
using Caist.Framework.Model.Param.SystemManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Caist.Framework.Service.SystemManage
{
    public class FileService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<FileEntity>> GetList(FileListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<FileEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<FileEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(List<FileEntity> entitys)
        {
            if (entitys.HasValue())
            {
                //先根据模块ID来删除对应的文件列表
                await DeleteFileByModuleId(entitys[0].ModuleId.Value);
                entitys.ForEach(p => p.Create());
                await this.BaseRepository().Insert<FileEntity>(entitys);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<FileEntity>(idArr);
        }
        public async Task DeleteFileByModuleId(long moduleId)
        {
            await this.BaseRepository().Delete<FileEntity>("module_id", moduleId);
        }
        #endregion

        #region 私有方法
        private Expression<Func<FileEntity, bool>> ListFilter(FileListParam param)
        {
            var expression = LinqExtensions.True<FileEntity>();
            if (param != null)
            {
                if (param.ModuleType.HasValue())
                {
                    expression = expression.And(t => t.ModuleType == param.ModuleType);
                }
                if (param.ModuleId.HasValue())
                {
                    expression = expression.And(t => t.ModuleId == param.ModuleId);
                }
            }
            return expression;
        }
        #endregion
    }
}
