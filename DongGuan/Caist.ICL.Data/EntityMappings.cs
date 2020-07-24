using NPoco.FluentMappings;
using Caist.ICL.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
namespace Caist.ICL.Data
{
    /// <summary>
    /// 实体对象映射
    /// ljl@2018
    /// </summary>
    public class EntityMappings : Mappings
    {
        public EntityMappings()
        {
            For<Base_ParameterConn>().TableName("Base_ParameterConn").PrimaryKey(w => w.Id, false);
            For<Base_ParameterIndexTemplet>().TableName("Base_ParameterIndexTemplet").PrimaryKey(w => w.Id, false);
            For<Dic_Content>().TableName("Dic_Content").PrimaryKey(w => w.Id, false);
            For<Dic_Type>().TableName("Dic_Type").PrimaryKey(w => w.Id, false);
            For<Cable_Info>().TableName("Cable_Info").PrimaryKey(w => w.Id, false);
            For<Cable_Type>().TableName("CableType").PrimaryKey(w => w.Id, false);
            For<Calculation>().TableName("Calculation").PrimaryKey(w => w.Id, false);
            For<Calculation_Detail>().TableName("Calculation_Detail").PrimaryKey(w => w.Id, false);
            For<Equipment>().TableName("Equipment").PrimaryKey(w => w.Id, false);
            For<Section>().TableName("Section").PrimaryKey(w => w.Id, false);
            For<CL_SectionCable>().TableName("CL_SectionCable").PrimaryKey(w => w.Id, false);
            For<Formula>().TableName("Formula").PrimaryKey(w => w.Id, false);
            For<Formula_Detail>().TableName("Formula_Detail").PrimaryKey(w => w.Id, false);
            For<Friction>().TableName("Friction").PrimaryKey(w => w.Id, false);
            For<Geometrical>().TableName("Geometrical").PrimaryKey(w => w.Id, false);
            For<GisCoordinateSystem>().TableName("GisCoordinateSystem").PrimaryKey(w => w.Id, false);
            For<Laying_Path>().TableName("Laying_Path").PrimaryKey(w => w.Id, false);
            For<Monitoring_Data>().TableName("Monitoring_Data").PrimaryKey(w => w.Id, false);
            For<MonitorPoint>().TableName("MonitorPoint").PrimaryKey(w => w.Id, false);
            For<Project_Info>().TableName("Project_Info").PrimaryKey(w => w.Id, false);
            For<ProjectEquipment>().TableName("ProjectEquipment").PrimaryKey(w => w.Id, false);
            For<System_RightObject>().TableName("System_RightObject").PrimaryKey(w => w.Id, false);
            For<System_Role>().TableName("System_Role").PrimaryKey(w => w.Id, false);
            For<System_Role_Right>().TableName("System_Role_Right").PrimaryKey(w => w.Id, false);
            For<System_UserInfo>().TableName("System_UserInfo").PrimaryKey(w => w.Id, false);
            For<System_UserOfRoles>().TableName("System_UserOfRoles").PrimaryKey(w => w.Id, false);
            For<Well_Type>().TableName("Well_Type").PrimaryKey(w => w.Id, false);
            For<Basic_Locus>().TableName("Basic_Locus").PrimaryKey(w => w.Id, false);
            For<System_Menu>().TableName("System_Menu").PrimaryKey(w => w.Id, false);
            For<System_MenuGrant>().TableName("Sys_Menu_Grant").PrimaryKey(w => w.Id, false);
            For<T_ShouLiFenXi>().TableName("T_ShouLiFenXi").PrimaryKey(w => w.Id, false);
        }
    }
}
