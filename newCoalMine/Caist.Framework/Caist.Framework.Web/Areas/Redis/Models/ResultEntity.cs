using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.Redis.Models
{
    public class ResultEntity
    {
        private string ip;
        private string address;
        private string value;
        private string sendTime;
        public string Ip { get => ip; set => ip = value; }
        public string Address { get => address; set => address = value; }
        public string Value { get => value; set => this.value = value; }
        public string SendTime { get => sendTime; set => sendTime = value; }
    }
}
