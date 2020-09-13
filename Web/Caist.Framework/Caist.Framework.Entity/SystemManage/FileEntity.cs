using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.SystemManage
{
    [Table("mk_files")]
    public class FileEntity: BaseExtensionEntity
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get; set;
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get; set;
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType
        {
            get; set;
        }

        /// <summary>
        /// 模块ID
        /// </summary>
        public long? ModuleId
        {
            get; set;
        }

        /// <summary>
        /// 关联的模块类型
        /// </summary>
        public string ModuleType
        {
            get; set;
        }
    }
}
