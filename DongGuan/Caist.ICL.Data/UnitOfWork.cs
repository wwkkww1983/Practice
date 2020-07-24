using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Caist.ICL.Core.Dao;
using NPoco;
using NPoco.FluentMappings;

namespace Caist.ICL.Data
{
    /// <summary>
    /// 工作单元
    /// ljl@2018
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        Database database;
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public Database Database => database ?? (database = Factory.GetDatabase());

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        public void Delete<TEntity>(object id)
        {
            Database.Delete<TEntity>(id);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        public void Delete<TEntity>(Expression<Func<TEntity, bool>> where)
        {
            Database.DeleteMany<TEntity>().Where(where).Execute();
        }

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get<TEntity>(object id)
        {
            return Database.SingleOrDefaultById<TEntity>(id);
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> where)
        {
            return Database.Query<TEntity>().Where(where).FirstOrDefault();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Insert<TEntity>(TEntity entity)
        {
            Database.Insert(entity);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entites"></param>
        public void Insert<TEntity>(IEnumerable<TEntity> entites)
        {
            Database.Insert(entites);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Update<TEntity>(TEntity entity)
        {
            Database.Update(entity);
        }

        /// <summary>
        /// 更新值改变的列
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="oldEntity"></param>
        public void Update<TEntity>(TEntity entity, TEntity oldEntity)
        {
            //var ss = Database.StartSnapshot(oldEntity);
            //ss.OverrideTrackedObject(entity);
            var ss = new Snapshot<TEntity>(Database, oldEntity);
            ss.OverrideTrackedObject(entity);
            Database.Update(entity, ss.UpdatedColumns());
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> where)
        {
            return Database.Query<TEntity>().Where(where).ToList();
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public List<TEntity> GetList<TEntity>(string where, params object[] args)
        {
            return Database.Query<TEntity>().Where(where, args).ToList();
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public List<TEntity> GetList<TEntity>(string orderBy, string where, params object[] args)
        {
            NPoco.Expressions.DefaultSqlExpression<TEntity> sqlExpression = new NPoco.Expressions.DefaultSqlExpression<TEntity>(Database);
            sqlExpression.Where(where, args);
            var sql = sqlExpression.Context.ToSelectStatement();
            if (!string.IsNullOrEmpty(orderBy))
            {
                sql += " order by " + orderBy;
            }

            return Database.Query<TEntity>(sql, sqlExpression.Context.Params).ToList();
        }

        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<TEntity> GetPage<TEntity>(int page, int rows, out int total, Expression<Func<TEntity, bool>> where)
        {
            var data = Database.Query<TEntity>().Where(where).ToPage(page, rows);
            total = (int)data.TotalItems;
            return data.Items;
        }

        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="where"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<TEntity> GetPage<TEntity>(int page, int rows, Expression<Func<TEntity, bool>> where)
        {
            var data = Database.Query<TEntity>().Where(where).Limit((page - 1) * rows, rows).ToList();//.Fetch<TEntity>(page, rows, new Sql());
            return data;
        }

        /// <summary>
        /// 查询SQL
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public List<TObject> Sql<TObject>(string sql, params object[] args)
        {
            return Database.Query<TObject>(sql, args).ToList();
        }

        public TObject SqlGet<TObject>(string sql, params object[] args)
        {
            return Database.Query<TObject>(sql, args).FirstOrDefault();
        }

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
        public List<TObject> SqlPage<TObject>(int page, int rows, out int total, string sql, params object[] args)
        {
            var data = Database.Page<TObject>(page, rows, sql, args);
            total = (int)data.TotalItems;
            return data.Items;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public TValue GetValue<TValue>(string sql, params object[] args)
        {
            return Database.ExecuteScalar<TValue>(sql, args);
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Execute(string sql, params object[] args)
        {
            return Database.Execute(sql, args);
        }

        ///// <summary>
        ///// 查询
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <returns></returns>
        //public IQuery<T> Query<T>()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="updateColumns"></param>
        public void Update<T>(T entity, Expression<Func<T, object>> updateColumns)
        {
            Database.Update(entity, updateColumns);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="entity"></param>
        public void Update<T>(Expression<Func<T, bool>> where, T entity)
        {
            Database.UpdateMany<T>().Where(where).Execute(entity);
        }

        /// <summary>
        /// 根据条件更新指定列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="entity"></param>
        /// <param name="updateColumns"></param>
        public void Update<T>(Expression<Func<T, bool>> where, T entity, Expression<Func<T, object>> updateColumns)
        {
            Database.UpdateMany<T>().Where(where).OnlyFields(updateColumns).Execute(entity);
        }

        /// <summary>
        /// 事务包裹提交
        /// </summary>
        /// <param name="action"></param>
        public void CommitTransaction(Action action)
        {
            try
            {
                Database.BeginTransaction();
                action();
                Database.CompleteTransaction();
            }
            catch (Exception ex)
            {
                Database.AbortTransaction();
                throw ex;
            }
        }

        static DatabaseFactory Factory;
        public void Register(string connectionString)
        {
            var config = FluentMappingConfiguration.Configure(new EntityMappings());
            Factory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() => new Database(connectionString, DatabaseType.SqlServer2008, System.Data.SqlClient.SqlClientFactory.Instance));
                x.WithFluentConfig(config);
            });
        }

        public string GetLastSQL()
        {
            return Database.LastSQL;
        }

        public bool Any<T>(Expression<Func<T, bool>> where)
        {
            return Database.Query<T>().Where(where).Any();
        }

        public ISnapshot<TEntity> Snapshot<TEntity>(TEntity entity)
        {
            var ss = new Snapshot<TEntity>(Database, entity);
            ss.OverrideTrackedObject(entity);
            return ss;
        }
    }
}
