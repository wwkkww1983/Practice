using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "192.168.20.120";
            textBox2.Text = "sa";
            textBox3.Text = "sa";
            textBox4.Text = "newpd";
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }
        private void button1_Click(object sender, EventArgs e)
        {
            DbHelperSQL.connectionString = "server=" + textBox1.Text + ";uid="+ textBox2.Text + ";pwd="+ textBox3.Text + ";database=" + textBox4.Text + ";";

            DbHelperSQLs.connectionString = "server=192.168.20.5;uid=mchy;pwd=Mchy123456;database=testScadaHis;";
            //开启任务执行光纤测温数据采集
            Task task1 = new Task(() =>
            {
                Stopwatch stw = new Stopwatch();
                stw.Start();
                this.GQCW();
            });
            task1.Start();
            //处理其他数据同步
            Task task2 = new Task(() =>
            {
                Stopwatch stw = new Stopwatch();
                stw.Start();
                this.DataTongbu();
            });
            task2.Start();

            //LED屏幕人员定位信息文件发布
            Task task3 = new Task(() =>
            {
                Stopwatch stw = new Stopwatch();
                stw.Start();
                this.LEDFaBu();
            });
            task3.Start();
        }

        private void LEDFaBu()
        {
            while (true)
            {
                TXT_Write();
                Thread.CurrentThread.Join(60000); //一分钟
            }
        }

        private void DataTongbu()
        {
            while(true)
            {
                //人员信息同步
                this.PeopleTongbu();
                //部门信息同步
                //this.DepartTongbu();
                //打卡机信息同步
                //this.Reauet();
                //工种信息同步
                //this.Datebine();
                //职务信息同步
                //this.Toume();
                //实时人员位置信息同步
                this.Rettou();
                //安全监测实时同步
                this.SecurityTongbu();
                Thread.CurrentThread.Join(1000);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //人员信息同步
             this.PeopleTongbu();


            //部门信息同步
             this.DepartTongbu();

            //打卡机信息同步
             this.Reauet();

            //工种信息同步
            this.Datebine();

            //职务信息同步
            this.Toume();

            //实时人员位置信息同步
            this.Rettou();

            //安全监测实时同步
            this.SecurityTongbu();
        }

        private void SecurityTongbu()
        {
            string selsql = "select * from realdata_kj90 where point <>''";
            DataTable dt = DbHelperSQLAQ.GetDataTable(selsql);
            if (dt.Rows.Count > 0)
            {
                MySQLHelper.Run_SQL("delete from securitymonitoringrealtimedatahistory where UpdatDateTime<'" + DateTime.Now.AddDays(-6).ToShortDateString() + " 00:00:00'",MySQLHelper.ConnStr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    MySQLHelper.Run_SQL("update securitymonitoringrealtimedata set Address='"+ dr["Address"].ToString() + "',lc='"+ dr["lc"].ToString() + "',lx='" + dr["lx"].ToString() + "',dw='" + dr["dw"].ToString() + "',ssz='" + dr["ssz"].ToString() + "',StatName='" + dr["StatName"].ToString() + "',UpdatDateTime='"+ DateTime.Now.ToString() +"' where point='" + dr["point"].ToString() + "'", MySQLHelper.ConnStr);
                    MySQLHelper.Run_SQL("insert into securitymonitoringrealtimedatahistory values('" + dr["point"].ToString() + "','" + dr["Address"].ToString() + "','" + dr["lc"].ToString() + "','" + dr["lx"].ToString() + "','" + dr["dw"].ToString() + "','" + dr["ssz"].ToString() + "','" + dr["StatName"].ToString() + "','"+ DateTime.Now.ToString() +"')",MySQLHelper.ConnStr);
                }
            }
        }
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter(); formatter.Serialize(ms, obj); return ms.GetBuffer();
            }
        }
        /// <summary>
        /// 光纤测温
        /// </summary>
        private void GQCW()
        {
            while (true)
            {
                try
                {
                    string sql = "select AreaName,MaxValue,MaxValuePos,MinValue,MinValuePos,AverageValue from FiberAreaRtDataTbl";
                    DataTable dt = DbHelperSQLs.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string AreaName = dt.Rows[i]["AreaName"].ToString();
                            string MaxValue = dt.Rows[i]["MaxValue"].ToString();
                            string MinValue = dt.Rows[i]["MinValue"].ToString();
                            string AverageValue = dt.Rows[i]["AverageValue"].ToString();
                            update(Convert.ToDecimal(MaxValue).ToString("0.00"), Convert.ToDecimal(MinValue).ToString("0.00"), Convert.ToDecimal(AverageValue).ToString("0.00"), AreaName);
                        }

                    }
                }
                catch(Exception ex)
                {
                    LogHelper.AddLog(ex.ToString() + DateTime.Now.ToString(), "光纤测温日志");
                }
                Thread.CurrentThread.Join(1000);
            }
        }

        

        /// <summary>
        /// 光纤测温修改方法
        /// </summary>
        /// <returns></returns>
        private void update(string Max, string Min,string AVG,string name)
        {
            string str = $"update gqcw set Max_Mum = '{Max}', Min_Value = '{Min}', AVG = '{AVG}',dataOfTime = '{DateTime.Now}' where  CY_Name = '{name}'";
            MySQLHelper.Run_SQL(str, MySQLHelper.ConnStr);
        }

        private void Rettou()
        {
            //实时人员位置信息同步
            string selsql = "select ID, PepoleNumber,CurrentStation,OriginStation,DownWellTime,OriginTime,CurrentTime,RemarkFirst,RemarkSecond,TR_LastUpdateTime from RealData";
            DataTable dt = DbHelperSQL.GetDataTable(selsql);
            if (dt.Rows.Count > 0)
            {
                selsql = "DELETE FROM realdata";
                MySQLHelper.Run_SQL(selsql, MySQLHelper.ConnStr);
                int curpeoplenums = dt.Rows.Count;
                decimal totaltimes = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //同步数据  新增或者修改
                    DataRow dr = dt.Rows[i];
                    string ID = dr["ID"].ToString();
                    string PepoleNumber = dr["PepoleNumber"].ToString();
                    string CurrentStation = dr["CurrentStation"].ToString();
                    string OriginStation = dr["OriginStation"].ToString();
                    string DownWellTime = dr["DownWellTime"].ToString();
                    string OriginTime = dr["OriginTime"].ToString();
                    string CurrentTime = dr["CurrentTime"].ToString();
                    string RemarkFirst = dr["RemarkFirst"].ToString();
                    string RemarkSecond = dr["RemarkSecond"].ToString();
                    string TR_LastUpdateTime = dr["TR_LastUpdateTime"].ToString();
                    totaltimes += this.JiSuanFenZhong(DownWellTime,DateTime.Now.ToString());
                    if(string.IsNullOrEmpty(TR_LastUpdateTime))
                    {
                        TR_LastUpdateTime = DateTime.Now.ToString();
                    }
                    string sqls5 = "";
                    sqls5 = $"INSERT INTO realdata(ID,PepoleNumber,CurrentStation,OriginStation,DownWellTime,OriginTime,CurrentTime,RemarkFirst,RemarkSecond,TR_LastUpdateTime)VALUES('{ID}','{PepoleNumber}','{CurrentStation}','{OriginStation}','{DownWellTime}','{OriginTime}','{CurrentTime}','{RemarkFirst}','{RemarkSecond}','{TR_LastUpdateTime}');";
                    MySQLHelper.Run_SQL(sqls5, MySQLHelper.ConnStr);
                    this.JiSuanTimeSpan(PepoleNumber, DownWellTime);
                }
                this.UpdatePeopleNums(curpeoplenums, totaltimes);
            } 
            dt.Clear();
            dt.Dispose();
        }

        /// <summary>
        /// 计算两个时间间隔分钟
        /// </summary>
        /// <param name="downWellTime"></param>
        /// <param name="nowtime"></param>
        /// <returns></returns>
        private decimal JiSuanFenZhong(string downWellTime, string nowtime)
        {
            DateTime dt1 = Convert.ToDateTime(nowtime);
            DateTime dt2 = Convert.ToDateTime(downWellTime);
            TimeSpan ts = dt1 - dt2;
            return Convert.ToInt32(ts.TotalMinutes);
        }

        private void UpdatePeopleNums(int curpeoplenums, decimal totaltimes)
        {
            DateTime curtime = DateTime.Now;
            //先判断当前这个人在每日下井时长统计表存在当天记录
            string selsql = "SELECT sysId,SumPeople FROM downholetimetable where Date>='" + curtime.ToShortDateString() + " 00:00:00' and Date<='" + curtime.ToShortDateString() + " 23:59:59'";
            DataTable dt = MySQLHelper.Get_DataTable(selsql,MySQLHelper.ConnStr,"ds");
            if (dt.Rows.Count > 0)
            {
                    //有记录更新人数 如果当前人数大于记录里面的人数，则更新，否则，不做更新操作
                int yuantounums = Convert.ToInt32(dt.Rows[0]["SumPeople"].ToString());

                if(curpeoplenums> yuantounums)
                {
                  //更新操作
                 MySQLHelper.Run_SQL("update downholetimetable set SumPeople='"+ curpeoplenums.ToString() + "',SumDate='" + totaltimes.ToString() + "' where Date>='" + curtime.ToShortDateString() + " 00:00:00' and Date<='" + curtime.ToShortDateString() + " 23:59:59'", MySQLHelper.ConnStr);
                }
            }
            else
            {
                string sysid = Guid.NewGuid().ToString();
                string curdate = curtime.ToShortDateString();
                //没记录，则新增记录
                MySQLHelper.Run_SQL($"INSERT INTO downholetimetable(sysId,StaffId,Date,SumDate,SumPeople)VALUES('{sysid}','','{curdate}','{totaltimes.ToString()}','{curpeoplenums.ToString()}');", MySQLHelper.ConnStr);
            }
            dt.Clear();
            dt.Dispose();
        }

        //判断表
        private void JiSuanTimeSpan(string pepoleNumber,string DownWellTime)
        {

            //先判断当前这个人在每日下井时长统计表存在当天记录
            string selsql = "SELECT RyId FROM mk_uphple where RyId='" + pepoleNumber + "'";
            DataTable dt = MySQLHelper.Get_DataTable(selsql,MySQLHelper.ConnStr,"ds");
            if(dt.Rows.Count>0)
            {

                DateTime curtime = DateTime.Now;
                DateTime dt1 = DateTime.Now;
                DateTime dt2 = Convert.ToDateTime(DownWellTime);
                TimeSpan ts = dt1 - dt2;
                int fenzhongs = Convert.ToInt32(ts.TotalMinutes);
                //更新时长
                MySQLHelper.Run_SQL("update mk_uphple set Total='"+ fenzhongs +"' where RyId='" + pepoleNumber+"'", MySQLHelper.ConnStr);
            }
            else
            {
                //新增记录  时长为1
                MySQLHelper.Run_SQL($"insert into mk_uphple(RyId,UpholeTime,Total) values ('{pepoleNumber}','{DownWellTime}',1)", MySQLHelper.ConnStr);
            }
            dt.Clear();
            dt.Dispose();
        }

        private void Toume()
        {
            //职务信息同步
            string selsql = "select PositionName,PositionNumber from JobPosition";
            DataTable dt = DbHelperSQL.GetDataTable(selsql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //同步数据  新增或者修改
                    DataRow dr = dt.Rows[i];
                    string PositionName = dr["PositionName"].ToString();
                    string PositionNumber = dr["PositionNumber"].ToString();
                    string sqls4 = "";
                    //先判断是否存在改记录
                    if (ifexist4(PositionNumber))
                    {
                        //更新
                        sqls4 = "update set PositionName='" + PositionName + "'where PositionNumber='" + PositionNumber + "'";
                    }
                    else
                    {
                        //新增p 
                        sqls4 = $"INSERT INTO jobposition(PositionName,PositionNumber)VALUES('{PositionName}','{PositionNumber}');";
                    }
                    MySQLHelper.Run_SQL(sqls4, MySQLHelper.ConnStr);
                }
            }
            dt.Clear();
            dt.Dispose();
        }

        private void Datebine()
        {
            //工种信息同步
            string selsql = "select TypeOfWorkName,typeofworkNumber from TypeOfWork";
            DataTable dt = DbHelperSQL.GetDataTable(selsql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //同步数据  新增或者修改
                    DataRow dr = dt.Rows[i];
                    string TypeOfWorkName = dr["TypeOfWorkName"].ToString();
                    string typeofworkNumber = dr["typeofworkNumber"].ToString();
                    string sqls3 = "";
                    //先判断是否存在改记录
                    if (ifexist3(typeofworkNumber))
                    {
                        //更新
                        sqls3 = "update set TypeOfWorkName='" + TypeOfWorkName + "' where typeofworkNumber='" + typeofworkNumber + "'";
                    }
                    else
                    {
                        //新增
                        sqls3 = $"INSERT INTO typeofwork(TypeOfWorkName,typeofworkNumber)VALUES('{TypeOfWorkName}','{typeofworkNumber}');";
                    }
                    MySQLHelper.Run_SQL(sqls3, MySQLHelper.ConnStr);
                }
            }
            dt.Clear();
            dt.Dispose();
        }

        private void Reauet()
        {
            //打卡机信息同步
            string selsql = "select StationNumber,StationAddress,StationStatus,IpAddress from Deviceinformation";
            DataTable dt = DbHelperSQL.GetDataTable(selsql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //同步数据  新增或者修改
                    DataRow dr = dt.Rows[i];
                    string StationNumber = dr["StationNumber"].ToString();
                    string StationAddress = dr["StationAddress"].ToString();
                    string StationStatus = dr["StationStatus"].ToString();
                    string IpAddress = dr["IpAddress"].ToString();
                    string sqls2 = "";
                    //先判断是否存在改记录
                    if (ifexist2(StationNumber))
                    {
                        //更新
                        sqls2 = "update set StationAddress='" + StationAddress + "',StationStatus='" + StationStatus + "',IpAddress='" + IpAddress + "' where StationNumber='" + StationNumber + "'";
                    }
                    else
                    {
                        //新增
                        sqls2 = $"INSERT INTO deviceinformation(StationNumber,StationAddress,StationStatus,IpAddress)VALUES('{StationNumber}','{StationAddress}','{StationStatus}','{IpAddress}');";
                    }
                    MySQLHelper.Run_SQL(sqls2, MySQLHelper.ConnStr);
                }
            }
            dt.Clear();
            dt.Dispose();
        }

        private void DepartTongbu()
        {
            //部门信息同步
            string selsql = "select DepartmentName,DepartmentNumber from department";
            DataTable dt = DbHelperSQL.GetDataTable(selsql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //同步数据  新增或者修改
                    DataRow dr = dt.Rows[i];
                    string DepartmentName = dr["DepartmentName"].ToString();
                    string DepartmentNumber = dr["DepartmentNumber"].ToString();
                    string sqls1 = "";
                    //先判断是否存在改记录
                    if (ifexist1(DepartmentName))
                    {
                        //更新
                        sqls1 = "update set DepartmentName='" + DepartmentName + "'where DepartmentNumber='" + DepartmentNumber + "'";
                    }
                    else
                    {
                        //新增
                        sqls1 = $"insert into department(DepartmentName,DepartmentNumber)values('{DepartmentName}', '{DepartmentNumber}'); ";
                }
                    MySQLHelper.Run_SQL(sqls1, MySQLHelper.ConnStr);
                }
            }
            dt.Clear();
            dt.Dispose();
        }

        private void PeopleTongbu()
        {
            //人员信息同步

            string selsql = "select ID,PepoleNumber,PepoleExNumber,DepartNumber,TypeOfWorkNumber,JobNumber,PepoleName from StaffInformation";
            DataTable dt = DbHelperSQL.GetDataTable(selsql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //同步数据  新增或者修改
                    DataRow dr = dt.Rows[i];
                    string ID = dr["ID"].ToString();
                    string PepoleNumber = dr["PepoleNumber"].ToString();
                    string PepoleExNumber = dr["PepoleExNumber"].ToString();
                    string DepartNumber = dr["DepartNumber"].ToString();
                    string TypeOfWorkNumber = dr["TypeOfWorkNumber"].ToString();
                    string JobNumber = dr["JobNumber"].ToString();
                    string PepoleName = dr["PepoleName"].ToString();
                    string sqls = "";
                    //先判断是否存在改记录
                    if (ifexist(PepoleNumber))
                    {
                        //更新
                        sqls = "update set PepoleExNumber='" + PepoleExNumber + "', DepartNumber='" + DepartNumber + "',TypeOfWorkNumber='" + TypeOfWorkNumber + "' JobNumber='" + JobNumber + "',PepoleName='" + PepoleName + "',updatetime='"+ DateTime.Now.ToString() +"' where PepoleNumber='" + PepoleNumber + "'";
                    }
                    else
                    {
                        //新增
                        sqls = $"insert into staffinformation(ID, PepoleNumber, PepoleExNumber, DepartNumber, TypeOfWorkNumber, JobNumber, PepoleName) values('{ID}', '{PepoleNumber}','{PepoleExNumber}','{DepartNumber}','{TypeOfWorkNumber}','{JobNumber}','{PepoleName}'); ";
                    }
                    MySQLHelper.Run_SQL(sqls, MySQLHelper.ConnStr);
                }
            }
            dt.Clear();
            dt.Dispose();
        }

        private bool ifexist(string PepoleNumber)
        {
            string sqls = "select ID PepoleNumber from staffinformation where PepoleNumber='" + PepoleNumber + "'";
            int nums = MySQLHelper.Get_DataTable(sqls, MySQLHelper.ConnStr, "ds").Rows.Count;
            if (nums > 0)
                return true;
            else
                return false;
        }

        private bool ifexist1(string DepartmentName)
        {
            string sqls1 = "select DepartmentName from department where DepartmentName= '" + DepartmentName + "'";
            int nums = MySQLHelper.Get_DataTable(sqls1, MySQLHelper.ConnStr, "ds").Rows.Count;
            if (nums > 0)
                return true;
            else
                return false;
        }
        private bool ifexist2(string StationNumber)
        {
            string sqls2 = "select StationNumber from deviceinformation where StationNumber= '" + StationNumber + "'";
            int nums = MySQLHelper.Get_DataTable(sqls2, MySQLHelper.ConnStr, "ds").Rows.Count;
            if (nums > 0)
                return true;
            else
                return false;
        }
        private bool ifexist3(string TypeOfWorkName)
        {
            string sqls3 = "select TypeOfWorkName from typeofwork where typeofworkNumber='" + TypeOfWorkName + "'";
            int nums = MySQLHelper.Get_DataTable(sqls3, MySQLHelper.ConnStr, "ds").Rows.Count;
            if (nums > 0)
                return true;
            else
                return false;
        }
        private bool ifexist4(string PositionName)
        {
            string sqls4 = "select PositionName from jobposition where PositionName='" + PositionName + "'";
            int nums = MySQLHelper.Get_DataTable(sqls4, MySQLHelper.ConnStr, "ds").Rows.Count;
            if (nums > 0)
                return true;
            else
                return false;
        }

        private bool ifexist5(string PepoleNumber)
        {
            string sqls5 = "select ID PepoleNumber from realdata where PepoleNumber='" + PepoleNumber + "'";
            int nums = MySQLHelper.Get_DataTable(sqls5, MySQLHelper.ConnStr, "ds").Rows.Count;
            if (nums > 0)
                return true;
            else
                return false;
        }

        private void button_GQCW_Click(object sender, EventArgs e)
        {
            DbHelperSQL.connectionString = "server=192.168.10.206;uid=mchy;pwd=sa;database=testScada;";

            this.GQCW();
        }


        //测试按钮
        private void button2_Click(object sender, EventArgs e)
        {
            this.TXT_Write();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //this.GQCW();
        }

        private void TXT_Write()
        {            
            bool status = false;
            try
            {
                //连接共享文件夹1
                status = FileShare.connectState(@"\\192.168.10.220\LedDisplayFiles\DeviceShow", "administrator", "Mchy123456");   ///Mchy123456
                if (status)
                {
                    DirectoryInfo theFolder = new DirectoryInfo(@"\\192.168.10.220\LedDisplayFiles\DeviceShow");
                    string month = DateTime.Now.Month.ToString();
                    if (month.Length != 2)
                        month = "0" + month;
                    string days = DateTime.Now.Day.ToString();
                    if (days.Length != 2)
                        days = "0" + days;
                    string hour = DateTime.Now.Hour.ToString();
                    if (hour.Length != 2)
                        hour = "0" + hour;
                    string fen = DateTime.Now.Minute.ToString();
                    if (fen.Length != 2)
                        fen = "0" + fen;


                    //测试写文件，拼出完整的路径
                    string LinkContent = "\\LinkContent_" + month + "" + days + "" + hour + "" + fen +".txt";
                    string filename = theFolder.ToString() + LinkContent.Replace(" ", "");
                    FileShare.WriteFiles(filename);
                    
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.ToString()+DateTime.Now.ToString(), "LED屏幕人员信息发送日志");
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //this.TXT_Write();
        }
    }
}