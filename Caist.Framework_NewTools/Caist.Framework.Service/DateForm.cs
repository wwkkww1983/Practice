using Caist.Framework.DataAccess;
using System;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    public partial class DateForm : Form
    {
        public DateForm()
        {
            InitializeComponent();
        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            var h = DateTime.Now.Hour;
            var m = DateTime.Now.Minute;
            var s = DateTime.Now.Second;
            if (h == 23 && m == 59 && (s > 29 && s < 59))//接近凌晨才进行建表操作
            {
                try
                {
                    var tab = "mk_plc_tongfeng_values";
                    var dt = DateTime.Now;
                    var weeks = GetWeekOfYear(dt);
                    var tabName = string.Join(string.Empty, dt.Year, weeks);
                    var tabNameAc = GetTableName(dt, tabName);
                    if (ExistsTable(tabNameAc))
                    {
                        tabName = GetCurrentYearWeeks() > weeks ? string.Join(string.Empty, dt.Year, weeks + 1) : string.Join(string.Empty, dt.Year + 1, 1);
                        tabNameAc = string.Join(string.Empty, tab, tabName);
                        if (!ExistsTable(tabNameAc))
                        {
                            richTextBox1.Text = CreateTable(tabName).ToString();
                        }
                    }
                    else
                    {
                        richTextBox1.Text = CreateTable(tabName).ToString();
                    }
                }
                catch (Exception ex)
                {
                    richTextBox1.Text = ex.Message;
                }
            }
        }

        /// <summary>
        /// 获取动态表名（mk_plc_tongfeng_values202029）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public string GetTableName(DateTime dt, string tabName)
        {
            return string.Join(string.Empty, tabName, dt.Year, GetWeekOfYear(dt));
        }

        /// <summary>
        /// 当前年份有多少周
        /// </summary>
        /// <returns></returns>
        public int GetCurrentYearWeeks()
        {
            var dt = DateTime.Parse(string.Join("-", DateTime.Now.Year, "12", "31"));
            return GetWeekOfYear(dt);
        }

        /// <summary>
        /// 获取当前时间在第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetWeekOfYear(DateTime dt)
        {
            var t = dt.DayOfYear % 7;
            var res = t == 0 ? dt.DayOfYear / 7 : dt.DayOfYear / 7 + 1;
            return res;
        }

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public bool ExistsTable(string tabName)
        {
            string sql = string.Format("select top 1 * from sysObjects where Id=OBJECT_ID(N'{0}') and xtype='U'", tabName);
            using (var conn = Connect.GetConn("SQLServer"))
            {
                return conn.GetDataTable(sql).Rows.Count > 0;
            }
        }

        /// <summary>
        /// 动态创建表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int CreateTable(string tableName)
        {
            string sql = string.Format(@"CREATE TABLE [dbo].[mk_plc_tongfeng_values{0}](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[dict_Id] [varchar](32) NULL,
	[sys_Id] [varchar](32) NULL,
	[dict_Value] [varchar](50) NULL,
	[create_Time] [smalldatetime] NULL,
 CONSTRAINT [PK_mk_plc_tongfeng_values{0}] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]", tableName);

            using (var conn = Connect.GetConn("SQLServer"))
            {
                return conn.ExcuteSQL(sql);
            }

        }
    }
}
