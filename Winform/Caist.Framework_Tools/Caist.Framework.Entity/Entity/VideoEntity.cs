using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Caist.Framework.Entity.Entity
{
    [Serializable]
    public class VideoEntity
    {
        [DBColumn(ColName = "id")]
        public long? Id { get; set; }
        [DBColumn(ColName = "Name")]
        public string Name { get; set; }
        [DBColumn(ColName = "gid")]
        public string Gid { get; set; }
        /// <summary>
        /// 异常消息名
        /// </summary>
        [DBColumn(ColName = "Url")]
        public string Url { get; set; }
      
    }
}
