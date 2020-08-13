using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Caist.ICL.Core.Dao
{
    /// <summary>
    /// 工作单元
    /// ljl@2018
    /// </summary>
    public interface IUnitOfWork
    {
        string GetLastSQL();
        /// <summary>
        /// 注册数据库
        /// </summary>
        /// <param name="connectionString"></param>
        void Register(string connectionString);
        /// <summary>
        /// 事务包裹提交
        /// </summary>
        /// <param name="action"></param>
        void CommitTransaction(Action action);
        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        void Delete<TEntity>(Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        void Delete<TEntity>(object id);
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        int Execute(string sql, params object[] args);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(object id);

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        List<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        List<TEntity> GetList<TEntity>(string where, params object[] args);

        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        List<TEntity> GetPage<TEntity>(int page, int rows, Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 是否有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Any<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        List<TEntity> GetPage<TEntity>(int page, int rows, out int total, Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        TValue GetValue<TValue>(string sql, params object[] args);
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entites"></param>
        void Insert<TEntity>(IEnumerable<TEntity> entites);
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Insert<TEntity>(TEntity entity);
        /// <summary>
        /// 查询SQL
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        List<TObject> Sql<TObject>(string sql, params object[] args);
        /// <summary>
        /// 查询SQL
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        TObject SqlGet<TObject>(string sql, params object[] args);
        /// <summary>
        /// 分页查询SQL
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        List<TObject> SqlPage<TObject>(int page, int rows, out int total, string sql, params object[] args);
        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="entity"></param>
        void Update<T>(Expression<Func<T, bool>> where, T entity);
        /// <summary>
        /// 根据条件更新指定列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="entity"></param>
        /// <param name="updateColumns"></param>
        void Update<T>(Expression<Func<T, bool>> where, T entity, Expression<Func<T, object>> updateColumns);
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="updateColumns"></param>
        void Update<T>(T entity, Expression<Func<T, object>> updateColumns);
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Update<TEntity>(TEntity entity);
        /// <summary>
        /// 更新值改变的列
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="oldEntity"></param>
        void Update<TEntity>(TEntity entity, TEntity oldEntity);

        /// <summary>
        /// 创建实例的快照
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        ISnapshot<TEntity> Snapshot<TEntity>(TEntity entity);
    }
}
