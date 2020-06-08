using Caist.Framework.Entity.SystemManage;
using Caist.Framework.Model.Param.SystemManage;
using Caist.Framework.Service.SystemManage;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.SystemManage
{
    public class FileBLL
    {
        private readonly FileService fileService = new FileService();

        #region 获取数据
        public async Task<TData<List<FileEntity>>> GetList(FileListParam param)
        {
            TData<List<FileEntity>> obj = new TData<List<FileEntity>>();
            obj.Result = await fileService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<FileEntity>> GetEntity(long id)
        {
            TData<FileEntity> obj = new TData<FileEntity>();
            obj.Result = await fileService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }


        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(List<FileEntity> entitys)
        {
            TData<string> obj = new TData<string>();
            await fileService.SaveForm(entitys);
            obj.Result = string.Empty;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();

            await fileService.DeleteForm(ids);

            obj.Tag = 1;
            return obj;
        }
        #endregion

    }
}
