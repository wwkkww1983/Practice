using SyncLogic;
using SyncUtil;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    public partial class FrmMian
    {

        readonly int _millSeconds = 60000;
        string _interval;
        Sync sc;
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
                sc.SyncData(ref _res);
                if (_res.HasValue())
                {
                    timerSyncData.Stop();
                    MessageBox.Show(_res);
                }
            }
            catch (Exception ex)
            {
                timerSyncData.Stop();
                MessageBox.Show(ex.Message);
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
                    Task.Run(() =>
                    {
                        sc.SyncData(ref _res);
                        if (_res.HasValue())
                        {
                            this.btnStop_Click(null, null);
                            MessageBox.Show(_res);
                        }
                    });
                }
                catch (Exception ex)
                {
                    timerSyncData.Stop();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("请先设置定时时间！");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var i = txtInterval.Text.Trim();
            if (i.HasValue())
            {
                var path = Common.GetConfigValue("IntervalPath");
                var res = FileOperation.WriteText(path, i);
                _interval = i;
                MessageBox.Show(res);
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
            catch (Exception)
            {
                Task.Run(() => {
                    btnStart.Enabled = true;
                    lblHint.Visible = false;
                });
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
