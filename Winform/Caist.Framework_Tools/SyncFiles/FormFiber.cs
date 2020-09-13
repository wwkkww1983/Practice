using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.ThreadPool;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

/**
 *  zhangmeiqing 2020-8-17
 *  此类为光纤测温以文本方式存储的矿而服务（中渝）
 * */
namespace SyncFiles
{
    public partial class FormFiber : Form
    {
        public Timer _formFiberTimer;
        string _path;
        List<string> _temps = new List<string>();
        Dictionary<int,string> _tempFibers = new Dictionary<int, string>();
        public static List<FiberEntity> _listFormFibers = new List<FiberEntity>();

        public FormFiber()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Common.InitLog("FiberLogPath".GetConfigrationStr());
            Init();
        }

        private void Init()
        {
            _path = GetPath();
            _temps = FileOperation.ReadTextList(_path);
            _tempFibers = GetKeyPair(FileOperation.ReadTextList("FiberConfigPath".GetConfigrationStr()));
            _formFiberTimer = new Timer();
            _formFiberTimer.Interval = 5000;
            _formFiberTimer.Tick += _timer_Tick;
        }

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
                CtrlBtn(true);
                Start();
                _formFiberTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CtrlBtn(bool flag)
        {
            btnSync.Enabled = !flag;
            btnStop.Enabled = flag;
        }

        private void Start()
        {
            //_temps.Clear();
            _listFormFibers.Clear();
            BuildList();
            var flag = DataServices.SaveFiberData(_listFormFibers);
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
            _listFormFibers.Add(new FiberEntity()
            {
                AreaName = name,
                AverageValue = listTemp.Average().ToString(),
                MaxValue = listTemp.Max().ToString(),
                MinValue = listTemp.Min().ToString(),
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
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
            CtrlBtn(false);
            _formFiberTimer.Stop();
        }
    }

}
