/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 14:21:52
*说明:			指标模版，包括测点指标和计算指标
    - Base_ParameterIndexTemplet 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// </summary>
    public class Base_ParameterIndexTemplet : BaseEntity
    {
    
        /// <summary>
        /// 指标编码 - IndexCode
        /// </summary>
        public string IndexCode { get; set; }
        /// <summary>
        /// 指标名称 - IndexName
        /// </summary>
        public string IndexName { get; set; }
        /// <summary>
        /// 单位 - Unit
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 最小值 - Min_Value
        /// </summary>
        public decimal? Min_Value { get; set; }
        /// <summary>
        /// 最大值 - Max_Value
        /// </summary>
        public decimal? Max_Value { get; set; }
        /// <summary>
        ///  - Default_Value
        /// </summary>
        public decimal? Default_Value { get; set; }
        /// <summary>
        /// 内部变量，计算函数用 - Local_Var
        /// </summary>
        public string Local_Var { get; set; }
        /// <summary>
        /// 外部变量，计算函数用 - Out_Var
        /// </summary>
        public string Out_Var { get; set; }
        /// <summary>
        /// 外部函数变量，计算函数用 - Out_Var_Func
        /// </summary>
        public string Out_Var_Func { get; set; }
        /// <summary>
        /// 计算公式 - Expression
        /// </summary>
        public string Expression { get; set; }
        /// <summary>
        /// 显示公式（包含注释） - Display_Exp
        /// </summary>
        public string Display_Exp { get; set; }
        /// <summary>
        /// 全局计算公式外部函数变量 - Full_Out_Var_Func
        /// </summary>
        public string Full_Out_Var_Func { get; set; }
        /// <summary>
        /// 全局计算公式外部变量 - Full_Out_Var
        /// </summary>
        public string Full_Out_Var { get; set; }
        /// <summary>
        /// 全局计算公式内部变量 - Full_Local_Var
        /// </summary>
        public string Full_Local_Var { get; set; }
        /// <summary>
        /// 全局名计算公式 - Full_Expression
        /// </summary>
        public string Full_Expression { get; set; }
        /// <summary>
        /// 全局显示公式 - Full_Display_Exp
        /// </summary>
        public string Full_Display_Exp { get; set; }
        /// <summary>
        /// 公式描述 - Exp_Desc
        /// </summary>
        public string Exp_Desc { get; set; }
        /// <summary>
        /// 采集频率 - Frequence
        /// </summary>
        public int? Frequence { get; set; }
        /// <summary>
        /// 1=实时量；2=计算量；3=手工录入量 - IndexType
        /// </summary>
        public int? IndexType { get; set; }
        /// <summary>
        /// 指标修改标识(0:正常 1:新增 2:修改 3:删除) - MFlag
        /// </summary>
        public int? MFlag { get; set; }
        /// <summary>
        /// 是否清零(0:不清零 1:清零) - Is_Clear_Zero
        /// </summary>
        public string Is_Clear_Zero { get; set; }
        /// <summary>
        /// 是否回写(0:不回写 1:回写) - Is_Write_Back
        /// </summary>
        public bool? Is_Write_Back { get; set; }
        /// <summary>
        /// 排序号 - Order_No
        /// </summary>
        public int? Order_No { get; set; }
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
        /// <summary>
        /// 是否删除 - Delteted
        /// </summary>
        public bool? Delteted { get; set; }
    }
}

