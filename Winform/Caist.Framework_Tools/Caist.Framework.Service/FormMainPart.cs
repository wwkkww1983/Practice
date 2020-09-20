using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.ThreadPool;
using Caist.Framework.Util;
using Newtonsoft.Json;
using SyncLogic;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    public partial class FrmMian
    {
        readonly int _millSeconds = 1000;
        string _interval;
        Sync sc;
        System.Timers.Timer timerSyncData;
        System.Timers.Timer timerSync;
        public void InitSyncData()
        {
            sc = new Sync();

            timerSyncData = new System.Timers.Timer();
            IntervalInit();
            timerSyncData.Elapsed += TimerSyncData_Elapsed;

            timerSync = new System.Timers.Timer();
            timerSync.Interval = GetInterval();
            timerSync.Elapsed += TimerSync_Elapsed;
        }

        private async void TimerSync_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            #region 人员定位定时存历史表
            await SavePeoplePositionToHistory();
            #endregion

            #region 光纤测温定时存历史表
            await SaveFiberToHistory();
            #endregion

            #region 供配电定时存历史表
            await SaveSubStationToHistory();
            #endregion
        }

        private Task SaveSubStationToHistory()
        {
            throw new NotImplementedException();
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

        private void TimerSyncData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                #region 通过HTTP读取安全监控(瓦斯监控)
                if (bool.Parse("IsHttpRead".GetConfigrationStr()))
                {
                    var model = HttpHelper.HttpGet("SecurityMonitorUrl".GetConfigrationStr());
                    model = model.Replace("Value", "ssz");
                    var listModel = JsonConvert.DeserializeObject<List<SecurityMonitorEntity>>(model);
                    DataServices.SaveSecurityMonitorData(listModel);
                }
                #endregion

                Task.Run(async () =>
                {
                    var tp = await sc.SyncDataAsync();
                    _res = tp.Item1;
                    if (_res.HasValue())
                    {
                        Common.LogError(_res);
                    }
                });
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
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
                    timerSync.Start();
                    Task.Run(async () =>
                    {
                        var tp = await sc.SyncDataAsync();
                        _res = tp.Item1;
                        if (_res.HasValue())
                        {
                            Common.LogError(_res);
                        }
                    });
                }
                catch (Exception ex)
                {
                    Common.LogError(ex);
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
                FileOperation.WriteText(path, i, false);
                _interval = i;
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
                timerSync.Stop();
                timerSyncData.Stop();
                btnStart.Enabled = true;
                lblHint.Visible = false;
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
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

        private async Task SavePeoplePositionToHistory()
        {
           var pepoleEntities = await GetPepolePositionList();
            //插入人员定位历史表
            await DataServices.InsertMk_People_Position(pepoleEntities);
        }

        private async Task SaveFiberToHistory()
        {
           var fibers = await GetFiberList();
            //插入光纤测温历史表
            await DataServices.InsertMk_Cable_Thermometry(fibers);
        }
    }
}
