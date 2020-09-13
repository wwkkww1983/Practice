using Caist.ICL.Models;
using System;

namespace Caist.ICL.Library
{
    public class Tools
    {
        public static ForceAnalysis CalcStress(StressEntity senity)
        {
            double diffX = 0;
            double diffY = 0;
            string ifhege = "1";
            double totalqianyingli = 0;
            diffX = senity.EndX.ToDouble() - senity.StartX.ToDouble();
            diffY = senity.EndY.ToDouble() - senity.StartY.ToDouble();
            if (diffX < 0)
            {
                diffX = 0 - diffX;
            }
            if (diffY < 0)
            {
                diffY = 0 - diffY;
            }
            double angle = Math.Atan2(diffY, diffX) * 180 / Math.PI;
            double lenghtss = Math.Sqrt(Math.Pow(senity.StartX.ToDouble() - senity.EndX.ToDouble(), 2) + Math.Pow(senity.StartY.ToDouble() - senity.EndY.ToDouble(), 2)); //两个点的长度
            var T1 = (9.8) * lenghtss * (0.2 * Math.Cos(angle) + Math.Sin(angle)); //起始牵引力 
            if (T1 < 0)
            {
                T1 = 0 - T1;
            }
            var T2 = (9.8) * (lenghtss) * (0.2 * Math.Cos(angle) - Math.Sin(angle)); //最终牵引力
            if (T2 < 0)
            {
                T2 = 0 - T2;
            }
            totalqianyingli += T2;
            var vT2 = T1 + (9.8) * (lenghtss) / Math.Cos(angle) * (0.2 * Math.Cos(angle) - Math.Sin(angle)); // 最终牵引力比较
            if (vT2 < 0)
            {
                vT2 = 0 - vT2;
            }
            string jianyi = "";
            if (Convert.ToDecimal(vT2) > Convert.ToDecimal(300))
            {
                ifhege = "0";
                jianyi = "建议添加滑轮";
                if (senity.Index % 2 == 0)
                {
                    jianyi = "建议添加输送机";
                }
            }
            else
            {
                ifhege = "1";
            }
            string section = "";
            if (senity.values[2] != null && senity.dd[2] != null)
            {
                section = senity.values[2].ToString() + "-" + senity.dd[2].ToString();
            }

            var myshouli = new ForceAnalysis();
            myshouli.StartX = senity.StartX;
            myshouli.StartY = senity.StartY;
            myshouli.Id = Guid.NewGuid().ToString();
            myshouli.Section = section;
            myshouli.Lengths = lenghtss.ToString();
            myshouli.BuryType = "管沟";
            myshouli.IfQualify = ifhege;
            myshouli.ProposedProg = jianyi;
            if (senity.values[8] != null)
            {
                myshouli.Material = senity.values[8].ToString();
            }
            myshouli.ForceValue1 = lenghtss.ToString("0.00");
            myshouli.ForceValue2 = totalqianyingli.ToString("0.00");
            myshouli.EndX = senity.EndX;
            myshouli.EndY = senity.EndY;
            return myshouli;
        }
    }
}
