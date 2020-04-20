using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.PeopleManage
{
    [Table("Region")]
    public class RegionEntity : BaseExtensionEntity
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
        public string Region_name
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Region_type
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int People_max
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Linger_max
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Display_x
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Display_y
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Display_type
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Region_info
        {
            set;
            get;
        }
    }
}
