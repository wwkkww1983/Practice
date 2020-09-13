/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 14:21:52
*说明:			字典条目表 - BaseInfo_DicItem 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 字典条目表 - Dic_Content
    /// </summary>
    public class Dic_Content : BaseEntity
    {

        /// <summary>
        /// 类型ID - DicType_ID
        /// </summary>
        public string DicType_ID { get; set; }
        /// <summary>
        /// 类型CODE - DicTypeContent
        /// </summary>
        public string DicTypeContent { get; set; }
        /// <summary>
        /// 排序 - Sort
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        ///  - CreateId
        /// </summary>
        public string CreateId { get; set; }
        /// <summary>
        ///  - CreateUser
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        ///  - CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        ///  - UpdateId
        /// </summary>
        public string UpdateId { get; set; }
        /// <summary>
        /// 修改人用户名 - UpdateUser
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 修改时间 - UpdateTime
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}

