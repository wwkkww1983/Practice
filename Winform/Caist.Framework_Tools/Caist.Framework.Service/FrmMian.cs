using AutoMapper;
using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Entity;
using Caist.Framework.Entity.Enum;
using Caist.Framework.Service.Control;
using Caist.Framework.ThreadPool;
using Caist.Framework.Util;
using Caist.Siemens;
using Fleck;
using Newtonsoft.Json;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caist.Framework.Service
{
    public partial class FrmMian : Form
    {
        /**{"key": "value"}**/
        #region 变量定义
        private Stopwatch sw = new Stopwatch();
        private TimeSpan ts = new TimeSpan();
        private List<PepolePositionEntity> _pepolePostionEntities = new List<PepolePositionEntity>();
        private List<FiberEntity> _fiberEntities = new List<FiberEntity>();
        private List<SiemensHelpers> _siemensHelpers = new List<SiemensHelpers>();
        private List<CaistTimer> _timers = new List<CaistTimer>();
        private List<DataBaseTimer> _dataBaseTimers = new List<DataBaseTimer>();
        string _socketTimerValue = "WebSocketTimer".GetConfigrationStr();
        public static FrmMian MyForm;
        FormSubstation _formSubstation;
        Dictionary<SiemensHelpers, int> _valuePairs = new Dictionary<SiemensHelpers, int>();
        public Dictionary<DeviceEntity, List<string>> DictInstructs { get; set; }
        //1分钟保存一次到历史记录表
        int SaveHistoryInterval = 1;
        /// <summary>
        /// 用于前端传入系统ID
        /// </summary>
        string _SystemId;
        string _ClientFlag;
        /// <summary>
        /// 用于每分钟存入一次数据库临时对象
        /// </summary>
        static Dictionary<string, HistoryEntity> _tempDataList = new Dictionary<string, HistoryEntity>();
        #endregion

        #region 消息推送实例
        //客户端url以及其对应的Socket对象字典
        IDictionary<string, IWebSocketConnection> dic_Sockets = new Dictionary<string, IWebSocketConnection>();
        //创建
        WebSocketServer server = null;
        #endregion

        #region 初始化
        public FrmMian()
        {
            InitializeComponent();
            MyForm = this;
            _formSubstation = new FormSubstation(this);
            Common.InitLog("PlcLogPath".GetConfigrationStr());
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMian_Load(object sender, EventArgs e)
        {
            try
            {
                InitWebsocketSend();

                //底部运行时间
                this.sw.Start();
                System.Timers.Timer timing = new System.Timers.Timer(1000);
                timing.Elapsed += Timing_Elapsed;
                timing.Start();

                //加载plc列表
                DataServices.LoadDataDevice(TreeDevice.Nodes);
                //加载PLC内存块长度配置信息列表
                DataServices.LoadDataTagGroup();
                //加载PLC后台配置内存地址指向表
                DataServices.LoadDataTag();
                //加载预警数据配置
                DataServices.LoadAlarmData();
                //加载告警数据配置
                DataServices.LoadAlertData();
                //初始化SiemensHelpers
                SiemensInit();
                //初始化plc 指令列表
                this.GetInit();
                //初始化PLC状态
                InitPlcStatus();
                //对象映射
                MappingModel();

                //数据同步初始化
                InitSyncData();
                //初始化mqtt配置信息
                LoadMqtt();
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }
        private void DataBaseTimerInit(RequestType requestType, string clientFlag)
        {
            #region 初始化websocket配置
            var interval = _socketTimerValue.HasValue() ? _socketTimerValue : "1000";
            if (_dataBaseTimers.FindIndex(p => p.ClientFlag == clientFlag && p.Tag == requestType) == -1)
            {
                var timer = new DataBaseTimer();
                timer.Interval = Convert.ToInt32(interval);
                timer.Elapsed += timer_Tick;
                timer.Tag = requestType;
                timer.ClientFlag = clientFlag;
                _dataBaseTimers.Add(timer);
                timer.Start();
            }
            else
            {
                _dataBaseTimers.FindAll(p => p.ClientFlag == clientFlag && p.Tag == requestType).ForEach(f =>
                {
                    f.Start();
                });
            }
            #endregion
        }

        private void Timing_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ts = sw.Elapsed;
            Task.Run(new Action(() =>
            {
                txttiming.Text = string.Format("{0}:{1}:{2}", ts.Hours.ToString("D5"), ts.Minutes.ToString("D2"), ts.Seconds.ToString().PadLeft(2, '0'));
            }));
        }

        private void InitWebsocketSend()
        {
            var flag = "ShowWebSocketSend".GetConfigrationStr();
            if (flag == "1")
            {
                toolStrip1.Visible = true;
                var ip = "WebSocketAddress".GetConfigrationStr();
                if (ip.HasValue())
                {
                    webAddress.Text = ip.Substring(0, ip.IndexOf(":"));
                    webPort.Text = ip.Substring(ip.IndexOf(":") + 1);
                }
            }
        }

        #region 定时器相关操作
        //socket 推送plc数据
        private void timer_Tick(object sender, EventArgs e)
        {
            var send = (DataBaseTimer)sender;
            switch (send.Tag)
            {
                case RequestType.PepolePosition:
                    SendPepolePostionData(send.ClientFlag);
                    break;
                case RequestType.SubStation:
                    SendSubStationData(send.ClientFlag);
                    break;
                case RequestType.Fiber:
                    SendFiberData(send.ClientFlag);
                    break;
            }
        }
        private void StopTimers(string clientFlag)
        {
            #region 先暂停之前所有定时
            StopDataBaseTimers(clientFlag);
            StopPlcTimers(clientFlag);
            #endregion
        }

        private void StopDataBaseTimers(string clientFlag)
        {
            _dataBaseTimers.FindAll(t => t.ClientFlag == clientFlag).ForEach(p =>
              {
                  p.Stop();
              });
        }

        private void StopPlcTimers(string clientFlag)
        {
            _timers.FindAll(t => t.ClientFlag == clientFlag).ForEach(p =>
            {
                p.Stop();
            });
        }
        #endregion

        private void SiemensInit()
        {
            DictInstructs = new Dictionary<DeviceEntity, List<string>>();
            PublicEntity.DeviceEntities.ForEach(d =>
            {
                SiemensHelpers siemensHelper = new SiemensHelpers();
                siemensHelper.DeviceEntity = d;
                siemensHelper.IP = d.Host;
                siemensHelper.Port = d.Port;
                siemensHelper.Init();
                DictInstructs.Add(d, siemensHelper.ListInstructs);
                _siemensHelpers.Add(siemensHelper);
            });
        }

        /// <summary>
        /// 加载指令
        /// </summary>
        public void GetInit()
        {
            try
            {
                foreach (var siemens in _siemensHelpers)
                {
                    var groups = siemens.DeviceEntity.TagGroupsEntities;
                    foreach (TagGroupsEntity group in groups)
                    {
                        var tags = group.TagEntities;
                        foreach (TagEntity tag in tags)
                        {
                            string Key = string.Empty;
                            if (group.Name.StartsWith("DB"))
                            {
                                Key = string.Format("{0}.{1}", group.Name, tag.Name);
                            }
                            else
                            {//其它不需要中间的点连接
                                Key = string.Format("{0}{1}", group.Name, tag.Name);
                            }
                            string Name = string.Format("{0}-{1}", siemens.IP, Key);
                            ListViewItem lvitem = new ListViewItem(Name);
                            lvitem.ToolTipText = Key;
                            lvitem.SubItems.Add(tag.Desc);
                            lvitem.SubItems.Add(tag.DataType);
                            lvitem.SubItems.Add(Key);
                            lvitem.SubItems.Add("");
                            lvitem.SubItems.Add(Extensions.ConvertOutPutEnum(int.Parse(tag.Output)));
                            lvitem.Tag = tag;
                            lvPoint.Items.Add(lvitem);
                            lvPoint.Columns[0].Width = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }

        /// <summary>
        /// 加载机器列表
        /// </summary>
        public void InitPlcStatus()
        {
            try
            {
                foreach (var helper in _siemensHelpers)
                {
                    var str = string.Format("【{0}-{1}:{2}】", helper.DeviceEntity.Name, helper.DeviceEntity.Host, helper.DeviceEntity.Port);
                    ListViewItem lvitem = new ListViewItem(helper.DeviceEntity.Id);
                    lvitem.SubItems.Add(str);
                    lvitem.SubItems.Add("");
                    lvlPlcStatus.Items.Add(lvitem);
                }
            }
            catch (Exception)
            {
            }
        }

        private void MappingModel()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<FiberEntity, FiberContent>();
                cfg.CreateMap<SubStationEntity, SubStationContent>();
                cfg.CreateMap<PepolePositionEntity, PepolePostionContent>();
            });
        }
        #endregion

        #region PLC读取
        /// <summary>
        /// 开启PLC连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPLCStrats_Click(object sender, EventArgs e)
        {
            try
            {
                Task.Run(new Action(async () =>
                {
                    //启动Plc连接
                    await PlcsStart();
                }));
                Task.Run(new Action(() =>
                {
                    webSocketStart_Click(null, null);
                }));

                //定时查询告警信息
                //IntervalCallAlert();

                btnPLCStrats.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }
        //定时查询告警信息
        private void IntervalCallAlert()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        /// <summary>
        /// 定时查询plc告警信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            PlcAlertStart();
        }
        private async Task PlcsStart()
        {
            _siemensHelpers.ForEach(helper =>
            {
                try
                {
                    ConnectPlc(helper);
                }
                catch (Exception ex)
                {
                    Common.LogError(ex);

                    ConnectPlcMsg(helper, "未连接");
                }
            });
            await AlwaysRunPlcRead();
        }

        private SendDataType GetSendDataType(string dataType)
        {
            SendDataType type = SendDataType.Float;
            switch (dataType)
            {
                case "0":
                    type = SendDataType.Short;
                    break;
                case "1":
                    type = SendDataType.UInt32;
                    break;
                case "2":
                    type = SendDataType.Ushort;
                    break;
                case "3":
                    type = SendDataType.Int;
                    break;
                case "4":
                    type = SendDataType.Float;
                    break;
                default:
                    break;
            }
            return type;
        }

        StringBuilder _sb = new StringBuilder();
        object _lockObject = new object();
        private void SendData(string v, string key, string dataType, string clientFlag)
        {
            //true或者false 必须配位short类型
            if (GetSendDataType(dataType) != SendDataType.Short)
            {
                double val = 0;
                double.TryParse(v, out val);
                var item = PublicEntity.AlarmEntities.Find(p => (p.MaxValue < val || p.MinValue > val) && p.ManipulateModelMark == key);
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
                    var str = JsonConvert.SerializeObject(alarmModel);
                    SendEverySocket(str);
                }
            }
            lock (_lockObject)
            {
                _sb.Append("{");
                if (GetSendDataType(dataType) == SendDataType.Short)
                {
                    var res = v.ToLower() == "false" ? 0 : 1;
                    _sb.AppendFormat("\"{0}\":\"{1}\"", key.Replace("\t", string.Empty), res);
                }
                else
                {
                    //if (v != "非数字")
                    //{
                    _sb.AppendFormat("\"{0}\":\"{1}\"", key.Replace("\t", string.Empty), v.IndexOf(".") > -1 ? double.Parse(v).ToString("#0.00") : v);
                    //}
                }
                _sb.Append("}");
                SendMessage(_sb.ToString(), clientFlag);
                _sb.Clear();
            }
        }

        private async Task<bool> SaveDataToHistoryTab(string key, string v, DeviceEntity device, InstructViewEnum instructType)
        {
            var history = new HistoryEntity()
            {
                DictId = key,
                DictValue = v.ToString(),
                TabName = device.TabName,
                InstructType = (int)instructType// 0:数据;1:控制;2:告警;3:启动;4:停止;9:参数设置
            };
            return await DataServices.SaveHistoryData(history);
        }
        private async Task<bool> SaveCommandDataToHistoryTab(InstructModel instructModel, DeviceEntity device)
        {
            var history = new HistoryEntity()
            {
                DictId = instructModel.Instruct,
                DictValue = instructModel.Value.ToString(),
                TabName = device.TabName,
                InstructType = (int)instructModel.InstructType
            };
            return await DataServices.SaveHistoryData(history);
        }

        /// <summary>
        /// 保存取值plc数据异常的数据
        /// </summary>
        /// <param name="item"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private async Task<bool> SaveAlarmData(AlarmEntity item, double v)
        {
            var alarm = new AlarmRecordEntity()
            {
                alarm_id = item.ModuleId,
                alarm_time = DateTime.Now,
                alarm_time_length = "0",
                alarm_reason = item.BroadCastContent,
                alarm_value = v.ToString()
            };
            return await DataServices.SaveAlarmData(alarm);
        }

        /// <summary>
        /// 保存后台配置告警读取的异常
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        private async Task<bool> SaveAlarmData(long itemId, string reason)
        {
            var alarm = new AlarmRecordEntity()
            {
                alarm_id = itemId,
                alarm_time = DateTime.Now,
                alarm_reason = reason
            };
            return await DataServices.SaveAlarmData(alarm);
        }

        //1、整合推送到plc采集； 2、判断报警信息（存入报警表，发送特定消息给前端）；3、整合数据同步进来；
        //1、光纤测温websocket推送；2、供电站webscoket推送；3、建历史表；4、svn 外网映射；5、PLC联调

        #endregion

        #region PLC告警（点表点位设置的报警）

        public void PlcAlertStart()
        {
            //构建告警返回模型
            var list = new List<AlarmContent>();
            PublicEntity.AlertEntities.ForEach(alert =>
            {
                _siemensHelpers.ForEach(async helper =>
                {
                    //读取数据
                    var str = helper.Read(alert.Command);
                    if (str == "1")//异常产生
                    {
                        list.Add(new AlarmContent()
                        {
                            SysId = alert.SystemId,
                            ModuleId = alert.ModuleId,
                            Name = alert.SystemName,
                            Message = alert.ManipulateModelName,
                            Type = true
                        });
                        var alarmModel = new AlarmModel()
                        {
                            Alarm = list
                        };
                        var msg = JsonConvert.SerializeObject(alarmModel);
                        list.Clear();
                        foreach (var item in dic_Sockets)
                        {
                            //每个socket都推送
                            await item.Value.Send(msg);
                        }
                        //保存告警数据到数据库
                        await SaveAlarmData(alert.ItemId, alert.ManipulateModelName);
                    }
                });
            });
        }
        #endregion

        #region WebSocket消息推送
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webSocketStart_Click(object sender, EventArgs e)
        {
            try
            {
                var url = "WebSocketAddress".GetConfigrationStr();
                string loaction = string.Format("ws://{0}", url);
                server = new WebSocketServer(loaction);//监听所有的的地址
                server.RestartAfterListenError = true;//出错后进行重启
                if (url.HasValue())
                {
                    webStart.Enabled = false;
                    webStop.Enabled = true;
                    webSend.Enabled = true;
                    //开始监听
                    server.Start(socket =>
                    {
                        socket.OnOpen = () =>   //连接建立事件
                        {
                            //获取客户端网页的url
                            string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                            dic_Sockets.Add(clientUrl, socket);
                            WebSocketMessage("|服务器:和客户端网页:" + clientUrl + " 建立WebSock连接！");
                        };
                        socket.OnClose = () =>  //连接关闭事件
                        {
                            string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                            if (dic_Sockets != null && dic_Sockets.Count > 0)
                            {
                                //移除websocket
                                dic_Sockets.Remove(clientUrl);
                            }
                            StopTimers(clientUrl);
                            if (_timers != null && _timers.Count > 0)
                            {
                                _timers.RemoveAt(_timers.FindIndex(p => p.ClientFlag == clientUrl));
                            }
                            //移除定时
                            WebSocketMessage("|服务器:和客户端网页:" + clientUrl + " 断开WebSock连接！");
                        };
                        socket.OnMessage = async message =>  //接受客户端网页消息事件
                        {
                            string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                            await PushSpecifiedMsg(message, clientUrl);
                            WebSocketMessage("|服务器:【收到】来客户端网页:" + clientUrl + "的信息：\n" + message);

                        };
                    });
                }
                else
                {
                    WebSocketMessage("请在配置文件中配置IP地址和端口！");
                }
            }
            catch (Exception ex)
            {
                webStart.Enabled = true;
                webStop.Enabled = false;
                webSend.Enabled = false;
                Common.LogError(ex);
            }
        }

        public async Task PushSpecifiedMsg(string message, string clientFlag)
        {
            _ClientFlag = clientFlag;
            if (message.HasValue())
            {
                try
                {
                    var reciveModel = JsonConvert.DeserializeObject<InstructModel>(message);
                    if (reciveModel.RequestType == RequestType.GetPlcValues)//要发送plc 数据值的关键字集合
                    {
                        StopTimers(clientFlag);
                        _SystemId = reciveModel.SystemId;
                    }
                    else if (reciveModel.RequestType == RequestType.PepolePosition)
                    {
                        StopTimers(clientFlag);
                        await SendPepolePostionData(clientFlag, true);
                        DataBaseTimerInit(RequestType.PepolePosition, clientFlag);
                    }
                    else if (reciveModel.RequestType == RequestType.Fiber)
                    {
                        StopTimers(clientFlag);
                        await SendFiberData(clientFlag, true);
                        DataBaseTimerInit(RequestType.Fiber, clientFlag);
                    }
                    else if (reciveModel.RequestType == RequestType.SubStation)
                    {
                        StopTimers(clientFlag);
                        await SendSubStationData(clientFlag);
                        DataBaseTimerInit(RequestType.SubStation, clientFlag);
                    }
                    else
                    {
                        _SystemId = reciveModel.SystemId;
                        FrontCommands(reciveModel, clientFlag);
                    }
                }
                catch (Exception ex)
                {
                    Common.LogError(ex);
                    SendMsg(ex.Message, clientFlag);
                }
            }
            else
            {
                SendMsg("参数命令不能为空！", clientFlag);
            }
        }

        //SiemensHelpers helper = new SiemensHelpers();
        private void FrontCommands(InstructModel reciveModel, string clientFlag)
        {
            /**
             * 统一请求控制面板命令值集合：getCommandValues; 
             * 获取单个控制命令状态值:getCommandValue;
             * 发送单个控制命令:setCommandValue;
             **/
            try
            {
                string ip;
                string port;
                string instruct;
                string first;
                string second;
                SiemensHelpers helper;
                //var flags = helper.Open("S71200", "192.168.20.88", 102, 0, 1);
                //if (!flags)
                //{
                //    MessageBox.Show("连接失败！");
                //    return;
                //}
                var requesType = reciveModel.RequestType;
                if (requesType == RequestType.GetCommandValues)//获取当前系统所有开关的状态
                {
                    List<InstructReturn> instructModelReturns = new List<InstructReturn>();
                    var dtCommands = DataServices.GetSwitcCommands(reciveModel);//获取当前系统命令集合
                    if (dtCommands != null && dtCommands.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCommands.Rows)
                        {
                            ip = dr["paramenter_ip"].ToString();
                            port = dr["paramenter_port"].ToString();
                            var unit = dr["paramenter_unit"].ToString();
                            helper = _siemensHelpers.Find(p => p.DeviceEntity.Host == ip && p.DeviceEntity.Port == port);

                            if (unit == "1")//单控
                            {
                                instruct = dr["paramenter_instruct"].ToString();
                                BuildListToFront(instruct, helper, instructModelReturns, dr, unit, clientFlag);
                            }
                            else//双控
                            {
                                first = dr["paramenter_instruct_start"].ToString();
                                second = dr["paramenter_instruct_end"].ToString();
                                BuildListToFront(first, second, helper, instructModelReturns, dr, unit, clientFlag);
                            }
                        }
                        SendMessage(JsonConvert.SerializeObject(new InstructModelReturns()
                        {
                            InstructModelReturn = instructModelReturns
                        }), clientFlag);
                    }
                    else
                    {
                        SendMessage(JsonConvert.SerializeObject(new InstructModelReturns()
                        {
                            InstructModelReturn = null
                        }), clientFlag);
                    }
                }
                else
                {
                    List<InstructReturn> instructModelReturns;
                    ip = reciveModel.Ip;
                    port = reciveModel.Port;
                    instruct = reciveModel.Instruct;
                    helper = _siemensHelpers.Find(p => p.DeviceEntity.Host == ip && p.DeviceEntity.Port == port);
                    if (requesType == RequestType.GetCommandValue)//单个命令状态查询 getCommandValue
                    {
                        instructModelReturns = new List<InstructReturn>();
                        var dtd = DataServices.GetSingleCommandValue(reciveModel);
                        if (dtd.HasData() && reciveModel.DataType.HasValue())
                        {
                            var m = new InstructReturn()
                            {
                                ControlName = string.Empty,
                                ParamenterUnit = dtd.Rows[0]["paramenter_unit"].ToString(),
                                ParamenterInstruct = instruct,
                                ParamenterName = dtd.Rows[0]["paramenter_name"].ToString(),
                                Id = dtd.Rows[0]["Id"].ToString()
                            };
                            GetPlcValueForType(instruct, helper, reciveModel.DataType, m);
                            instructModelReturns.Add(m);
                            SendMessage(JsonConvert.SerializeObject(new InstructModelReturns()
                            {
                                InstructModelReturn = instructModelReturns
                            }), clientFlag);
                        }
                        else
                        {
                            SendMsg("数据类型为空或指令配置异常！", clientFlag);
                        }
                    }
                    else if (requesType == RequestType.SetCommandValue)//发送单个控制命令:setCommandValue;
                    {
                        if (instruct.HasValue() && reciveModel.DataType.HasValue())
                        {
                            if (GetSendDataType(reciveModel.DataType) == SendDataType.Int)
                            {
                                var res = 0;
                                int.TryParse(reciveModel.Value,out res);
                                helper.Write(instruct, res);
                            }
                            else
                            {
                                bool res = reciveModel.Value == "1";
                                helper.Write(instruct, res);
                            }
                            SendMsg("success", clientFlag);

                            //保存数据到历史记录表
                            bool flag = SaveCommandDataToHistoryTab(reciveModel, helper.DeviceEntity).Result;
                        }
                        else
                        {
                            SendMsg("指令或数据类型不能为空！", clientFlag);
                        }
                    }
                    else
                    {
                        SendMsg("未知指令！", clientFlag);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
                SendMsg(ex.Message, clientFlag);
            }
        }

        private void BuildListToFront(string instruct, SiemensHelpers helper, List<InstructReturn> instructModelReturns, DataRow dr, string unit, string clientFlag)
        {
            if (instruct.HasValue())
            {
                var model = new InstructReturn()
                {
                    ControlName = dr["control_name"].ToString(),
                    ParamenterUnit = unit,
                    ParamenterInstruct = instruct,
                    ParamenterName = dr["paramenter_name"].ToString(),
                    Id = dr["id"].ToString()
                };
                GetPlcValueForType(instruct, helper, dr["paramenter_value_type"].ToString(), model);
                instructModelReturns.Add(model);
            }
            else
            {
                SendMsg("指令异常！", clientFlag);
            }
        }

        /// <summary>
        /// 根据数据类型来决定读取的方式
        /// </summary>
        private void GetPlcValueForType(string instruct, SiemensHelpers helper, string dataType, InstructReturn model)
        {
            if (GetSendDataType(dataType) == SendDataType.Short)
            {
                model.ParamenterInstruct_V = helper.Read(instruct);//从plc获取值
            }
            else
            {
                model.ParamenterInstruct_V = helper.Read(instruct, GetSendDataType(dataType));//从plc获取值
            }
        }
        /// <summary>
        /// 根据数据类型来决定读取的方式
        /// </summary>
        private void GetPlcValueForType(string first, string second, SiemensHelpers helper, string dataType, InstructReturn model)
        {
            if (GetSendDataType(dataType) == SendDataType.Short)
            {
                model.ParamenterInstructStart_V = helper.Read(first);//从plc获取值
                model.ParamenterInstructEnd_V = helper.Read(second);//从plc获取值
            }
            else
            {
                model.ParamenterInstructStart_V = helper.Read(first, GetSendDataType(dataType));//从plc获取值
                model.ParamenterInstructEnd_V = helper.Read(second, GetSendDataType(dataType));//从plc获取值
            }
        }

        private void BuildListToFront(string first, string second, SiemensHelpers helper, List<InstructReturn> instructModelReturns, DataRow dr, string unit, string clientFlag)
        {
            if (first.HasValue() && second.HasValue())
            {
                var model = new InstructReturn()
                {
                    ControlName = dr["control_name"].ToString(),
                    ParamenterUnit = unit,
                    ParamenterInstructStart = first,
                    ParamenterInstructEnd = second,
                    ParamenterName = dr["paramenter_name"].ToString(),
                    Id = dr["id"].ToString()
                };
                GetPlcValueForType(first, second, helper, dr["paramenter_value_type"].ToString(), model);
                instructModelReturns.Add(model);
            }
            else
            {
                SendMsg("指令异常！", clientFlag);
            }
        }

        private void SendMsg(string msg, string clientFlag)
        {
            WebSocketMessage(msg, clientFlag);
            SendMessage(msg, clientFlag);
        }

        #region 人员定位
        public async Task SendPepolePostionData(string clientFlag, bool flag = false)
        {
            List<PepolePositionEntity> pepoleEntities = await GetPepolePositionList();
            var list = new List<PepolePostionContent>();
            List<Pepole> listPepole = new List<Pepole>();
            int i = 0, count = 0;

            foreach (var item in pepoleEntities)
            {
                //计算工时
                DateTime t = DateTime.Now;
                DateTime.TryParse(item.InMineTime, out t);
                var res = DateTime.Now - t;
                if (list.Count == 0 || list.Any(p => p.CurrentStation == item.CurrentStation))
                {
                    if (list.Count == 0)
                    {
                        list.Add(new PepolePostionContent()
                        {
                            CurrentStation = item.CurrentStation,
                            StationAddress = item.StationAddress,
                        });
                    }
                    listPepole.Add(new Pepole()
                    {
                        PepoleNumber = item.PepoleNumber,
                        PepoleName = item.PepoleName,
                        TypeOfWorkName = item.TypeOfWork,
                        Post = item.Post,
                        Duty = item.Duty,
                        ManHour = res.Minutes
                    });
                }
                else
                {
                    list.Last().Nums = i;
                    list.Last().Pepoles = new List<Pepole>(listPepole);
                    listPepole.Clear();
                    i = 0;
                    list.Add(new PepolePostionContent()
                    {
                        CurrentStation = item.CurrentStation,
                        StationAddress = item.StationAddress,
                    });
                    listPepole.Add(new Pepole()
                    {
                        PepoleNumber = item.PepoleNumber,
                        PepoleName = item.PepoleName,
                        TypeOfWorkName = item.TypeOfWork,
                        Post = item.Post,
                        Duty = item.Duty,
                        ManHour = res.Minutes
                    });
                }
                i++;
                count++;
                if (count == pepoleEntities.Count)
                {
                    list.Last().Nums = i;
                    list.Last().Pepoles = new List<Pepole>(listPepole);
                    listPepole.Clear();
                    i = 0;
                }
            }
            var ssm = new PepolePostionModel()
            {
                PepolePosition = list
            };
            if ((!_pepolePostionEntities.HasValue() || !ListNotEqual(pepoleEntities, _pepolePostionEntities)) && list.Count > 0 &&
                dic_Sockets.Values.Count > 0 || flag)//flag:标识是否略过重复判断发送数据
            {
                var str = ssm.ToJson();
                SendMessage(str, clientFlag);
                if (str.HasValue() && list.Count > 0)
                {
                    _pepolePostionEntities = pepoleEntities;
                }
            }
            list.Clear();
        }

        private async Task<List<PepolePositionEntity>> GetPepolePositionList()
        {
            //几种读取方式
            var readWay = "PepolePositionReadWay".GetConfigrationStr();
            List<PepolePositionEntity> pepolePositions = null;
            switch (readWay.ToLower())
            {
                case "file":
                    break;
                case "database":
                    pepolePositions = await DataServices.GetPepolePostionData();
                    break;
                case "http":
                    var model = HttpHelper.HttpGet("PepolePositionUrl".GetConfigrationStr());
                    pepolePositions = JsonConvert.DeserializeObject<List<PepolePositionEntity>>(model);
                    break;
                default:
                    break;
            }

            return pepolePositions;
        }
        #endregion

        #region 供配电
        private async Task SendSubStationData(string clientFlag)
        {
            _formSubstation.OpcStart(clientFlag);
        }
        #endregion

        #region 光纤测温
        private async Task SendFiberData(string clientFlag, bool flag = false)
        {
            List<FiberEntity> fibers = await GetFiberList();
            var list = new List<FiberContent>();
            foreach (var item in fibers)
            {
                list.Add(Mapper.Map<FiberEntity, FiberContent>(item));
            }
            var fm = new FiberModel()
            {
                Fiber = list
            };
            if ((!_fiberEntities.HasValue() || !ListNotEqual(fibers, _fiberEntities)) && list.Count > 0 &&
                    dic_Sockets.Values.Count > 0 || flag)
            {
                var str = fm.ToJson();
                SendMessage(str, clientFlag);
                if (str.HasValue() && list.Count > 0)
                {
                    _fiberEntities = fibers;
                }
            }
            list.Clear();
        }

        private static async Task<List<FiberEntity>> GetFiberList()
        {
            //几种读取方式
            var readWay = "FiberReadWay".GetConfigrationStr();
            List<FiberEntity> fibers = null;
            switch (readWay.ToLower())
            {
                case "file":
                    break;
                case "database":
                    fibers = await DataServices.GetFiberData();
                    break;
                case "http":
                    break;
                default:
                    break;
            }

            return fibers;
        }
        #endregion

        private bool ListNotEqual<T>(List<T> entityies1, List<T> entityies2)
        {
            bool flag = false;
            if (entityies1.HasValue())
            {
                var str = JsonConvert.SerializeObject(entityies1);
                var str1 = JsonConvert.SerializeObject(entityies2);
                flag = str == str1;
            }
            return flag;
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (dic_Sockets.Count > 0)
                {
                    //关闭与客户端的所有的连接
                    foreach (var item in dic_Sockets.Values)
                    {
                        if (item != null)
                        {
                            item.Close();
                        }
                    }
                    webStart.Enabled = true;
                    webStop.Enabled = false;
                    webSend.Enabled = false;
                    WebSocketMessage("客户端网页已经全部关闭！");
                }
                else
                {
                    WebSocketMessage("当前服务端没有客户端连接！");
                }
            }
            catch (Exception)
            {
                webStart.Enabled = false;
                webStop.Enabled = true;
                webSend.Enabled = true;
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webSend_Click(object sender, EventArgs e)
        {
            string value = webContent.Text.Trim();
            if (value.HasValue())
            {
                int c = 0;
                int.TryParse(txtSendCount.Text.Trim(), out c);
                if (c == 0)
                {
                    MessageBox.Show("推送次数必须大于0");
                }
                else
                {
                    for (int i = 0; i < c; i++)
                    {
                        SendMessage(value, string.Empty);
                    }
                }
            }
            else
            {
                WebSocketMessage("请输入发送内容！");
            }

        }

        public void SendMessage(string val, string clientFlag)
        {
            if (dic_Sockets != null && dic_Sockets.FirstOrDefault(p => p.Key == clientFlag).Value != null)
            {
                dic_Sockets.FirstOrDefault(p => p.Key == clientFlag).Value.Send(val);
                //if (dic_Sockets != null && dic_Sockets.Count != 0)
                //{
                //    dic_Sockets.FirstOrDefault().Value.Send(val);
                //}
            }
        }
        public void SendMessage(string val)
        {
            if (dic_Sockets != null && dic_Sockets.Count > 0)
            {
                dic_Sockets.FirstOrDefault().Value.Send(val);
                //if (dic_Sockets != null && dic_Sockets.Count != 0)
                //{
                //    dic_Sockets.FirstOrDefault().Value.Send(val);
                //}
            }
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        /// <param name="str"></param>
        public void WebSocketMessage(string str, string client = "")
        {
            if (client.HasValue() && str.HasValue())
            {
                webMessage.Invoke(new Action(() =>
                {
                    string value = string.Format("-{0} - 时间：{1}  内容：{2}\r", client, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), str);
                    webMessage.SelectionFont = new Font("宋体", 12, FontStyle.Regular);  //设置SelectionFont属性实现控件中的文本为楷体，大小为12，字样是粗体
                    webMessage.SelectionColor = System.Drawing.Color.Red;    //设置SelectionColor属性实现控件中的文本颜色为红色
                    webMessage.AppendText(value);
                }));
            }
            else
            {
                webMessage.Invoke(new Action(() =>
                {
                    string value = string.Format("-时间：{0}  内容：{1}\r", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), str);
                    webMessage.SelectionFont = new Font("宋体", 12, FontStyle.Regular);  //设置SelectionFont属性实现控件中的文本为楷体，大小为12，字样是粗体
                    webMessage.SelectionColor = System.Drawing.Color.Blue;    //设置SelectionColor属性实现控件中的文本颜色为红色
                    webMessage.AppendText(value);
                }));
            }
        }

        private void webContent_TextChanged(object sender, EventArgs e)
        {
            LimitLine(200, this.webMessage);
            this.webMessage.SelectionStart = this.webMessage.Text.Length;
            this.webMessage.SelectionLength = 0;
        }
        public void LimitLine(int maxLength, RichTextBox richTextBox)
        {
            if (richTextBox.Lines.Length > maxLength)
            {
                int moreLines = richTextBox.Lines.Length - maxLength;
                string[] lines = richTextBox.Lines;
                Array.Copy(lines, moreLines, lines, 0, maxLength);
                Array.Resize(ref lines, maxLength);
                richTextBox.Lines = lines;
            }
        }

        #endregion

        #region 全局窗口事件
        private void FrmMian_FormClosed(object sender, FormClosedEventArgs e)
        {
            MqtCLient?.Disconnect();
        }
        #endregion
    }

}
