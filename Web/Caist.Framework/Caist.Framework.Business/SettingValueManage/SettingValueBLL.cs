using Caist.Framework.Entity.SettingValueManage;
using Caist.Framework.Model.Param.SettingValueManage;
using Caist.Framework.Service.SettingValueManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.SettingValueManage
{
    public class SettingValueBLL
    {
        private SettingValueService viewParamenterService = new SettingValueService();

        #region 获取数据
        public async Task<TData<List<SettingValueEntity>>> GetList(SettingValueListParam param)
        {
            TData<List<SettingValueEntity>> obj = new TData<List<SettingValueEntity>>();
            obj.Result = await viewParamenterService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SettingValueEntity>>> GetPageList(SettingValueListParam param, Pagination pagination)
        {
            TData<List<SettingValueEntity>> obj = new TData<List<SettingValueEntity>>();
            obj.Result = await viewParamenterService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SettingValueEntity>>> GetPageContentList(SettingValueListParam param, Pagination pagination)
        {
            TData<List<SettingValueEntity>> obj = new TData<List<SettingValueEntity>>();
            obj.Result = await viewParamenterService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<SettingValueEntity>> GetEntity(long id)
        {
            TData<SettingValueEntity> obj = new TData<SettingValueEntity>();
            obj.Result = await viewParamenterService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await viewParamenterService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(SettingValueEntity entity)
        {
            TData<string> obj = new TData<string>();
            await viewParamenterService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateEntity(SettingValueEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (entity.Id.HasValue)
            {

                //根据ID 获取对象值，避免参数传空导致原有数据丢失，将有更新得值复制到新的实体对象中传入更新方法
                SettingValueEntity newEntity = await viewParamenterService.GetEntity(entity.Id.Value);
                if (entity.ParameterMaxValue.HasValue())
                {
                    newEntity.ParameterMaxValue = entity.ParameterMaxValue;
                }
                if (entity.ParameterMinValue.HasValue())
                {
                    newEntity.ParameterMinValue = entity.ParameterMinValue;
                }
                if (entity.ParameterValue.HasValue())
                {
                    newEntity.ParameterValue = entity.ParameterValue;
                }
                newEntity.BaseModifyTime = DateTime.Now;
                await viewParamenterService.UpdateEntity(newEntity);
                obj.Result = entity.Id.ParseToString();
                obj.Tag = 1;
            }
            else
            {
                obj.Message = "ID为空无法更新数据";
                obj.Result = "0";
                obj.Tag = 0;
            }

            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await viewParamenterService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
