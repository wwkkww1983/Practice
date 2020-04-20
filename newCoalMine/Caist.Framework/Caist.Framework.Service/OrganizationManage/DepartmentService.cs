﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.OrganizationManage;
using Caist.Framework.Model;
using Caist.Framework.Model.Param.OrganizationManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;

namespace Caist.Framework.Service.OrganizationManage
{
    public class DepartmentService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<DepartmentEntity>> GetList(DepartmentListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.OrderBy(p => p.DepartmentSort).ToList();
        }

        public async Task<DepartmentEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<DepartmentEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(department_sort) FROM sys_department");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }

        /// <summary>
        /// 部门名称是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ExistDepartmentName(DepartmentEntity entity)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.DepartmentName == entity.DepartmentName);
            }
            else
            {
                expression = expression.And(t => t.DepartmentName == entity.DepartmentName && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        /// <summary>
        /// 是否存在子部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExistChildrenDepartment(long id)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            expression = expression.And(t => t.ParentId == id);
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(DepartmentEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
                await db.Delete<DepartmentEntity>(idArr);
                await db.Commit();
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        #endregion

        #region 私有方法
        private Expression<Func<DepartmentEntity, bool>> ListFilter(DepartmentListParam param)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            if (param != null)
            {
                if (!param.DepartmentName.IsEmpty())
                {
                    expression = expression.And(t => t.DepartmentName.Contains(param.DepartmentName));
                }
            }
            return expression;
        }
        #endregion
    }
}
