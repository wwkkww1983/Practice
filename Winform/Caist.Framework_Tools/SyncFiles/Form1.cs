using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.ThreadPool;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SyncFiles
{
    public partial class Form1 : Form
    {
        Timer _timer;
        string _path;
        List<string> _temps = new List<string>();
        List<FiberEntity> _fibers = new List<FiberEntity>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            _path = GetPath();
            _temps = FileOperation.ReadTextList(_path);
            _timer = new Timer();
            _timer.Interval = 5000;
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            Start();
        }
        private void btnSync_Click(object sender, EventArgs e)
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
            _temps.Clear();
            _fibers.Clear();
            BuildList();
            DataServices.InsertData(_fibers);
            MessageBox.Show("success");
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
                    if (i == 100)//TODO:提取到配置文件中
                    {
                        AddFiberEntityToList(listTemp, "地面变电所至主井口");
                        listTemp.Clear();
                    }
                    else if (i == 500)
                    {
                        AddFiberEntityToList(listTemp, "主斜井口至主斜井底");
                        listTemp.Clear();
                    }
                    else if (i == newTemps.Count - 1)
                    {
                        AddFiberEntityToList(listTemp, "11807运输石门至临时配电所");
                        listTemp.Clear();
                    }
                }
            }
        }

        private void AddFiberEntityToList(List<float> listTemp, string name)
        {
            _fibers.Add(new FiberEntity()
            {
                area_name = name,
                average_value = listTemp.Average().ToString(),
                max_value = listTemp.Max().ToString(),
                min_value_pos = listTemp.Max().ToString(),
                min_value = listTemp.Min().ToString(),
                max_value_pos = listTemp.Min().ToString()
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
    }

}
