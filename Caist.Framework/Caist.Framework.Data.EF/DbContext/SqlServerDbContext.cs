using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Caist.Framework.Data.EF
{
    public class SqlServerDbContext : DbContext, IDisposable
    {
        private string ConnectionString { get; set; }

        #region 构造函数
        private static SqlConnection GetEFConnctionString(string connString)
        {
            var obj = ConfigurationManager.ConnectionStrings[connString];
            SqlConnection con;
            if (obj != null)
            {
                con = new SqlConnection(obj.ConnectionString);
            }
            else
            {
                con = new SqlConnection(connString);
            }
            return con;
        }
        public SqlServerDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region 重载
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //解决EF版本过高导致在 FETCH 语句中选项 NEXT 的用法无效 的BUG
            //optionsBuilder.UseSqlServer(ConnectionString);
            optionsBuilder.UseSqlServer(ConnectionString, b => b.UseRowNumberForPaging());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly entityAssembly = Assembly.Load(new AssemblyName("Caist.Framework.Entity"));
            IEnumerable<Type> typesToRegister = entityAssembly.GetTypes().Where(p => !string.IsNullOrEmpty(p.Namespace))
                                                                         .Where(p => !string.IsNullOrEmpty(p.GetCustomAttribute<TableAttribute>()?.Name));
            foreach (Type type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Model.AddEntityType(type);
            }
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                PrimaryKeyConvention.SetPrimaryKey(modelBuilder, entity.Name);
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.Relational().TableName;
                modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToLower());

                var properties = entity.GetProperties();
                foreach (var property in properties)
                {
                    ColumnConvention.SetColumnName(modelBuilder, entity.Name, property.Name);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
