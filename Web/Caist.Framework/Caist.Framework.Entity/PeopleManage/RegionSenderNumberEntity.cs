using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.PeopleManage
{
    [Table("RegionSenderNumber")]
    public class RegionSenderNumberEntity : BaseExtensionEntity
    {
        /// <summary>
		/// 
		/// </summary>
		public int Region_id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Sender_num
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Worker_num
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Other_num
        {
            set;
            get;
        }
    }
}
