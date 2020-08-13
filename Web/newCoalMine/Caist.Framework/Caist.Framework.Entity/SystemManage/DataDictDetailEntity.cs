﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.SystemManage
{
    [Table("sys_data_dict_detail")]
    public class DataDictDetailEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string DictType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? DictSort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? DictKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string DictValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
    }
}
