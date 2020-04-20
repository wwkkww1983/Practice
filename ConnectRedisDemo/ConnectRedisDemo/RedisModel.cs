using System;
using System.Collections.Generic;

namespace ConnectRedisDemo
{
    [Serializable]
    public class RedisModel
    {
        public string Ip { get; set; }
        public string Address { get; set; }
        public string Value { get; set; }
        public DateTime SendTime { get; set; } 
    }

    [Serializable]
    public class JsonModel
    {
        public List<string> tongfeng { get; set; }
        public List<string>  yafeng { get; set; }
        public List<string> jushan { get; set; }
        public List<string> pidai { get; set; } 
    }
}
