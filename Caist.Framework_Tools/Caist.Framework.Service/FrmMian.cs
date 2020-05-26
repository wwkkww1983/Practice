using AutoMapper;
using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Entity;
using Caist.Framework.Entity.Enum;
using Caist.Framework.PLC.Siemens;
using Caist.Framework.PLC.Siemens.Common;
using Caist.Framework.PLC.Siemens.Model;
using Caist.Framework.Service.Control;
using Caist.Framework.ThreadPool;
using Caist.Framework.WebSocket;
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
        private List<FiberEntity> _fiberEntities = new List<FiberEntity>();
        private List<SiemensHelper> _siemensHelpers = new List<SiemensHelper>();
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
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMian_Load(object sender, EventArgs e)
        {
            this.sw.Start();
            this.timing.Start();
            this.LoadDataDevice(TreeDevice.Nodes);
            this.LoadDataTagGroup();
            this.LoadDataTag();
            //初始化SiemensHelpers
            SiemensInit();
            this.GetInit();

            var s = "WebSocketTimer".GetConfigrationStr();
            if (s.HasValue())
            {
                timerWebSocket.Interval = 1000;// Convert.ToInt32(s);
            }
            MappingModel();

            //数据同步初始化
            Init();
        }

        private void SiemensInit()
        {
            PublicEntity.DeviceEntities.ForEach(d =>
            {
                SiemensHelper siemensHelper = new SiemensHelper();
                siemensHelper.DeviceEntity = d;
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
                            string Name = string.Format("{0}.{1}", groupEntity.Name, instructEntity.Name);
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

        private void MappingModel()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<FiberEntity, FiberContent>();
                cfg.CreateMap<SubStationEntity, SubStationContent>();
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
            Task.Run(() =>
            {
                PlcStart();
            });
        }

        private void PlcStart()
        {
            //StartInfo info = new StartInfo { Timeout = 1, MinWorkerThreads = PublicEntity.DeviceEntities.Count() };
            //IThreadPool pool = ThreadPoolFactory.Create(info, "PLC线程池");
            //PublicEntity.DeviceEntities.ForEach(v =>
            _siemensHelpers.ForEach(v =>
            {
                v.IP = v.DeviceEntity.Host;
                v.Port = v.DeviceEntity.Port;
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
                    Task.Run(()=>{ 
                        CaistTimer TimerMessage = new CaistTimer() { Interval = 1000 };
                        TimerMessage.Elapsed += TimerMessage_Elapsed;
                        TimerMessage.obj = v;
                        TimerMessage.Start();
                        
                    });
                    //IWorkItem item = pool.QueueUserWorkItem(Print, v);

                }
            });
            webStart_Click(null, null);
            //pool.WaitAll();
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
                            string key = string.Format("{0}.{1}", groupEntity.Name, instructEntity.Name);
                            ListViewItem item = lvPoint.FindItemWithText(key);
                            string Name = item.ToolTipText.ToString();
                            switch (instructEntity.CheckDataType())
                            {
                                case DataTypeEnum.TYPE_INT:
                                case DataTypeEnum.TYPE_BYTE:
                                case DataTypeEnum.TYPE_SHORT:
                                case DataTypeEnum.TYPE_BOOL:
                                    SendData(int.Parse(entity.GetValue(Name).ToString()), key, entity.DeviceEntity, item.SubItems[3].Text);
                                    item.SubItems[4].Text = Convert.ToInt32(entity.GetValue(Name)).ToString();
                                    break;
                                case DataTypeEnum.TYPE_FLOAT:
                                    SendData(Convert.ToDouble(entity.GetValue(Name)), key, entity.DeviceEntity, item.SubItems[3].Text);
                                    
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
        private async Task SendData(double v, string key, DeviceEntity entity,string addr)
        {
            var item = PublicEntity.AlarmEntities.Find(p => (p.MaxValue < v || p.MinValue > v) && p.ManipulateModelMark == key);
            if (item != null)
            {
                var alarmModel = new AlarmModel()
                {
                    Alarm = new AlarmContent()
                    {
                        Name = item.SystemName,
                        Message = item.BroadCastContent,
                        Type = true
                    }
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
        //TODO: 1、光纤测温websocket推送；2、供电站webscoket推送；3、建历史表；4、svn 外网映射；
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
                if (!string.IsNullOrEmpty(webAddress.Text.ToString()) && !string.IsNullOrEmpty(webPort.Text.ToString()))
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
                            //如果存在这个客户端,那么对这个socket进行移除
                            if (dic_Sockets.ContainsKey(clientUrl))
                            {
                                dic_Sockets.Remove(clientUrl);
                            }
                            WebSocketMessage("|服务器:和客户端网页:" + clientUrl + " 断开WebSock连接！");
                        };
                        socket.OnMessage = message =>  //接受客户端网页消息事件
                        {
                            string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                            WebSocketMessage("|服务器:【收到】来客户端网页:" + clientUrl + "的信息：\n" + message);

                        };
                    });
                }
                else
                {
                    WebSocketMessage("请输入IP地址或者端口");
                }
                //其它表的推送
                this.timerWebSocket.Start();
            }
            catch (Exception ex)
            {
                webStart.Enabled = true;
                webStop.Enabled = false;
                webSend.Enabled = false;
            }
        }

        /// <summary>
        /// 向前端推送数据库表数据
        /// </summary>
        /// <returns></returns>
        private async Task PushMsgToClient()
        {
            await SendFiberData();
            await SendSubStationData();
        }

        private async Task SendSubStationData()
        {
            List<SubStationEntity> stationEntities = await DataServices.GetSubStationData();
            var list = new List<SubStationContent>();
            foreach (var item in stationEntities)
            {
                list.Add(Mapper.Map<SubStationEntity, SubStationContent>(item));
            }
            var ssm = new SubStationModel()
            {
                SubStation = list
            };
            if ((!_stationEntities.HasValue() || !ListNotEqual(stationEntities, _stationEntities)) &&
                dic_Sockets.Values.Count > 0)
            {
                var str = ssm.ToJson();
                SendMessage(str);
                _stationEntities = stationEntities;
            }
        }

        private async Task SendFiberData()
        {
            List<FiberEntity> fibers = await DataServices.GetFiberData();
            var list = new List<FiberContent>();
            foreach (var item in fibers)
            {
                list.Add(Mapper.Map<FiberEntity, FiberContent>(item));
            }
            var fm = new FiberModel()
            {
                Fiber = list
            };
            if ((!_fiberEntities.HasValue() || !ListNotEqual(fibers, _fiberEntities)) &&
                    dic_Sockets.Values.Count > 0)
            {
                var str = fm.ToJson();
                SendMessage(str);
                _fiberEntities = fibers;
            }
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
            if (!string.IsNullOrEmpty(value))
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
            foreach (var socket in dic_Sockets.Values)
            {
                socket.Send(val);
                string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                WebSocketMessage("|服务器:【发送】客户端网页:" + clientUrl + "的信息：\n" + val);
                webContent.Clear();
            }
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        /// <param name="str"></param>
        public void WebSocketMessage(string str, string client = "")
        {
            if (!string.IsNullOrEmpty(client) && !string.IsNullOrEmpty(str))
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
                if (!string.IsNullOrEmpty(str) && string.IsNullOrEmpty(client))
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

        #region 数据加载
        private void LoadDataDevice(TreeNodeCollection treeNode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"SELECT  a.tab_name,a.id as Id, a.Device_Name as Name, a.Device_Host as Host, a.Device_Port as Port, a.Slot_No as CPU_SlotNO, a.PLCType as PLCType,
                            a.Local as LocalTASP,  a.Remote as RemoteTASP, a.parent_id as ParentId,a.tab_name as TabName  FROM mk_device a WHERE a.base_is_delete = 0 and tab_name is not null and tab_name <> ''
                            ;");//and a.Device_Host in ('192.168.200.53')
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.DeviceEntities = DataConvert.DataTableToList<DeviceEntity>(dataTable).ToList();
            }
            PublicEntity.DeviceEntities.ForEach(d =>
            {
                TreeNode tree = treeNode.Add(string.Format("{0}-[{1}:{2}]", d.Name, d.Host, d.Port.ToString()));
                tree.ImageIndex = 0;
            });
        }

        public void LoadDataTagGroup(string id = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select a.id as Id,a.device_id as DeviceId,a.name as Name,a.modular_type as MMType,a.read_count as ReadCount,a.begin_address as BeginAddress,
                             a.begin_block as Block from mk_instruct_group a where a.base_is_delete = 0 ");
            if (!string.IsNullOrEmpty(id))
            {
                builder.Append(" and a.device_id = '" + id + "' ");
            }
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.TagGroupsEntities = DataConvert.DataTableToList<TagGroupsEntity>(dataTable).ToList();
            }
        }

        public void LoadDataTag(string id = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select a.id as Id,a.Name as Name,a.instruct_group_id as TagGroup,a.address as Address,a.data_type as DataType,a.output as Output,a.remark as [Desc] 
                             from mk_instruct a where a.base_is_delete = 0 ");
            if (!string.IsNullOrEmpty(id))
            {
                builder.Append(" and a.device_id = '" + id + "' ");
            }
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.TagEntities = DataConvert.DataTableToList<TagEntity>(dataTable).ToList();
            }
        }

        /// <summary>
        /// 获取报警标准数据
        /// </summary>
        /// <param name="id"></param>
        public void LoadAlarmData(string id = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select s.id,s.system_name,a.min_value,a.max_value,a.broadcast_content,m.manipulate_model_mark,m.manipulate_model_name
                from [dbo].[mk_system_setting] s inner join [dbo].[mk_alarm_settings] a  on s.id=a.system_models
                inner join [dbo].[mk_view_manipulate_model] m on a.view_manipulate_id=m.id where a.base_is_delete = 0 ");
            if (!string.IsNullOrEmpty(id))
            {
                builder.Append(" and a.device_id = '" + id + "' ");
            }
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.AlarmEntities = DataConvert.DataTableToList<AlarmEntity>(dataTable).ToList();
            }
        }
        #endregion

        private void timerWebSocket_Tick(object sender, EventArgs e)
        {
            //启动websocket
            PushMsgToClient();
        }
    }

    #region 返回数据模型
    #region 报警
    public class AlarmModel
    {
        public AlarmContent Alarm { get; set; }
    }

    public class AlarmContent
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public bool Type { get; set; }
    }
    #endregion

    #region 光纤测温
    public class FiberModel
    {
        public List<FiberContent> Fiber { get; set; }
    }

    public class FiberContent
    {
        public string AreaName { get; set; }
        public string MaxValue { get; set; }
        public string MaxValuePos { get; set; }
        public string MinValue { get; set; }
        public string MinValuePos { get; set; }
        public string AverageValue { get; set; }
    }
    #endregion

    #region 配电站
    public class SubStationModel
    {
        public List<SubStationContent> SubStation { get; set; }
    }

    public class SubStationContent
    {
        public string Sys_Id { get; set; }
        public string F { get; set; }
        public string IA { get; set; }
        public string P { get; set; }
        public string Q { get; set; }
        public string COS { get; set; }
    }
    #endregion

    #endregion 
}
