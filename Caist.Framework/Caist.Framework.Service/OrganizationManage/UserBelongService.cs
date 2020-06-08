﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.OrganizationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;

namespace Caist.Framework.Service.OrganizationManage
{
    public class UserBelongService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<UserBelongEntity>> GetList(UserBelongEntity entity)
        {
            var expression = LinqExtensions.True<UserBelongEntity>();
            if (entity != null)
            {
                if (entity.BelongType != null)
                {
                    expression = expression.And(t => t.BelongType == entity.BelongType);
                }
                if (entity.UserId != null)
                {
                    expression = expression.And(t => t.UserId == entity.UserId);
                }
            }
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<UserBelongEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<UserBelongEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(UserBelongEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(long id)
        {
            await this.BaseRepository().Delete<UserBelongEntity>(id);
        }
        #endregion
    }
}
