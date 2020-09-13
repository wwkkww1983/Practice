using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.ApplicationManage
{

    public class MqtUploadReqEntity
    { 
        public string mkCode { get; set; }
        public string systemCode { get; set; }
        public string key { get; set; }
    }

    public class MqtUploadResEntity
    {
        public bool state { get; set; }
        public List<MqtUploadEntity> data { get; set; }
        public int code { get; set; }
    }
    public class MqtUploadEntity
    {
        public DateTime? uploadTime { get; set; }
        public string mkCode { get; set; }
        public string code { get; set; }
        public string panelCode { get; set; }
        public string addressTypeCode { get; set; }
        public string deviceAddressCode { get; set; }
        public string systemCode { get; set; }

        public string indexCode { get; set; }
        public string parameterCode { get; set; }
        public string value { get; set; }
        public string codeDescribe { get; set; }

    }

}
