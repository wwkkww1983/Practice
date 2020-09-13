using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Threading;

namespace DLClient
{
    public partial class MapControl : UserControl
    {
        #region 变量
        /// <summary>
        /// 经纬度集合
        /// </summary>
        List<PointLatLng> listPoint = new List<PointLatLng>();
        /// <summary>
        /// 位置集合
        /// </summary>
        List<PositionMark> listPosition = new List<PositionMark>();
        public OperationMode _OperationMode = OperationMode.View;
        public string _SectionId { get; set; }
        public Form _ParentObj { get; set; }
        private GeocodingProvider gp; //地址编码服务
        string path = "MarkData.txt";
        private GMapOverlay markersOverlay = new GMapOverlay("markers"); //放置marker的图层
        string mapDataPath = ConfigurationManager.AppSettings["MapDataPath"];
        double mapStart_Lat = Convert.ToDouble(ConfigurationManager.AppSettings["MapStart_Lat"]);//起始经度
        double mapStart_Lng = Convert.ToDouble(ConfigurationManager.AppSettings["MapStart_Lng"]);//起始纬度
        int X;
        int Y;
        #endregion

        public MapControl()
        {
            InitializeComponent();

            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            var x = this.Width / 2 + pictureBox1.Width / 2;
            var y = this.Height / 2 + pictureBox1.Height / 2;
            this.pictureBox1.Location = new Point(x, y);
        }

