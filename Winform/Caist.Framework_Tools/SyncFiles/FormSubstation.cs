using Caist.Framework.Entity;
using Caist.Framework.Service;
using Caist.Framework.ThreadPool;
using Opc.Ua;
using SyncFiles.Tools;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SyncFiles
{
    public partial class FormSubstation : Form
    {
        #region 变量
        private List<OpcHelper> _opcHelpers = new List<OpcHelper>();
        List<OpcTimer> _listCaistTimers = new List<OpcTimer>();
        Dictionary<OpcHelper, int> _valuePairs = new Dictionary<OpcHelper, int>();
        FrmMian _frmMian;
        #endregion
        public FormSubstation(FrmMian frmMian)
        {
            this._frmMian = frmMian;
            InitializeComponent();
        }

        private void FormSubstation_Load(object sender, EventArgs e)
        {
            InitSubStation();
            OpcInit();
        }
        #region 初始化

        private void OpcInit()
        {
            PublicEntity.OPCPowerEntities.ForEach(d =>
            {
                OpcHelper oPCHelper = new OpcHelper();
                oPCHelper.IP = d.IP;
                oPCHelper.Port = d.Port;
                oPCHelper.Init();
                _opcHelpers.Add(oPCHelper);
            });
        }
        private void InitSubStation()
        {
            txtAddress.Text = "SubStationAddress".GetConfigrationStr();
        }
        #endregion

        public async void OpcStart()
        {
            _opcHelpers.ForEach(v =>
            {
                try
                {
                    v.Connect();

                    OpcTimer TimerMessage = new OpcTimer() { Interval = 100 };
                    TimerMessage.Elapsed += TimerMessage_Elapsed;
                    TimerMessage.obj = v;
                    _valuePairs.Add(v, 0);
                    _listCaistTimers.Add(TimerMessage);
                }
                catch (Exception ex)
                {
                    Common.LogError(ex);
                }
            });

            foreach (var item in _listCaistTimers)
            {
                item.Start();
            }
        }

        StringBuilder _sb = new StringBuilder();
        private void TimerMessage_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            OpcHelper helper = (OpcHelper)((OpcTimer)sender).obj;
            var count = _valuePairs[helper];
            var list = helper.ListAddress;//获取当前helper对应的地址集合
            var strs = list[count].Split(':');
            DataValue dataValue = helper._OpcUaClient.ReadNode(new NodeId(strs[1]));
            SendData(helper.IP, strs[0], strs[1], dataValue);

            count++;
            count = list.Count == count ? 0 : count;
            _valuePairs[helper] = count;
        }

        private void SendData(string ip,string name, string addr, DataValue dataValue)
        {
            _sb.Append("{");
            _sb.AppendFormat("\"{0}\":\"{1}\"", $"{ip}-{name}-{addr}", dataValue.WrappedValue.Value);
            _sb.Append("}");
            //通过websocket发送数据到前端
            _frmMian.SendMessage(_sb.ToString());
            txtReadNodeId.Invoke(new Action(()=> {
                txtReadNodeId.Text = _sb.ToString();
            }));
        }

        #region 链接opc
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            OpcStart();
        }
        #endregion

        #region 直接读取模式
        private void btnRead_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #region 订阅模式

        //private void btnSub_Click(object sender, EventArgs e)
        //{
        //    _OpcUaClient.AddSubscription("king", txtSubNodeId.Text, SubCallback);
        //}

        //private void SubCallback(string key, MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs args)
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(new Action<string, MonitoredItem, MonitoredItemNotificationEventArgs>(SubCallback), key, monitoredItem, args);
        //        return;
        //    }

        //    if (key == "king")
        //    {
        //        // 如果有多个的订阅值都关联了当前的方法，可以通过key和monitoredItem来区分
        //        MonitoredItemNotification notification = args.NotificationValue as MonitoredItemNotification;
        //        if (notification != null)
        //        {
        //            txtSubNodeIdVal.Text = notification.Value.WrappedValue.Value.ToString();
        //        }
        //    }
        //}
        #endregion
    }
}
