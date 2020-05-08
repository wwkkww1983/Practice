using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.PeopleManage;
using Caist.Framework.Model.PeopleManage;
using Caist.Framework.Util.Extension;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.PeopleManage
{
    public class RegionService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<RegionPeopleNumEntity>> PersonnelList(RegionParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<RegionPeopleNumEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<RegionPeopleNumEntity>> PeopleCounting(RegionParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = SqlFilter(param, strSql);
            var list = await this.BaseRepository().FindList<RegionPeopleNumEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<RegionPeopleNumEntity>> GetPeopleInfo(RegionParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = GetPeopleInfoSQL(param, strSql);
            var list = await this.BaseRepository().FindList<RegionPeopleNumEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<RegionEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<RegionEntity>(id);
        }

        #endregion

        #region 提交数据
        public async Task SaveForm(RegionEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<RegionEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<RegionEntity>(entity);
            }
        }

        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(RegionParam param, StringBuilder strSql)
        {
            strSql.Append(@"SELECT pepole_Number as PepoleNumber,Current_Station as CurrentStation,Pepole_Name as PepoleName,t.[type_of_work_name],
                            Down_Well_Time as DownWellTime,Station_Address as StationAddress FROM caist_mk_db.dbo.mk_device_information c
                            INNER JOIN(SELECT a.pepole_Number, Current_Station, Down_Well_Time, b.Pepole_Name from  caist_mk_db.dbo.mk_real_data a
                            INNER JOIN caist_mk_db.dbo.mk_staff_information b on a.Pepole_Number = b.Pepole_Number)d on d.Current_Station = c.Station_Number
                            left join caist_mk_db.[dbo].[mk_type_of_work] t on d.pepole_number=t.type_of_work_number");

            strSql.Append(@" WHERE 1 = 1");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (param.CurrentStation.HasValue())
                {
                    strSql.Append(" and Current_Station = @CurrentStation");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@CurrentStation", param.CurrentStation));
                }
            }
            return parameter;
        }

        private List<DbParameter> SqlFilter(RegionParam param, StringBuilder strSql)
        {
            strSql.Append(@"select COUNT(pepole_number) as Nums,Current_Station as CurrentStation,Station_Address as StationAddress
                            from caist_mk_db.dbo.mk_real_data a 
                            inner join caist_mk_db.dbo.mk_device_information b on a.Current_Station=b.Station_Number 
                            GROUP BY Current_Station,Station_Address");

            var parameter = new List<DbParameter>();
          
            return parameter;
        }

        private List<DbParameter> GetPeopleInfoSQL(RegionParam param, StringBuilder strSql)
        {
            strSql.Append(@"SELECT w.Pepole_Number as PepoleNumber,m.Pepole_Name as PepoleName,c.Station_Address as StationAddress,w.Current_Station as CurrentStation,w.Down_Well_Time as DownWellTime FROM caist_mk_db.dbo.mk_real_data w inner JOIN caist_mk_db.dbo.mk_staff_information m ON w.Pepole_Number=m.Pepole_Number INNER JOIN caist_mk_db.dbo.mk_device_information c ON c.Station_Number=w.Current_Station where 1=1");

            var parameter = new List<DbParameter>();
            if (param != null && param.PeopleNumber.HasValue())
            {
                strSql.Append(" and m.Pepole_Number like '%@peopleNumber%'");
                parameter.Add(DbParameterExtension.CreateDbParameter("@peopleNumber", param.PeopleNumber));
            }
            if (param != null && param.PeopleName.HasValue())
            {
                strSql.Append(" and m.Pepole_Name like '%@peopleName%'");
                parameter.Add(DbParameterExtension.CreateDbParameter("@peopleName", param.PeopleName));
            }
            return parameter;
        }
        #endregion
    }
}