        private void initData()
        {
            switch (_OperationMode)
            {
                case OperationMode.View:
                    //btnCancel.Visible = false;
                    //btnClear.Visible = false;
                    break;
                case OperationMode.Edit:

                    break;
            }
        }
        private void gMap_Load(object sender, EventArgs e)
        {
            BackgroundWorker backWork = new BackgroundWorker();
            backWork.DoWork += (s, a) =>
            {
                Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
                {
                    MapLoad();
                }));

            };
            backWork.RunWorkerCompleted += BackWork_RunWorkerCompleted;
            backWork.RunWorkerAsync();

        }

        private void BackWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Visible = false;
        }

        private void MapLoad()
        {
            gMap.MapProvider = GMapProviders.GoogleChinaMap; //google china 地图

            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly;
            GMap.NET.GMaps.Instance.ImportFromGMDB(mapDataPath); //缓存位置
            try
            {
                gMap.MinZoom = 1;
                gMap.MaxZoom = 19;
                gMap.Position = new PointLatLng(mapStart_Lat, mapStart_Lng);
                gMap.Zoom = 13;
            }
            catch (Exception ex) { }
            gMap.DragButton = System.Windows.Forms.MouseButtons.Left; //左键拖拽地图
            gMap.Overlays.Add(markersOverlay);

            if (_OperationMode == OperationMode.Edit)
            {
                gMap.MouseClick += GMap_MouseClick;
                gMap.ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("撤销",CancelToolStripMenuItem_Click),
                    new MenuItem("标记",MarkToolStripMenuItem_Click),
                    new MenuItem("添加设备",AddDeviceToolStripMenuItem_Click)
                });
            }

            LoadMapMarks();
        }

        private void LoadMapMarks()
        {
            var pmList = ReadDataForSql();
            if (pmList.Count > 0)
            {
                foreach (var item in pmList)
                {
                    PointLatLng point = new PointLatLng(item.Lat, item.Lng);
                    AddMark(point);
                    //ShowToolTip(point, item.Distance.ToString());
                }
            }
        }

        private void GMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                X = e.X;
                Y = e.Y;
            }
        }

        private double ShowDistance(PointLatLng point)
        {
            double dist = 0;
            if (listPoint.Count > 0)
            {
                var fPoint = listPoint[listPoint.Count - 1];
                dist = MapUtils.GetDistance(fPoint.Lat, fPoint.Lng, point.Lat, point.Lng);
            }
            return dist;
        }
        private string GetAddressName(PointLatLng point)
        {
            List<Placemark> plc = null;
            var st = GMapProviders.BingHybridMap.GetPlacemarks(point, out plc);
            if (st == GeoCoderStatusCode.G_GEO_SUCCESS && plc != null)
            {
                foreach (var pl in plc)
                {
                    if (!string.IsNullOrEmpty(pl.PostalCodeNumber))
                    {
                        Debug.WriteLine("Accuracy: " + pl.Accuracy + ", " + pl.Address + ", PostalCodeNumber: " + pl.PostalCodeNumber);
                    }
                }
            }

            gp = gMap.MapProvider as GeocodingProvider;
            if (gp == null) //地址转换服务，没有就使用OpenStreetMap
            {
                gp = GMapProviders.OpenStreetMap as GeocodingProvider;
            }
            GMapProvider.Language = LanguageType.ChineseSimplified; //使用的语言，默认是英文

            GeoCoderStatusCode statusCode = GeoCoderStatusCode.Unknow;
            Placemark? place = gp.GetPlacemark(point, out statusCode);
            if (statusCode == GeoCoderStatusCode.G_GEO_SUCCESS)
            {
                ShowToolTip(point, place.Value.Address);
            }
            return place == null ? string.Empty : place.Value.Address;
        }

        private void ShowToolTip(PointLatLng point, string place)
        {
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.blue_dot);
            marker.ToolTipText = place;
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            //markersOverlay.Markers.Add(marker);
        }

        private void AddMark(PointLatLng point)
        {
            if (_OperationMode == OperationMode.Edit)
            {
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.blue_dot);
                markersOverlay.Markers.Add(marker);
            }
            listPoint.Add(point);
            GMapRoute route = new GMapRoute(listPoint, "line");
            route.Stroke.Color = Color.Red;
            route.Stroke.Width = 2;  //设置画
            markersOverlay.Routes.Add(route);
        }

        #region 文件操作
        public void WriteData(string str, FileMode fileMode = FileMode.Create)
        {
            Stream sm = null;
            StreamWriter sw = null;
            try
            {
                sm = CreateFile(fileMode);
                sw = new StreamWriter(sm);
                if (fileMode == FileMode.Append)
                {
                    sw.WriteLine(str);
                }
                else
                {
                    sw.Write(str);
                }
            }
            finally
            {
                sw.Close();
                sm.Close();
            }
        }

        public List<PositionMark> ReadData()
        {
            List<PositionMark> list = new List<PositionMark>();
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);

                try
                {
                    var strs = sr.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(strs))
                    {
                        list = JsonConvert.DeserializeObject<List<PositionMark>>(strs);
                    }
                }
                finally
                {
                    sr.Close();
                }
            }
            return list;
        }

        public Stream CreateFile(FileMode fileMode = FileMode.Append)
        {
            Stream stm = null;
            if (!File.Exists(path))
            {
                stm = File.Create(path);
            }
            else
            {
                stm = File.Open(path, fileMode);
            }
            return stm;
        }

        //public void ClearData(ClearMode cm)
        //{
        //    List<PositionMark> list = null;
        //    switch (cm)
        //    {
        //        case ClearMode.Part:
        //            list = ReadData();
        //            if (list.Count > 0)
        //            {
        //                list.RemoveAt(list.Count - 1);
        //                var str = list.IgnoreNull();
        //                WriteData(str);
        //            }

        //            break;
        //        case ClearMode.All:
        //            list = ReadData();
        //            list.Clear();
        //            WriteData(string.Empty);
        //            break;
        //    }
        //}
        #endregion

        #region 数据库操作
        public bool WriteDataForSql(PositionMark pm)
        {
            string sql = @"INSERT INTO [dbo].[Basic_Locus]
           ([Id],[SectionId],[Point_X],[Point_Y],[CreateId],[CreateUser],[CreateTime])
            VALUES
           (@Id,@SectionId,@Point_X,@Point_Y,@CreateId,@CreateUser,getdate())";
            var sqlp = new SqlParameter[] {
                 new SqlParameter("Id",SqlDbType.VarChar),
                 new SqlParameter("SectionId",SqlDbType.VarChar),
                 new SqlParameter("Point_X",SqlDbType.NVarChar),
                 new SqlParameter("Point_Y",SqlDbType.NVarChar),
                 new SqlParameter("CreateId",SqlDbType.VarChar),
                 new SqlParameter("CreateUser",SqlDbType.NVarChar)
            };
            sqlp[0].Value = Guid.NewGuid().ToString();
            sqlp[1].Value = _SectionId;
            sqlp[2].Value = pm.Lat;
            sqlp[3].Value = pm.Lng;
            sqlp[4].Value = string.Empty;
            sqlp[5].Value = string.Empty;

            return DbHelperSQL.ExecuteSql(sql, sqlp) > 0;
        }
        public List<PointLatLng> ReadDataForSql()
        {
            List<PointLatLng> list = new List<PointLatLng>();
            string sql = @"select [Id],[SectionId],[Point_X],[Point_Y],[CreateId],[CreateUser],[CreateTime] from [dbo].[Basic_Locus] where SectionId=@sectionId order by createTime asc";
            var sqlp = new SqlParameter[] {
             new SqlParameter("sectionId",SqlDbType.VarChar)
            };
            sqlp[0].Value = _SectionId;
            var dt = DbHelperSQL.Query(sql, sqlp).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                list.Add(new PointLatLng
                {
                    Lat = Convert.ToDouble(item["Point_X"]),
                    Lng = Convert.ToDouble(item["Point_Y"])
                });
            }
            return list;
        }

        #endregion

        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mark = markersOverlay.Markers;
            if (mark.Count > 0)
            {
                mark.RemoveAt(mark.Count - 1);
            }
            if (listPoint.Count > 0)
            {
                listPoint.RemoveAt(listPoint.Count - 1);
            }
            var moy = markersOverlay.Routes;
            if (moy.Count > 0)
            {
                moy.RemoveAt(moy.Count - 1);
            }
        }
        private void MarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PointLatLng point = gMap.FromLocalToLatLng(X, Y);

            #region 位置信息
            PositionMark pm = new PositionMark();
            pm.Lat = point.Lat;
            pm.Lng = point.Lng;
            pm.Distance = listPosition.Count > 0 ? listPosition[listPosition.Count - 1].Distance + ShowDistance(point) : ShowDistance(point);
            ShowToolTip(point, pm.Distance.ToString());
            pm.Zoom = gMap.Zoom;
            //pm.Address = GetAddressName(point);//TODO:地址获取有问题
            #endregion

            AddMark(point);
            WriteDataForSql(pm);//保存位置
        }
        private void AddDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("构建中...");
        }
    }
}
