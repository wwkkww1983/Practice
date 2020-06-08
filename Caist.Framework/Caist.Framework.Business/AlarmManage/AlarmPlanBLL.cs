using Caist.Framework.Business.SystemManage;
using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Entity.SystemManage;
using Caist.Framework.Model.Param.OrganizationManage;
using Caist.Framework.Model.Param.SystemManage;
using Caist.Framework.Service.AlarmManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.AlarmManage
{
    public class AlarmPlanBLL
    {
        private readonly AlarmPlanService planService = new AlarmPlanService();
        private readonly FileBLL fileBLL = new FileBLL();

        #region 获取数据
        public async Task<TData<List<AlarmPlanEntity>>> GetList(AlarmPlanListParam param)
        {
            TData<List<AlarmPlanEntity>> obj = new TData<List<AlarmPlanEntity>>();
            obj.Result = await planService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AlarmPlanReturnEntity>>> GetPageList(AlarmPlanListParam param, Pagination pagination)
        {
            TData<List<AlarmPlanReturnEntity>> obj = new TData<List<AlarmPlanReturnEntity>>();
            obj.Result = await planService.GetPageList(param, pagination);
            foreach (var item in obj.Result)//根据模块ID来获取对应的文件列表
            {
                List<FileEntity> list = new List<FileEntity>();
                var files = await fileBLL.GetList(new FileListParam()
                {
                    ModuleId = long.Parse(item.Id)
                });
                foreach (var file in files.Result)
                {
                    if (file.FileName.Length > 35)
                    {
                        item.Img = file;
                    }
                    else
                    {
                        list.Add(file);
                    }
                }
                item.Files = list;
            }
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AlarmPlanEntity>>> GetPageContentList(AlarmPlanListParam param, Pagination pagination)
        {
            TData<List<AlarmPlanEntity>> obj = new TData<List<AlarmPlanEntity>>();
            obj.Result = await planService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AlarmPlanEntity>> GetEntity(long id)
        {
            TData<AlarmPlanEntity> obj = new TData<AlarmPlanEntity>();
            obj.Result = await planService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(AlarmPlanEntity entity)
        {
            TData<string> obj = new TData<string>();
            await planService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await planService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
