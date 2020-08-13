using System;

namespace Caist.Framework.Entity.Entity
{
    public class AlarmRecordEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public long id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public long alarm_id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? alarm_time
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string alarm_time_length
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string alarm_reason
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int base_is_delete
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? base_create_time
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? base_modify_time
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public long? base_creator_id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public long? base_modifier_id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? base_version
        {
            set;
            get;
        }
    }
}
