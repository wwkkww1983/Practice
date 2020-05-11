using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using PLCComHelperProj;

// Token: 0x02000009 RID: 9
internal class PLCLink
{
    public string method_0()
    {
        return this.IPSetAdd;
    }
    public void SetIP(string string_1)
    {
        this.IPSetAdd = string_1;
    }

    ///<summary >传递私有参数device</summary>
	public Device GetDevice()
    {
        return this.device;
    }

    public void method_3(Device device_1)
    {
        this.device = device_1;
    }

    public int method_4()
    {
        return this.int_2;
    }

    public void method_5(int int_3)
    {
        if (int_3 < 20)
        {
            this.int_2 = 20;
            return;
        }
        this.int_2 = int_3;
    }

    public int commStatus()
    {
        return this.PLCStatue;
    }

    private bool PLCconnect()
    {
        bool result = false;
        bool result2;
        try
        {
            this.PLCStatue = 1;//正在连接状态
            if (this.socket_0.Connected)//如果套接字处于打开状态、则关闭
            {
                this.socket_0.Close();
            }
            this.socket_0 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IAsyncResult asyncResult = this.socket_0.BeginConnect(this.IPSetAdd, this.TCPPort, null, null);
            DateTime now = DateTime.Now;
            while (!asyncResult.IsCompleted)//超时检测
            {
                DateTime now2 = DateTime.Now;
                if ((now2 - now).TotalMilliseconds > 3000.0)
                {
                    return false;
                }
                Thread.Sleep(50);//每间隔50ms检测一次
            }
            result = true;
            this.socket_0.EndConnect(asyncResult);//终止连接
            this.socket_0.ReceiveTimeout = 3000;//3000ms无数据接收则超时
            return result;
        }
        catch (Exception)
        {
            result2 = false;
        }
        return result2;
    }

    private void method_8()
    {
        if (this.socket_0 != null)
        {
            this.socket_0.Close();
        }
        this.PLCStatue = 0;
    }

    public bool method_9()
    {
        this.PLCStatue = 0;
        return true;
    }
    public bool PLC_StartConnect()//开始连接
    {
        bool result;
        if (result = this.PLCconnect())//PLC连接状态返回值=0 连接失败
        {
            this.PLCStatue = 2; /*TCP连接成功*/
            this.StopFlag = false;
            this.thread = new Thread(new ParameterizedThreadStart(this.GetStatue));
            this.thread.IsBackground = true;
            this.thread.Start(this);
        }
        else
        {
            this.PLCStatue = 0; /*未连接*/
        }
        return result;
    }

    public void PLC_StopConnect()
    {
        this.StopFlag = true;
        if (this.thread != null && this.thread.IsAlive)
        {
            this.thread.Join(1000);
        }
        this.method_8();
    }

    private int method_12(string string_1, int int_3)
    {
        return this.device.TagGroups[string_1].GetByteData(int_3);
    }

    private int method_13(string string_1, int int_3)
    {
        return this.device.TagGroups[string_1].GetIntData(int_3);
    }

    private float method_14(string string_1, int int_3)
    {
        return this.device.TagGroups[string_1].GetFloatData(int_3);
    }

    private short method_15(string string_1, int int_3)
    {
        return this.device.TagGroups[string_1].GetShortValue(int_3);
    }

    private int method_16(string string_1, int int_3, int int_4)
    {
        return this.device.TagGroups[string_1].GetBitData(int_3, int_4);
    }

    public object getValue(string str)
    {
        str = str.Trim().ToUpper();
        string[] array = str.Split(new char[] { '.' });
        string text = array[0];
        string key = array[1];
        TagGroup tagGroup = this.device.TagGroups[text];
        Tag tag = tagGroup.Tags[key];
        object result;
        switch (tag.CheckDataType())
        {
            case e_PLC_DATA_TYPE.TYPE_INT:
                result = this.method_13(text, Convert.ToInt32(tag.Address));
                break;
            case e_PLC_DATA_TYPE.TYPE_FLOAT:
                result = this.method_14(text, Convert.ToInt32(tag.Address));
                break;
            case e_PLC_DATA_TYPE.TYPE_BOOL:
                {
                    string[] array2 = tag.Address.Split(new char[]
                    {
                '.'
                    });
                    int int_ = Convert.ToInt32(array2[0]);
                    int int_2 = Convert.ToInt32(array2[1]);
                    result = this.method_16(text, int_, int_2);
                    break;
                }
            case e_PLC_DATA_TYPE.TYPE_SHORT:
                result = this.method_15(text, Convert.ToInt32(tag.Address));
                break;
            case e_PLC_DATA_TYPE.TYPE_BYTE:
                result = this.method_12(text, Convert.ToInt32(tag.Address));
                break;
            default:
                result = null;//-9999f;
                break;
        }
        return result;
    }

    private void GetStatue(object object_0)//独立线程处理状态
    {
        bool flag = false;
        while (!this.StopFlag)
        {
            #region  while
            if (!flag)
            {
                if (!this.device.ConnectToPLC(this.socket_0))
                {
                    flag = false;
                    this.PLCStatue = 5; /*PLC握手错误*/
                    continue;
                }
                flag = true;
                this.PLCStatue = 3; /*PLC握手成功*/
            }
            if (this.device.ProcessPLCData(this.socket_0, this.StopFlag))
            {
                this.PLCStatue = 4; /*正常采集过程中..*/
            }
            else
            {
                this.PLCStatue = 6; /*通讯错误*/
                while (!this.StopFlag)
                {
                    if (this.PLCconnect())
                    {
                        flag = false;
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }
            Thread.Sleep(this.int_2);
            #endregion
        }
        this.PLCStatue = 0; /*未连接*/
    }
    public void WriteData(string name, double value)//写数据
    {
        name = name.Trim().ToUpper();
        string[] array = name.Split(new char[] { '.' });//以.分隔数组
        string key = array[0];
        string key2 = array[1];
        TagGroup tagGroup = this.device.TagGroups[key];
        Tag tag = tagGroup.Tags[key2];
        e_PLC_MMType e_PLC_MMType_ = tagGroup.Get_MMtype();
        e_PLC_DATA_TYPE e_PLC_DATA_TYPE_ = tag.CheckDataType();
        if (this.BackTrue(e_PLC_MMType_, e_PLC_DATA_TYPE_))
        {
            new WriteData(tagGroup, tag, value);
            this.device.AddWriteData(tagGroup, tag, value);
        }
    }
    private bool BackTrue(e_PLC_MMType e_PLC_MMType_0, e_PLC_DATA_TYPE e_PLC_DATA_TYPE_0)
    {
        return true;
    }

    public PLCLink() : base()//构造函数
    {
        this.IPSetAdd = FuckProtect.DataFrom(1742);
        this.TCPPort = 102;
        this.socket_0 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        this.device = new Device();
        this.int_2 = 100;

    }
    private string IPSetAdd;
    private bool StopFlag;
    public int TCPPort;
    private Socket socket_0;
    private int PLCStatue;
    private Device device;
    private int int_2;
    private Thread thread;
}
