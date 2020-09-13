using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Video
{
    public class Result<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }
    public class Pages
    {
        public int total { get; set; }
        public List<Devie> list { get; set; }
    }
    public class HlsUrl { 
        public string url { get; set; }
    }

    public class Devie
    {
        /// <summary>
        /// 设备唯一编号  用于获取单个设备资源
        /// </summary>
        public string cameraIndexCode { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string cameraName { get; set; }


    }

    public class Hls
    {
        public string cameraIndexCode { get; set; }
        public int streamType = 0;
        public string protocol = "hls";
        public int transmode = 1;
    }
}
