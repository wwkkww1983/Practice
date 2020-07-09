using Caist.Framework.ThreadPool;
using SyncLogic;
using SyncUtil;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    public partial class FrmMian
    {
        readonly int _millSeconds = 60000;
        string _interval;
        Sync sc;
        string _path = Common.GetConfigValue("LogPath");
        public void Init()
        {
            sc = new Sync();

            IntervalInit();
            timerSyncData.Tick += Timer1_Tick;
        }

        private void IntervalInit()
        {
            var str = Common.GetConfigValue("IntervalPath");
            if (str.HasValue())
            {
                var s = System.Windows.Forms.Application.StartupPath;
                _interval = FileOperation.ReadText(str);
                txtInterval.Text = _interval;
            }
        }

        private int GetInterval()
        {
            return Convert.ToInt32(_interval) * _millSeconds;
        }

        private void Timer1_Tick(object sender, System.EventArgs e)
        {
            try
            {
                Task.Run(async () =>
                {
                    var tp = await sc.SyncDataAsync();
                    _res = tp.Item1;
                    if (_res.HasValue())
                    {
                        ShowErrorLog(_res);
                    }
                });
                //var tp = sc.SyncDataAsync();
                //if (tp.IsCompleted)
                //{
                //    _res = tp.Result.Item1;
                //    if (_res.HasValue())
                //    {
                //        ShowErrorLog(_res);
                //    }
                //    GC.Collect();
                //    GC.WaitForPendingFinalizers();
                //}
            }
            catch (Exception ex)
            {
                ShowErrorLog(ex.Message);
            }
        }
        string _res = string.Empty;
        private void btnStart_Click(object sender, System.EventArgs e)
        {
            if (_interval.HasValue())
            {
                try
                {
                    timerSyncData.Interval = GetInterval();
                    btnStart.Enabled = false;
                    lblHint.Visible = true;
                    timerSyncData.Start();
                    Task.Run(async () =>
                    {
                        var tp = await sc.SyncDataAsync();
                        _res = tp.Item1;
                        if (_res.HasValue())
                        {
                            ShowErrorLog(_res);
                        }
                    });
                }
                catch (Exception ex)
                {
                    ShowErrorLog(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("请先设置定时时间！");
            }
        }

        private void ShowErrorLog(string msg)
        {
            string path = Path.Combine(_path, DateTime.Now.ToString("yyyyMMdd") + ".txt");
            FileOperation.WriteText(path, msg);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var i = txtInterval.Text.Trim();
            if (i.HasValue())
            {
                var path = Common.GetConfigValue("IntervalPath");
                var res = FileOperation.WriteText(path, i, false);
                _interval = i;
                ShowErrorLog(res);
            }
            else
            {
                MessageBox.Show("请填写定时时间！");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                timerSyncData.Stop();
                btnStart.Enabled = true;
                lblHint.Visible = false;
            }
            catch (Exception ex)
            {
                ShowErrorLog(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("确定要终止吗？", "警告", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lblHint.Visible)
            {
                lblwarn.Visible = true;
                lblWaring.Text = "当前程序正在同步中，中途终止可能会导致同步数据出问题；\r\n如果不想再次进行同步，可以点击【定时停止】！";
                e.Cancel = true;
            }
        }
    }
}
