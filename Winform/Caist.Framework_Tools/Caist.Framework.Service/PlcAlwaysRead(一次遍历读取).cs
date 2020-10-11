using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Entity;
using Caist.Framework.Entity.Enum;
using Caist.Framework.ThreadPool;
using Caist.Siemens;
using Newtonsoft.Json;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            timer.Interval = 2100;
            timer.Elapsed += Timer_Elapsed1;
            timer.Start();
            _swPlcHistory.Start();
        }

        private async void Timer_Elapsed1(object sender, System.Timers.ElapsedEventArgs e)
        {
            await ReadPlc_RealTime();
            await PlcConnectStatus();
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

        object _lockObj = new object();
        private async Task ReadPlc_RealTime()
        {
            try
            {
                foreach (var helper in _siemensHelpers)
                {
                    var groups = helper.DeviceEntity.TagGroupsEntities;
                    foreach (var g in groups)
                    {
                        var tags = g.TagEntities;
                        foreach (var t in tags)
                        {
                            //if ("192.168.20.16,192.168.20.18".IndexOf(helper.DeviceEntity.Host) == -1)
                            //    continue;

                            string val = string.Empty;
                            var uniqueKey = $"{helper.DeviceEntity.Host}-{g.Name}.{t.Name}";
                            var key = $"{g.Name}.{t.Name}";
                            //true或者false 必须配位short类型
                            try
                            {
                                if (GetSendDataType(t.DataType) == SendDataType.Short)
                                {
                                    val = helper.Read(key);
                                }
                                else
                                {
                                    val = helper.Read(key, GetSendDataType(t.DataType));
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
                            catch (Exception ex) {
                                Common.LogError(ex);
                            }

                            if (val != "非数字")
                            {
                                //只推送相关的系统ID的值
                                if (helper.DeviceEntity.SystemId == _SystemId)
                                {
                                    SendData(val, uniqueKey, t.DataType, _ClientFlag);
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

                                //1分钟保存一次到历史记录表
                                if (_swPlcHistory.Elapsed.Minutes == SaveHistoryInterval)
                                {
                                    _swPlcHistory.Restart();
                                    //保存数据到历史记录表
                                    await DataServices.SaveHistoryData(_tempDataList.Values.ToList());
                                    _tempDataList.Clear();
                                }
                                #endregion

                                //报警数据处理
                                await AbnormalDataHandle(val, uniqueKey, helper.DeviceEntity, t.DataType);

                            }
                        }
                    }
                }
                //_siemensHelpers.ForEach(helper =>
                //{
                //    var groups = helper.DeviceEntity.TagGroupsEntities;
                //    groups.ForEach(g =>
                //    {
                //        var tags = g.TagEntities;
                //        tags.ForEach(async t =>
                //        {
                //            //await Task.Delay(100);
                //            string val = string.Empty;
                //            var uniqueKey = $"{helper.DeviceEntity.Host}-{g.Name}.{t.Name}";
                //            var key = $"{g.Name}.{t.Name}";
                //            //true或者false 必须配位short类型
                //            try
                //            {
                //                if (GetSendDataType(t.DataType) == SendDataType.Short)
                //                {
                //                    val = helper.Read(key);
                //                }
                //                else
                //                {
                //                    val = helper.Read(key, GetSendDataType(t.DataType));
                //                }
                //            }
                //            catch (Exception ex) { }
                //            //只推送相关的系统ID的值
                //            if (helper.DeviceEntity.SystemId == _SystemId)
                //            {
                //                await Task.Run(new Action(() =>
                //                {
                //                    SendData(val, uniqueKey, t.DataType);
                //                }));
                //            }
                //            //报警数据处理
                //            await AbnormalDataHandle(val, uniqueKey, helper.DeviceEntity, t.DataType);

                //            var history = new HistoryEntity()
                //            {
                //                DictId = uniqueKey,
                //                DictValue = val.ToString(),
                //                TabName = helper.DeviceEntity.TabName,
                //                InstructType = (int)InstructViewEnum.Data// 0:数据;1:控制;2:告警;3:启动;4:停止;9:参数设置
                //            };
                //            //if (_tempDataList.ContainsKey(uniqueKey))
                //            //{
                //            //    _tempDataList[uniqueKey] = history;
                //            //}
                //            //else
                //            //{
                //            _tempDataList.AddOrUpdate(uniqueKey, history, (keys, oldValue) => history);
                //            //}

                //            //1分钟保存一次到历史记录表
                //            if (_swPlcHistory.Elapsed.Minutes == SaveHistoryInterval)
                //            {
                //                //保存数据到历史记录表
                //                await DataServices.SaveHistoryData(_tempDataList.Values.ToList());
                //                _stopwatch.Restart();
                //                _tempDataList.Clear();
                //            }

                //        });
                //    });
                //});
            }
            catch (Exception ex)
            {
                //Common.LogError(ex);
            }
        }

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
