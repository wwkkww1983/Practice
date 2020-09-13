using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.Entity
{
    [Serializable]
    [Table("mk_alarm_record")]
    public class AlarmRecordEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "id")]
        public long id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "alarm_id")]
        public long alarm_id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "alarm_time")]
        public DateTime? alarm_time
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "alarm_time_length")]
        public string alarm_time_length
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "alarm_reason")]
        public string alarm_reason
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "base_is_delete")]
        public int base_is_delete
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "base_create_time")]
        public DateTime? base_create_time
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "base_modify_time")]
        public DateTime? base_modify_time
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "base_creator_id")]
        public long? base_creator_id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "base_modifier_id")]
        public long? base_modifier_id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "base_version")]
        public int? base_version
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColName = "alarm_value")]
        public string alarm_value
        {
            set;
            get;
        }
    }
}
