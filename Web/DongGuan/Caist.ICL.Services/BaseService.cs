using Caist.ICL.Core.Dao;
using Caist.ICL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Caist.ICL.Services
{
    /// <summary>
    /// 业务操作基类
    /// </summary>
    public class BaseService<TEntity>
        where TEntity : BaseEntity
    {
        public IUnitOfWork UnitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(TEntity entity)
        {
            UnitOfWork.Insert(entity);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            UnitOfWork.Insert(entities);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            UnitOfWork.Delete<TEntity>(id);
        }
        /// <summary>
        ///  批量删除
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        public virtual void DeleteBase(BaseEntity[] sysId)
        {
            foreach (var item in sysId)
            {
                UnitOfWork.Delete<TEntity>(item.Id);
            }
        }
        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="where"></param>
        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            UnitOfWork.Delete(where);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            UnitOfWork.Update(entity);
        }

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateColumns"></param>
        public virtual void Update(TEntity entity, Expression<Func<TEntity, object>> updateColumns)
        {
            UnitOfWork.Update(entity, updateColumns);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="entity"></param>
        public virtual void Update(Expression<Func<TEntity, bool>> where, TEntity entity)
        {
            UnitOfWork.Update(where, entity);
        }

        /// <summary>
        /// 根据条件更新指定列
        /// </summary>
        /// <param name="where"></param>
        /// <param name="entity"></param>
        /// <param name="updateColumns"></param>
        public virtual void Update(Expression<Func<TEntity, bool>> where, TEntity entity, Expression<Func<TEntity, object>> updateColumns)
        {
            UnitOfWork.Update(where, entity, updateColumns);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="oldEntity"></param>
        public virtual void Update(TEntity entity, TEntity oldEntity)
        {
            UnitOfWork.Update(entity, oldEntity);
        }

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(object id)
        {
            return UnitOfWork.Get<TEntity>(id);
        }

        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> where)
        {
            return UnitOfWork.GetList(where);
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <param name="where"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual List<TEntity> GetList(string where, params object[] args)
        {
            return UnitOfWork.GetList<TEntity>(where, args);
        }

        ///// <summary>
        ///// 根据条件获取
        ///// </summary>
        ///// <param name="orderBy"></param>
        ///// <param name="where"></param>
        ///// <param name="args"></param>
        ///// <returns></returns>
        //public List<TEntity> GetList(string orderBy, string where, params object[] args)
        //{
        //    return UnitOfWork.GetList<TEntity>(orderBy, where, args);
        //}

        ///// <summary>
        ///// 根据条件获取
        ///// </summary>
        ///// <param name="page"></param>        ///// <param name="rows"></param>
        ///// <param name="orderBy"></param>
        ///// <param name="where"></param>
        ///// <param name="args"></param>
        ///// <returns></returns>
        //public List<TEntity> GetPage(int page, int rows, out int total, string orderBy, string where, params object[] args)
        //{
        //    return UnitOfWork.GetPage<TEntity>(page, rows, out total, orderBy, where, args);
        //}
    }
}