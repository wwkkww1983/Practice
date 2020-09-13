using Caist.Framework.Entity;
using Caist.Framework.Entity.Entity;
using Caist.Framework.Entity.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Caist.Framework.Service
{
    public partial class FrmMian
    {
        private Stopwatch _swPlcHistory = new Stopwatch();
        private PlcConnects _plcStatus = new PlcConnects();
        #region 定时存储PLC数据
        public async Task AlwaysRunPlcRead()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed1;
            timer.Start();
        }

        private void Timer_Elapsed1(object sender, System.Timers.ElapsedEventArgs e)
        {
            ReadPlc_RealTime();

            PlcConnectStatus();
        }

        string[] _systemName = new string[] { "通风", "压风", "皮带", "泵", "局部" };
        //连接状态
        private async Task PlcConnectStatus()
        {
            List<PlcConnect> plcConnects = new List<PlcConnect>();
            foreach (var name in _systemName)
            {
                var sims = _siemensHelpers.FindAll(p => p.DeviceEntity.Name.IndexOf(name) > -1);
                PlcConnect plcConnect = new PlcConnect();
                var flag = 0;
                foreach (var item in sims)
                {
                    plcConnect.SysId = item.DeviceEntity.SystemId;
                    plcConnect.PlcName = name;
                    flag = item.IsConnected() ? 1 : 0;
                    if (flag == 1)
                    {
                        break;
                    }
                }
                plcConnect.PlcStatus = flag;
                plcConnects.Add(plcConnect);
            }
            _plcStatus.PlcConnectStatus = plcConnects;
            var strModel = JsonConvert.SerializeObject(_plcStatus);
            await SendEverySocket(strModel);
        }

        private PlcConnect GetPlcConnect()
        {
            var pct = new PlcConnect()
            {

            };

            return pct;
        }

        private void ReadPlc_RealTime()
        {
            try
            {
                _siemensHelpers.ForEach(helper =>
                {
                    var groups = helper.DeviceEntity.TagGroupsEntities;
                    double val;
                    groups.ForEach(g =>
                    {
                        var tags = g.TagEntities;
                        tags.ForEach(async t =>
                        {
                            var key = $"{g.Name}.{t.Name}";
                            //读取数据
                            double.TryParse(helper.Read(key), out val);
                            //报警数据处理
                            AbnormalDataHandle(val, key, helper.DeviceEntity);
                            //1分钟保存一次到历史记录表
                            if (_swPlcHistory.Elapsed.Minutes == SaveHistoryInterval)
                            {
                                //保存数据到历史记录表
                                await SaveDataToHistoryTab(key, val.ToString(), helper.DeviceEntity, InstructViewEnum.Data);
                                _stopwatch.Restart();
                            }

                        });
                    });
                });
            }
            catch (Exception ex)
            {
                //Common.LogError(ex);
            }
        }

        private async void AbnormalDataHandle(double v, string key, DeviceEntity device)
        {
            var item = PublicEntity.AlarmEntities.Find(p => (p.MaxValue < v || p.MinValue > v) && p.ManipulateModelMark == key);
            if (item != null)
            {
                var list = new List<AlarmContent>();
                list.Add(new AlarmContent()
                {
                    SysId = item.SysId,
                    ModuleId = item.ModuleId,
                    Name = item.SystemName,
                    Message = item.BroadCastContent,
                    Type = true
                });
                var alarmModel = new AlarmModel()
                {
                    Alarm = list
                };
                var strModel = JsonConvert.SerializeObject(alarmModel);
                //保存异常数据到数据库
                await SaveAlarmData(item, v);
                //保存报警数据到历史记录表
                await SaveDataToHistoryTab(key, v.ToString(), device, InstructViewEnum.Alarm);
                await SendEverySocket(strModel);
            }
        }

        /// <summary>
        /// 发送给每一个socket
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private async Task SendEverySocket(string msg)
        {
            foreach (var socket in dic_Sockets)
            {
                //每个socket都推送
                await socket.Value.Send(msg);
            }
        }
        #endregion
    }
}
