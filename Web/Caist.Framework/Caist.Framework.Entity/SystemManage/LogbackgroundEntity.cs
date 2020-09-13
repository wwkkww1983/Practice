using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.SystemManage
{
    [Table("sys_log_background")]
    public class LogbackgroundEntity : BaseCreateEntity
    {
        //子系统名称
        public string SystemName { get; set; }
        //开关状态
        public string SwitchStatus { get; set; }
        //当前模式
        public string CurrentMode { get; set; }
        //操作人
        public string Operator { get; set; }
       //备注是否操作成功
        public string Remarks { get; set; }
    }
}
