using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace PLCComHelperProj
{

    public class ClientComHelper
    {
        private readonly int Fuck_int;
        private PLCLink plcSet;
        private XmlDocument xmlDocument;
        private string XmlString;
        private bool ProtectFlg = true;

        ///<summary >xml文件路径</summary>
        public string TagConfigFile//xml文件路径
        {
            get
            {
                return this.XmlString;
            }
            set
            {
                this.XmlString = value;
            }
        }
        ///<summary >连接状态</summary>
		public int CommStatus//连接状态
        {
            get
            {
                return this.plcSet.commStatus();
            }
        }
        ///<summary >设置IP地址</summary>
		public string IP//设置IP地址
        {
            set
            {
                this.plcSet.SetIP(value);
            }
        }

        ///<summary >xml文件读取</summary>
        public bool Init()//xml文件读取
        {
            int num = 0;
            if (!File.Exists(XmlString))//文件存在
            {
                return false;
            }
            this.plcSet.GetDevice().Clear();//清除原始数据
            this.xmlDocument.Load(this.XmlString);//载入xml
            XmlNode documentElement = this.xmlDocument.DocumentElement;//获取文档的根 
            XmlNode xmlNode = documentElement.SelectSingleNode("/Device");//选择匹配 XPath 表达式的第一个 XmlNode  ip

            this.plcSet.SetIP(xmlNode.Attributes["ip"].Value);
            this.plcSet.GetDevice().CPU_SlotNO = xmlNode.Attributes["cpuSlotNO"].Value;
            this.plcSet.GetDevice().PLCType = xmlNode.Attributes["PLCType"].Value;
            this.plcSet.GetDevice().LocalTASP = xmlNode.Attributes["localTASP"].Value;
            this.plcSet.GetDevice().RemoteTASP = xmlNode.Attributes["remoteTASP"].Value;

            XmlNodeList xmlNodeList = documentElement.SelectNodes("//TagGroup");
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                string text = xmlNodeList[i].Attributes["name"].Value.ToUpper();//"Group_M01"
                string value = xmlNodeList[i].Attributes["mmtype"].Value;
                int block = (xmlNodeList[i].Attributes["block"].Value == "") ? 0 : Convert.ToInt32(xmlNodeList[i].Attributes["block"].Value);
                int address = Convert.ToInt32(xmlNodeList[i].Attributes["beginAddress"].Value);
                int readCount = Convert.ToInt32(xmlNodeList[i].Attributes["readCount"].Value);

                this.plcSet.GetDevice().TagGroups.Add(text, new TagGroup(text, value, block, address, readCount));// name="Group_M01" mmtype="M" block="" beginAddress="0" readCount="200"
                XmlNodeList xmlNodeList2 = xmlNodeList[i].SelectNodes("Tag");
                for (int j = 0; j < xmlNodeList2.Count; j++)
                {
                    string text2 = xmlNodeList2[j].Attributes["name"].Value.ToUpper();
                    string value2 = xmlNodeList2[j].Attributes["dataType"].Value;
                    string value3 = xmlNodeList2[j].Attributes["address"].Value;
                    string value4 = xmlNodeList2[j].Attributes["desc"].Value;
                    TagGroup tagGroup = this.plcSet.GetDevice().TagGroups[text];
                    tagGroup.Tags.Add(text2, new Tag(tagGroup, text2, value3, value2, value4));
                    num++;
                }
            }
            if (num > this.Fuck_int)
            {
                MessageBox.Show(FuckProtect.DataFrom(452));
                ProtectFlg = false;
                return false;
            }
            ProtectFlg = true;
            return this.plcSet.method_9();
        }
        ///<summary >开始连接</summary>
		public bool Start()//开始连接
        {
            if (ProtectFlg)
            {
                return this.plcSet.PLC_StartConnect();
            }
            return false;
        }
        public void Stop()//停止连接
        {
            this.plcSet.PLC_StopConnect();
        }

        public object GetValue(string name)
        {
            return this.plcSet.getValue(name);
        }
        public Device GetDevice()//显示数据
        {
            return this.plcSet.GetDevice();
        }

        public void WriteData(string name, double value)//写数据
        {
            if (this.ValuTs(value.ToString()))
            {
                this.plcSet.WriteData(name, value);
            }
        }
        private bool ValuTs(string str)//判断数据是否存在
        {
            if (str == "")
            {
                return false;
            }
            Regex regex = new Regex(FuckProtect.DataFrom(522));//表示不可变的正则表达式
            return regex.IsMatch(str);//构造函数中指定的正则表达式在指定的输入字符串中是否找到了匹配项
        }
        public ClientComHelper() : base()
        {
            this.Fuck_int = 2000; //呵呵16点限制在这里
            this.plcSet = new PLCLink();
            this.xmlDocument = new XmlDocument();
            this.XmlString = Path.Combine(Application.StartupPath, FuckProtect.DataFrom(578));
        }

    }
}
