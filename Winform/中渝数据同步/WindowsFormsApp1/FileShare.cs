using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class FileShare
    {
        public FileShare() { }

        public static bool connectState(string path)
        {
            return connectState(path, "", "");
        }

        public static bool connectState(string path, string userName, string passWord)
        {

            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                //string doslist = @"net use * / del / y";
                //proc.StandardInput.WriteLine(doslist);
                string dosLine = @"net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.ToString(), "日志");
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }


        //read file
        public static void ReadFiles(string path)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(path))
                {
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);

                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.AddLog(e.ToString(), "日志");
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        //写入的内容
        public static void WriteFiles(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    string renshustrj = "六枝特区中渝煤矿当前井下人数：" + RYCXNums()+"人                                        ";
                    renshustrj += "编号  姓名       工种       所在位置        下井时间                      ";
                    string linkContent = renshustrj + RYCX();
                    sw.Write("[{\"moveSpeed\":\"5\",\"fontName\":\"1\",\"fontColor\":\"1\",\"showMode\":\"3\",\"attribute\":\"2\"");
                    sw.Write(",\"linkContent\":\"" + linkContent + "\"");
                    //sw.WriteLine(",\"fontSize\":\"0\",\"deviceUID\":\"08-00-06-73-05-01-c1-cb\",\"stopTime\":\"0\"}]");
                    sw.WriteLine(",\"fontSize\":\"0\",\"deviceUID\":\"08-00-06-73-05-01-c1-cb\",\"stopTime\":\"0\"}]");
                }
                LogHelper.AddLog("发送LED节目成功" + DateTime.Now.ToString(), "LED屏幕人员信息发送日志");
            }
            catch (Exception e)
            {
                LogHelper.AddLog(e.ToString()+DateTime.Now.ToString(), "LED屏幕人员信息发送日志");
            }
        }

        /// <summary>
        /// 人员查询
        /// </summary>
        public static string RYCX()
        {
            string result = "";
            string Sql = "SELECT w.PepoleNumber,m.PepoleName,s.TypeOfWorkName,c.StationAddress,w.DownWellTime FROM realdata w left JOIN staffinformation m ON w.PepoleNumber=m.PepoleNumber left JOIN deviceinformation c ON c.StationNumber=w.CurrentStation left join typeofwork s on m.TypeOfWorkNumber=s.typeofworkNumber";
            DataTable dt = MySQLHelper.Get_DataTable(Sql, MySQLHelper.ConnStr, "");
            if (dt.Rows.Count > 0)
            {
               result = DataTableJson(dt);
               
                return result;
            }
            else
            {
                return result = "{\"code\":100,\"message\":\"无列表数据\"}";
            }
        }
        public static string RYCXNums()
        {
            string Sql = "SELECT distinct ID FROM realdata";
            DataTable dt = MySQLHelper.Get_DataTable(Sql, MySQLHelper.ConnStr, "");
            return dt.Rows.Count.ToString();
        }

        /// <summary>
        /// json方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string curvalues = dt.Rows[i][j].ToString();
                    if (j == 0)
                    {
                        jsonBuilder.Append(curvalues);
                        for (int k = 0; k < 4 - curvalues.Length; k++)
                        {
                            jsonBuilder.Append("  ");
                        }
                    }
                    else if (j == 1)
                    {
                        jsonBuilder.Append(curvalues);
                        for (int k = 0; k < 4 - curvalues.Length; k++)
                        {
                            jsonBuilder.Append("  ");
                        }
                    }
                    else if(j == 2)
                    {
                        jsonBuilder.Append(curvalues);
                        for (int k = 0; k < 8 - curvalues.Length; k++)
                        {
                            jsonBuilder.Append("  ");
                        }
                    }
                    else if(j == 3)
                    {
                        if (curvalues == "副井底" || curvalues == "主井底" || curvalues == "主井口" || curvalues == "风井口" || curvalues == "副井口")
                        {
                            jsonBuilder.Append(curvalues + "             ");
                        }
                        else
                        {
                            jsonBuilder.Append(curvalues);
                            for (int k = 0; k < 12 - curvalues.Length; k++)
                            {
                                jsonBuilder.Append("  ");
                            }
                        }
                    }
                    else if(j == 4)
                    {
                        curvalues = Convert.ToDateTime(curvalues).ToString("yyyy-MM-dd HH:mm:ss");
                        jsonBuilder.Append(curvalues+"      ");
                    }
                }
                //jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }
            //if (dt.Rows.Count > 0)
            //{
            //    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            //}
            return jsonBuilder.ToString();
        }

    }
}
