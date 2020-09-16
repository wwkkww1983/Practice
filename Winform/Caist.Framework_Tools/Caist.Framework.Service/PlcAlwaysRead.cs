using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Entity;
using Caist.Framework.Entity.Enum;
using Caist.Framework.Service.Control;
using Caist.Framework.ThreadPool;
using Caist.Siemens;
using Newtonsoft.Json;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    public partial class FrmMian
    {
        private Stopwatch _swPlcHistory = new Stopwatch();
        private PlcConnects _plcStatus = new PlcConnects();

        static Dictionary<string, SiemensHelpers> _dictInstructs = new Dictionary<string, SiemensHelpers>();
        static Dictionary<SiemensHelpers, CaistTimer> _dictReconnects = new Dictionary<SiemensHelpers, CaistTimer>();
        #region 定时存储PLC数据
        public async Task AlwaysRunPlcRead()
        {
            #region 一直读取PLC进行相应的逻辑处理
            foreach (var helper in _siemensHelpers)
            {
                CaistTimer timer = new CaistTimer() { Interval = 100 };
                timer.Elapsed += Timer_Elapsed1;
                timer.obj = helper;
                if (!_valuePairs.ContainsKey(helper))
                {
                    _valuePairs.Add(helper, 0);
                }
                timer.Start();
            } 
            #endregion

            #region 定时保存到历史表
            System.Timers.Timer timerHistory = new System.Timers.Timer() { Interval = 1000 };
            timerHistory.Elapsed += TimerHistory_Elapsed;
            timerHistory.Start();
            #endregion
            #region 定时读取PLC连接状态
            System.Timers.Timer timerConnect = new System.Timers.Timer() { Interval = 1000 };
            timerConnect.Elapsed += TimerConnect_Elapsed;
            timerConnect.Start();
            #endregion
            _swPlcHistory.Start();
        }

        private async void TimerConnect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            await PlcConnectStatus();
        }

        private async void TimerHistory_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            await SaveDataForOneMinutes();
        }

        object _lockObj = new object();
        private async void Timer_Elapsed1(object sender, System.Timers.ElapsedEventArgs e)
        {
            var helper = ((CaistTimer)sender).obj;
            //每个单独的索引
            var i = _valuePairs[helper];
            try
            {
                if (helper.IsConnected())
                {
                    //192.168.20.18-DB16.DBD1:0
                    var tempKey = helper.ListInstructs.ElementAt(i);

                    var tempStr = tempKey.Split(':');
                    string val = string.Empty;
                    var uniqueKey = tempStr[0];
                    var key = tempStr[0].Split('-')[1];
                    var dataType = tempStr[1];
                    //true或者false 必须配位short类型
                    try
                    {
                        if (GetSendDataType(dataType) == SendDataType.Short)
                        {
                            val = helper.Read(key);
                        }
                        else
                        {
                            val = helper.Read(key, GetSendDataType(dataType));
                        }

                        #region DataGrid赋值
                        lvPoint.Invoke(new Action(() =>
                        {
                            ListViewItem item = lvPoint.FindItemWithText(uniqueKey);

                            if (item != null)
                            {
                                item.SubItems[4].Text = val;
                            }

                        }));
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Common.LogError(ex);
                    }

                    if (val != "非数字")
                    {
                        //只推送相关的系统ID的值
                        if (helper.DeviceEntity.SystemId == _SystemId)
                        {
                            SendData(val, uniqueKey, dataType, _ClientFlag);
                        }

                        #region 历史记录保存
                        if (val.ToString().HasValue())
                        {
                            var history = new HistoryEntity()
                            {
                                DictId = uniqueKey,
                                DictValue = val.ToString(),
                                TabName = helper.DeviceEntity.TabName,
                                InstructType = (int)InstructViewEnum.Data// 0:数据;1:控制;2:告警;3:启动;4:停止;9:参数设置
                            };
                            lock (_lockObj)
                            {
                                if (_tempDataList.ContainsKey(uniqueKey))
                                {
                                    _tempDataList[uniqueKey] = history;
                                }
                                else
                                {
                                    _tempDataList.Add(uniqueKey, history);
                                }
                            }
                        }

                        #endregion

                        //报警数据处理
                        await AbnormalDataHandle(val, uniqueKey, helper.DeviceEntity, dataType);

                    }
                }
            }
            catch (Exception ex)
            {
                //Common.LogError(ex);
            }
            i++;
            i = helper.ListInstructs.Count == i ? 0 : i;
            _valuePairs[helper] = i;
        }

        private async Task SaveDataForOneMinutes()
        {
            //1分钟保存一次到历史记录表
            if (_swPlcHistory.Elapsed.Minutes == SaveHistoryInterval)
            {
                _swPlcHistory.Restart();
                //保存数据到历史记录表
                await DataServices.SaveHistoryData(_tempDataList.Values.ToList());
                _tempDataList.Clear();
            }
        }

        #region plc连接状态
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
                    if (!item.IsConnected())
                    {
                        ReconnectPlc(item);
                    }
                }
                plcConnect.PlcStatus = flag;
                plcConnects.Add(plcConnect);
            }
            _plcStatus.PlcConnectStatus = plcConnects;
            var strModel = JsonConvert.SerializeObject(_plcStatus);
            await SendEverySocket(strModel);
        }

        private void ReconnectPlc(SiemensHelpers helper)
        {
            if (_dictReconnects.ContainsKey(helper))
            {
                if(!_dictReconnects[helper].Enabled)
                    _dictReconnects[helper].Start();
            }
            else
            {
                CaistTimer timerReconnect = new CaistTimer() { Interval = 1200 };
                
                timerReconnect.Elapsed += TimerReconnect_Elapsed;
                timerReconnect.obj = helper;
                if (!_valuePairs.ContainsKey(helper))
                {
                    _valuePairs.Add(helper, 0);
                }
                timerReconnect.Start();
                _dictReconnects.Add(helper, timerReconnect);
            }
        }

        private void TimerReconnect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var helper = ((CaistTimer)sender).obj;
            try
            {
                if (!helper.IsConnected())
                {
                    ConnectPlc(helper);
                }
                else
                {
                    _dictReconnects[helper].Stop();
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);

                ConnectPlcMsg(helper, "未连接");
            }
        }

        private void ConnectPlc(SiemensHelpers helper)
        {
            var result = helper.Open(helper.DeviceEntity.PLCType, helper.DeviceEntity.Host, short.Parse(helper.DeviceEntity.Port), 0, short.Parse(helper.DeviceEntity.CPU_SlotNO));
            if (!result)
            {
                ConnectPlcMsg(helper, "未连接");
            }
            else
            {
                ConnectPlcMsg(helper, "已连接");
            }
        }

        private void ConnectPlcMsg(SiemensHelpers helper, string msg)
        {
            lvlPlcStatus.Invoke(new Action(() =>
            {
                ListViewItem item = lvlPlcStatus.FindItemWithText(helper.DeviceEntity.Id);
                if (item != null)
                {
                    item.SubItems[2].ForeColor = msg == "未连接" ? Color.Red : Color.Green;
                    item.SubItems[2].Text = msg;
                }
            }));
        }

        #endregion

        private async Task AbnormalDataHandle(string v, string key, DeviceEntity device, string dataType)
        {
            if (GetSendDataType(dataType) != SendDataType.Short)
            {
                double t = 0;
                double.TryParse(v, out t);
                var item = PublicEntity.AlarmEntities.Find(p => (p.MaxValue < t || p.MinValue > t) && p.ManipulateModelMark == key);
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
                    await SaveAlarmData(item, t);
                    //保存报警数据到历史记录表
                    await DataServices.SaveHistoryData(_tempDataList.Values.ToList());
                    await SaveDataToHistoryTab(key, v.ToString(), device, InstructViewEnum.Alarm);
                    await SendEverySocket(strModel);
                }
            }
        }

        /// <summary>
        /// 发送给每一个socket
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private async Task SendEverySocket(string msg)
        {
            if (dic_Sockets != null && dic_Sockets.Count > 0)
            {
                foreach (var socket in dic_Sockets)
                {
                    //每个socket都推送
                    await socket.Value.Send(msg);
                }
            }
        }
        #endregion
    }
}
