using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.ThreadPool;
using Opc.Ua;
using Opc.Ua.Client;
using OpcUaHelper;
using OpcUaHelper.Forms;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncFiles
{
    public partial class FormFiber : Form
    {
        #region 变量
        public Timer _timer;
        string _path;
        List<string> _temps = new List<string>();
        Dictionary<int, string> _tempFibers = new Dictionary<int, string>();
        List<FiberEntity> _fibers = new List<FiberEntity>();
        private OpcUaClient _OpcUaClient = null; 
        #endregion
        public FormFiber()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitFiber();
            InitSubStation();
        }

        #region 初始化
        private void InitFiber()
        {
            _path = GetPath();
            _temps = FileOperation.ReadTextList(_path);
            _tempFibers = GetKeyPair(FileOperation.ReadTextList("FiberConfigPath".GetConfigrationStr()));
            _timer = new Timer();
            _timer.Interval = 5000;
            _timer.Tick += _timer_Tick;
        }
        private void InitSubStation()
        {
            _OpcUaClient = new OpcUaClient();
            txtAddress.Text = "SubStationAddress".GetConfigrationStr();
        } 
        #endregion

        #region 光纤测温
        private Dictionary<int, string> GetKeyPair(List<string> lists)
        {
            return lists.Select(p => p.Split(':')).ToDictionary(k => int.Parse(k[0]), v => v[1]);
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            Start();
        }
        public void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                Start();
                _timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Start()
        {
            //_temps.Clear();
            _fibers.Clear();
            BuildList();
            var flag = DataServices.InsertData(_fibers);
            if (!flag)
            {
                MessageBox.Show("faild!");
            }
        }

        public void BuildList()
        {
            if (_temps.HasValue())
            {
                var newTemps = _temps.Select(p => float.Parse(p)).ToList();
                List<float> listTemp = new List<float>();
                for (int i = 0; i < newTemps.Count; i++)
                {
                    listTemp.Add(newTemps[i]);
                    foreach (var item in _tempFibers)
                    {
                        if (i == item.Key)
                        {
                            AddFiberEntityToList(listTemp, item.Value);
                            listTemp.Clear();
                        }
                    }
                }
            }
        }

        private void AddFiberEntityToList(List<float> listTemp, string name)
        {
            _fibers.Add(new FiberEntity()
            {
                AreaName = name,
                AverageValue = listTemp.Average().ToString(),
                MaxValue = listTemp.Max().ToString(),
                MinValuePos = listTemp.Max().ToString(),
                MinValue = listTemp.Min().ToString(),
                MaxValuePos = listTemp.Min().ToString()
            });
        }

        /// <summary>
        /// 获取读取的目录路径
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            var path = "Path".GetConfigrationStr();
            var ymd = GetLastFolderName(path);
            var second = Path.Combine(path, ymd);
            var hour = GetLastFolderName(second);
            var third = Path.Combine(second, hour);
            var ms = GetLastFolderName(third);
            return Path.Combine(third, ms, "PathEnd".GetConfigrationStr());
        }

        public string GetLastFolderName(string path)
        {
            return Directory.GetDirectories(path)?.Select(p => p.Substring(p.LastIndexOf("\\") + 1)).OrderByDescending(t => t).FirstOrDefault();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _timer.Stop();
        } 
        #endregion

        #region 供配电OPC
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            using (FormConnectSelect formConnectSelect = new FormConnectSelect(_OpcUaClient))
            {
                if (formConnectSelect.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _OpcUaClient.ConnectServer(txtAddress.Text);
                        btnConnect.BackColor = Color.LimeGreen;
                    }
                    catch (Exception ex)
                    {
                        ClientUtils.HandleException(Text, ex);
                    }
                }
            }
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            _OpcUaClient.AddSubscription("king", txtSubNodeId.Text, SubCallback);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            DataValue dataValue = _OpcUaClient.ReadNode(new NodeId(txtReadNodeId.Text));
            txtReadNodeIdVal.Text = dataValue.WrappedValue.Value.ToString();
        }
        private void SubCallback(string key, MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs args)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, MonitoredItem, MonitoredItemNotificationEventArgs>(SubCallback), key, monitoredItem, args);
                return;
            }

            if (key == "king")
            {
                // 如果有多个的订阅值都关联了当前的方法，可以通过key和monitoredItem来区分
                MonitoredItemNotification notification = args.NotificationValue as MonitoredItemNotification;
                if (notification != null)
                {
                    txtSubNodeIdVal.Text = notification.Value.WrappedValue.Value.ToString();
                }
            }
        }
        #endregion
    }

}
