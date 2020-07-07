using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Entity;
using Caist.Framework.Entity.Enum;
using Caist.Framework.PLC.Siemens;
using Caist.Framework.PLC.Siemens.Common;
using Caist.Framework.PLC.Siemens.Model;
using Caist.Framework.Service.Control;
using Caist.Framework.ThreadPool;
using Fleck;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Caist.Framework.PLC.Siemens.Enum.ModularType;

namespace Caist.Framework.Service
{
    public partial class FrmMian : Form
    {
        /**{"key": "value"}**/
        #region 变量定义
        private Stopwatch sw = new Stopwatch();
        private TimeSpan ts = new TimeSpan();
        private List<SubStationEntity> _stationEntities = new List<SubStationEntity>();
        private List<PepolePostionEntity> _pepolePostionEntities = new List<PepolePostionEntity>();
        private List<FiberEntity> _fiberEntities = new List<FiberEntity>();
        private List<SiemensHelper> _siemensHelpers = new List<SiemensHelper>();
        public static FrmMian MyForm;
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
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMian_Load(object sender, EventArgs e)
        {
            InitWebsocketSend();

            //底部程序运时间
            this.sw.Start();
            this.timing.Start();

            //加载plc列表
            DataServices.LoadDataDevice(TreeDevice.Nodes);
            //加载PLC内存块长度配置信息列表
            DataServices.LoadDataTagGroup();
            //加载PLC后台配置内存地址指向表
            DataServices.LoadDataTag();
            //初始化SiemensHelpers
            SiemensInit();
            //初始化plc 指令列表
            this.GetInit();

            #region 初始化websocket配置
            var s = "WebSocketTimer".GetConfigrationStr();
            if (s.HasValue())
            {
                timerWebSocket.Interval = Convert.ToInt32(s);
                timerWebSocket.Tick += TimerWebSocket_Tick;
                //其它表的推送
                timerWebSocket.Start();
            }
            #endregion
            //对象映射
            //MappingModel();

            //数据同步初始化
            Init();
            //初始化mqtt配置信息
            LoadMqtt();
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

        //socket 推送plc数据
        private void TimerWebSocket_Tick(object sender, EventArgs e)
        {
            PushMsgToClient();
        }

        private void SiemensInit()
        {
            PublicEntity.DeviceEntities.ForEach(d =>
            {
                SiemensHelper siemensHelper = new SiemensHelper();
                siemensHelper.DeviceEntity = d;
                siemensHelper.SetIP(d.Host);
                siemensHelper.SetPort(d.Port);
                siemensHelper.Init();
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
                    Device device = siemens.GetDevice();
                    foreach (KeyValuePair<string, InstructGroupEntity> item in device.ValuePairs)
                    {
                        InstructGroupEntity groupEntity = item.Value;
                        foreach (KeyValuePair<string, InstructEntity> list in groupEntity.InstructPairs)
                        {
                            InstructEntity instructEntity = list.Value;
                            string Name = string.Format("{2}-{0}.{1}", groupEntity.Name, instructEntity.Name, siemens.DeviceEntity.Host);
                            string Key = string.Format("{0}.{1}", groupEntity.Id, instructEntity.Id);
                            ListViewItem lvitem = new ListViewItem(Name);
                            lvitem.ToolTipText = Key;
                            lvitem.SubItems.Add(instructEntity.Desc);
                            lvitem.SubItems.Add(instructEntity.DataType);
                            lvitem.SubItems.Add(instructEntity.GetAddressName());
                            lvitem.SubItems.Add("");
                            lvitem.SubItems.Add(Extensions.ConvertOutPutEnum(int.Parse(instructEntity.Output)));
                            lvitem.Tag = instructEntity;
                            lvPoint.Items.Add(lvitem);
                            lvPoint.Columns[0].Width = -1;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        //private void MappingModel()
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<FiberEntity, FiberContent>();
        //        cfg.CreateMap<SubStationEntity, SubStationContent>();
        //        cfg.CreateMap<PepolePostionEntity, PepolePostionContent>();
        //    });
        //}
        #endregion

        #region PLC读取
        /// <summary>
        /// 开启PLC连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPLCStrats_Click(object sender, EventArgs e)
        {
            btnPLCStrats.Enabled = false;
            Task.Run(() =>
            {
                PlcStart();
            });
        }

        private void PlcStart()
        {
            StartInfo info = new StartInfo { Timeout = 1, MinWorkerThreads = PublicEntity.DeviceEntities.Count() };
            IThreadPool pool = ThreadPoolFactory.Create(info, "PLC线程池");
            //PublicEntity.DeviceEntities.ForEach(v =>
            _siemensHelpers.ForEach(v =>
            {
                var result = v.Start();
                if (!result)
                {
                    txtMessage.Invoke(new Action(() =>
                    {
                        txtMessage.AppendText(string.Format("PLC【{0}-{1}:{2}】连接失败" + Environment.NewLine, v.DeviceEntity.Name, v.DeviceEntity.Host, v.DeviceEntity.Port));
                    }));
                }
                else
                {
                    PlcTool.Invoke(new Action(() =>
                    {
                        btnPLCStrats.Enabled = false;
                        btnPLCStop.Enabled = true;
                        txtMessage.AppendText(string.Format("PLC【{0}-{1}:{2}】连接成功" + Environment.NewLine, v.DeviceEntity.Name, v.DeviceEntity.Host, v.DeviceEntity.Port));
                    }));
                    Task.Run(() =>
                    {
                        CaistTimer TimerMessage = new CaistTimer() { Interval = 1000 };
                        TimerMessage.Elapsed += TimerMessage_Elapsed;
                        TimerMessage.obj = v;
                        TimerMessage.Start();

                    });
                    IWorkItem item = pool.QueueUserWorkItem(Print, v);

                }
            });
            webStart_Click(null, null);
            pool.WaitAll();
        }

        /// <summary>
        /// PLC连接状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool _deviceStatusflag = true;
        private void TimerMessage_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _deviceStatusflag = true;
            SiemensHelper entity = (SiemensHelper)((CaistTimer)sender).obj;
            SystemStatus.Invoke(new Action(() =>
            {
                switch (entity.CommStatus)
                {
                    case 0:
                        txtStatus.Text = "未连接";
                        break;
                    case 1:
                        txtStatus.Text = "正在进行TCP连接";
                        break;
                    case 2:
                        txtStatus.Text = "TCP连接成功";
                        break;
                    case 3:
                        txtStatus.Text = "PLC握手成功";
                        break;
                    case 4:
                        txtStatus.Text = "正常采集过程中...";
                        break;
                    case 5:
                        _deviceStatusflag = false;
                        txtStatus.Text = "PLC握手错误";
                        break;
                    case 6:
                        _deviceStatusflag = false;
                        txtStatus.Text = "通讯错误";
                        break;
                    default:
                        break;
                }
                if (_deviceStatusflag)
                {
                    Device device = entity.GetDevice();
                    foreach (KeyValuePair<string, InstructGroupEntity> group in device.ValuePairs)
                    {
                        InstructGroupEntity groupEntity = group.Value;
                        foreach (KeyValuePair<string, InstructEntity> pair in groupEntity.InstructPairs)
                        {
                            InstructEntity instructEntity = pair.Value;
                            string key = string.Format("{2}-{0}.{1}", groupEntity.Name, instructEntity.Name, entity.DeviceEntity.Host);
                            ListViewItem item = lvPoint.FindItemWithText(key);
                            string Name = item.ToolTipText.ToString();
                            switch (instructEntity.CheckDataType())
                            {
                                case DataTypeEnum.TYPE_INT:
                                case DataTypeEnum.TYPE_BYTE:
                                case DataTypeEnum.TYPE_SHORT:
                                case DataTypeEnum.TYPE_BOOL:
                                    SendData(int.Parse(entity.GetValue(Name).ToString()), key, entity.DeviceEntity, string.Format("{0}-" + item.SubItems[3].Text, entity.DeviceEntity.Host));
                                    item.SubItems[4].Text = Convert.ToInt32(entity.GetValue(Name)).ToString();
                                    break;
                                case DataTypeEnum.TYPE_FLOAT:
                                    SendData(Convert.ToDouble(entity.GetValue(Name)), key, entity.DeviceEntity, string.Format("{0}-" + item.SubItems[3].Text, entity.DeviceEntity.Host));

                                    item.SubItems[4].Text = entity.GetValue(Name).ToString();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }));
        }
        StringBuilder _sb = new StringBuilder();
        private async void SendData(double v, string key, DeviceEntity entity, string addr)
        {
            var item = PublicEntity.AlarmEntities.Find(p => (p.MaxValue < v || p.MinValue > v) && p.ManipulateModelMark == key);
            if (item != null)
            {
                var list = new List<AlarmContent>();
                list.Add(new AlarmContent()
                {
                    SysId = item.Id,
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
                SendMessage(str);
                //保存异常数据到数据库
                await SaveAlarmData(item, v);
            }
            else
            {
                _sb.Append("{");
                _sb.AppendFormat("\"{0}\":\"{1}\"", addr, v.ToString("#0.00"));
                _sb.Append("}");
                SendMessage(_sb.ToString());
                _sb.Clear();
                //保存数据到历史记录表
                bool flag = await SaveDataToHistoryTab(key, v, entity);
            }
        }

        private async Task<bool> SaveDataToHistoryTab(string key, double v, DeviceEntity device)
        {
            var history = new HistoryEntity()
            {
                DictId = key,
                DictValue = v.ToString(),
                TabName = device.TabName
            };
            return await DataServices.SaveHistoryData(history);
        }

        private async Task<bool> SaveAlarmData(AlarmEntity item, double v)
        {
            var alarm = new AlarmRecordEntity()
            {
                alarm_id = item.Id,
                alarm_time = DateTime.Now,
                alarm_time_length = v.ToString(),
                alarm_reason = item.BroadCastContent
            };
            return await DataServices.SaveAlarmData(alarm);
        }

        //TODO:1、整合推送到plc采集； 2、判断报警信息（存入报警表，发送特定消息给前端）；3、整合数据同步进来；
        //TODO: 1、光纤测温websocket推送；2、供电站webscoket推送；3、建历史表；4、svn 外网映射；5、PLC联调
        public object Print(Object obj)
        {
            while (true)
            {
                return null;
            }
        }
        /// <summary>
        /// 计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timing_Tick(object sender, EventArgs e)
        {
            ts = sw.Elapsed;
            txttiming.Text = string.Format("{0}:{1}:{2}", ts.Hours.ToString("D5"), ts.Minutes.ToString("D2"), ts.Seconds.ToString().PadLeft(2, '0'));
        }
        #endregion

        #region WebSocket消息推送
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webStart_Click(object sender, EventArgs e)
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
                            //string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                            //WebSocketMessage("|服务器:和客户端网页:" + clientUrl + " 断开WebSock连接！");
                        };
                        socket.OnMessage = async message =>  //接受客户端网页消息事件
                        {
                            string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                            await PushSpecifiedMsg(message);
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
            }
        }

        public async Task PushSpecifiedMsg(string message)
        {
            if (message.Contains("人员定位"))
            {
                await SendPepolePostionData(true);
            }
            else if (message.Contains("光纤测温"))
            {
                await SendFiberData(true);
            }
            else if (message.Contains("供配电"))
            {
                await SendSubStationData(true);
            }
            else if (message.HasValue() && message.ToLower().Contains("remotecontrol"))
            {
                FrontSwitchControl(message);
            }
            else
            {
                SendMsg("未知命令！");
            }
        }

        private void FrontSwitchControl(string message)
        {
            /**
             * 统一请求控制面板命令值集合：getCommandValues; 
             * 获取单个控制命令状态值:getCommandValue;
             * 发送单个控制命令:setCommandValue;
             **/
            try
            {
                var reciveModel = JsonConvert.DeserializeObject<InstructModel>(message);

                string ip;
                string port;
                string instruct;
                SiemensHelper helper;
                var requesType = reciveModel.RemoteControl.RequestType.ToLower();
                if (requesType == "getcommandvalues")//获取当前系统所有开关的状态
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
                                BuildListToFront(ip, port, instruct, helper, instructModelReturns, dr, unit);
                            }
                            else//双控
                            {
                                instruct = dr["paramenter_instruct_start"].ToString();
                                BuildListToFront(ip, port, instruct, helper, instructModelReturns, dr, unit);
                                instruct = dr["paramenter_instruct_end"].ToString();
                                BuildListToFront(ip, port, instruct, helper, instructModelReturns, dr, unit);
                            }
                        }
                        SendMessage(JsonConvert.SerializeObject(new InstructModelReturns()
                        {
                            InstructModelReturn = instructModelReturns
                        }));
                    }
                }
                else
                {
                    List<InstructReturn> instructModelReturns;
                    ip = reciveModel.RemoteControl.Ip;
                    port = reciveModel.RemoteControl.Port;
                    instruct = reciveModel.RemoteControl.Instruct;
                    helper = _siemensHelpers.Find(p => p.DeviceEntity.Host == ip && p.DeviceEntity.Port == port);
                    if (requesType == "getcommandvalue")//增加单个命令状态查询 getCommandValue
                    {
                        instructModelReturns = new List<InstructReturn>();
                        var dtd = DataServices.GetSingleCommandValue(reciveModel);
                        if (dtd.HasData())
                        {
                            var Key = string.Format("{0}.{1}", dtd.Rows[0]["instructId"].ToString(), dtd.Rows[0]["groupID"].ToString());
                            instructModelReturns.Add(new InstructReturn()
                            {
                                ControlName = string.Empty,
                                ParamenterUnit = string.Empty,
                                ParamenterInstruct = instruct,
                                ParamenterInstruct_V = helper.GetValue(Key).ToString(),//从plc获取值
                                ParamenterName = string.Empty,
                                Id = string.Empty
                            });
                            SendMessage(JsonConvert.SerializeObject(new InstructModelReturns()
                            {
                                InstructModelReturn = instructModelReturns
                            }));
                        }
                        else
                        {
                            SendMsg("指令配置异常！");
                        }

                    }
                    else//发送单个控制命令:setCommandValue;
                    {
                        var arrays = GetInstructArray(instruct);
                        if (arrays != null)
                        {
                            var dt = DataServices.GetGroupInfo(ip, port, arrays);
                            if (dt.HasData())
                            {
                                helper.SendIntruct(dt.Rows[0]["instructId"].ToString(), dt.Rows[0]["groupID"].ToString(), double.Parse(reciveModel.RemoteControl.Value));
                            }
                            SendMessage("success");
                        }
                        else
                        {
                            SendMsg("前端发送指令异常！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SendMsg(ex.Message);
            }
        }

        private void BuildListToFront(string ip, string port, string instruct, SiemensHelper helper, List<InstructReturn> instructModelReturns, DataRow dr, string unit)
        {
            var arrays = GetInstructArray(instruct);
            if (arrays != null)
            {
                var dt = DataServices.GetGroupInfo(ip, port, arrays);//获取命令对应的命令ID和组ID
                if (dt.HasData())
                {
                    var Key = string.Format("{0}.{1}", dt.Rows[0]["instructId"].ToString(), dt.Rows[0]["groupID"].ToString());
                    instructModelReturns.Add(new InstructReturn()
                    {
                        ControlName = dr["control_name"].ToString(),
                        ParamenterUnit = unit,
                        ParamenterInstruct = instruct,
                        ParamenterInstruct_V = helper.GetValue(Key).ToString(),//从plc获取值
                        ParamenterName = dr["paramenter_name"].ToString(),
                        Id = dr["id"].ToString()
                    });
                }
                else
                {
                    SendMsg("后台配置指令异常！");
                }
            }
            else
            {
                SendMsg("指令异常！");
            }
        }

        private void SendMsg(string msg)
        {
            WebSocketMessage(msg);
            SendMessage(msg);
        }
        private Tuple<string, string> GetInstructArray(string instruct)
        {
            Tuple<string, string> t = null;
            if (instruct.HasValue())
            {
                var front = instruct.Substring(0, instruct.IndexOf("."));
                var end = instruct.Substring(instruct.IndexOf(".") + 1);
                t = Tuple.Create(front, end);
            }
            return t;
        }

        /// <summary>
        /// 向前端推送数据库表数据
        /// </summary>
        /// <returns></returns>
        private async void PushMsgToClient()
        {
            await SendFiberData();
            await SendSubStationData();
            await SendPepolePostionData();
        }

        private async Task SendPepolePostionData(bool flag = false)
        {
            List<PepolePostionEntity> pepoleEntities = await DataServices.GetPepolePostionData();
            var list = new List<PepolePostionContent>();
            foreach (var item in pepoleEntities)
            {
                //list.Add(Mapper.Map<PepolePostionEntity, PepolePostionContent>(item));
                list.Add(new PepolePostionContent()
                {
                    CurrentStation = item.CurrentStation,
                    Nums = item.Nums,
                    StationAddress = item.StationAddress
                });
            }
            var ssm = new PepolePostionModel()
            {
                PepolePosition = list
            };
            if ((!_pepolePostionEntities.HasValue() || !ListNotEqual(pepoleEntities, _pepolePostionEntities)) && list.Count > 0 &&
                dic_Sockets.Values.Count > 0 || flag)//flag:标识是否略过重复判断发送数据
            {
                var str = ssm.ToJson();
                SendMessage(str);
                if (str.HasValue() && list.Count > 0)
                {
                    _pepolePostionEntities = pepoleEntities;
                }
            }
            list.Clear();
        }

        #region 供配电
        private async Task SendSubStationData(bool flag = false)
        {
            List<SubStationEntity> stationEntities = await DataServices.GetSubStationData();
            var list = new List<SubStationContent>();
            foreach (var item in stationEntities)
            {
                //list.Add(Mapper.Map<SubStationEntity, SubStationContent>(item));
                list.Add(new SubStationContent()
                {
                    COS = item.COS,
                    F = item.F,
                    IA = item.IA,
                    P = item.P,
                    Q = item.Q,
                    Sys_Id = item.SysId
                });
            }
            var ssm = new SubStationModel()
            {
                SubStation = list
            };
            if ((!_stationEntities.HasValue() || !ListNotEqual(stationEntities, _stationEntities)) && list.Count > 0 &&
                dic_Sockets.Values.Count > 0 || flag)
            {
                var str = ssm.ToJson();
                SendMessage(str);
                if (str.HasValue() && list.Count > 0)
                {
                    _stationEntities = stationEntities;
                }
            }
            list.Clear();
        }
        #endregion

        private async Task SendFiberData(bool flag = false)
        {
            List<FiberEntity> fibers = await DataServices.GetFiberData();
            var list = new List<FiberContent>();
            foreach (var item in fibers)
            {
                //list.Add(Mapper.Map<FiberEntity, FiberContent>(item));
                list.Add(new FiberContent()
                {
                    AreaName = item.AreaName,
                    AverageValue = item.AverageValue,
                    MaxValue = item.MaxValue,
                    MaxValuePos = item.MaxValuePos,
                    MinValue = item.MinValue,
                    MinValuePos = item.MinValuePos
                });
            }
            var fm = new FiberModel()
            {
                Fiber = list
            };
            if ((!_fiberEntities.HasValue() || !ListNotEqual(fibers, _fiberEntities)) && list.Count > 0 &&
                    dic_Sockets.Values.Count > 0 || flag)
            {
                var str = fm.ToJson();
                SendMessage(str);
                if (str.HasValue() && list.Count > 0)
                {
                    _fiberEntities = fibers;
                }
            }
            list.Clear();
        }

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
                SendMessage(value);
            }
            else
            {
                WebSocketMessage("请输入发送内容！");
            }

        }

        public void SendMessage(string val)
        {
            //处理错误：DIctionary：集合已修改，可能无法执行枚举操作
            //解决：另外创建一个数组来循环修改集合值
            var New_Sockets = dic_Sockets.Values.ToList<IWebSocketConnection>();
            foreach (var socket in New_Sockets)
            {
                socket.Send(val);
            }
            New_Sockets.Clear();
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
            if (webMessage.TextLength == webMessage.MaxLength)
            {
                webMessage.Clear();
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
