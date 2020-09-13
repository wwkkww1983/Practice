namespace Caist.Siemens
{
    /// <summary>
    /// 库支持的S7 cpu类型
    /// </summary>
    public enum CpuType
    {
        /// <summary>
        /// S7 200 cpu type
        /// </summary>
        S7200 = 0,

        /// <summary>
        /// Siemens Logo 0BA8
        /// </summary>
        Logo0BA8 = 1,

        /// <summary>
        /// S7 300 cpu type
        /// </summary>
        S7300 = 10,

        /// <summary>
        /// S7 400 cpu type
        /// </summary>
        S7400 = 20,

        /// <summary>
        /// S7 1200 cpu type
        /// </summary>
        S71200 = 30,

        /// <summary>
        /// S7 1500 cpu type
        /// </summary>
        S71500 = 40,
    }

    /// <summary>
    ///调用函数后可以设置的错误代码类型
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 该功能已正确执行
        /// </summary>
        NoError = 0,

        /// <summary>
        /// 错误类型的CPU错误
        /// </summary>
        WrongCPU_Type = 1,

        /// <summary>
        /// 连接错误
        /// </summary>
        ConnectionError = 2,

        /// <summary>
        /// Ip地址不可用
        /// </summary>
        IPAddressNotAvailable,

        /// <summary>
        /// 变量的格式错误
        /// </summary>
        WrongVarFormat = 10,

        /// <summary>
        /// 接收字节数错误
        /// </summary>
        WrongNumberReceivedBytes = 11,

        /// <summary>
        /// 发送数据错误
        /// </summary>
        SendData = 20,

        /// <summary>
        /// 读取数据错误
        /// </summary>
        ReadData = 30,

        /// <summary>
        /// 写入数据错误
        /// </summary>
        WriteData = 50
    }

    /// <summary>
    /// 可读取的内存区域类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 输入区内存
        /// </summary>
        Input = 129,

        /// <summary>
        /// 输出区域内存
        /// </summary>
        Output = 130,

        /// <summary>
        /// Merkers area内存(M0, M0.0,...)
        /// </summary>
        Memory = 131,

        /// <summary>
        /// DB区域内存(DB1, DB2,...)
        /// </summary>
        DataBlock = 132,

        /// <summary>
        /// 内存区域定时器(T1、T2、...)
        /// </summary>
        Timer = 29,

        /// <summary>
        /// 计数器区域内存(C1, C2,...)
        /// </summary>
        Counter = 28
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum VarType
    {
        /// <summary>
        /// S7位变量类型(bool)
        /// </summary>
        Bit,

        /// <summary>
        /// S7字节变量类型(8位)
        /// </summary>
        Byte,

        /// <summary>
        /// S7字变量类型(16位，2字节)
        /// </summary>
        Word,

        /// <summary>
        /// S7 DWord变量类型(32位，4字节)
        /// </summary>
        DWord,

        /// <summary>
        /// S7 Int变量类型(16位，2字节)
        /// </summary>
        Int,

        /// <summary>
        /// DInt变量类型(32位，4字节)
        /// </summary>
        DInt,

        /// <summary>
        /// 实变量类型(32位，4字节)
        /// </summary>
        Real,

        /// <summary>
        /// 变量类型(变量)
        /// </summary>
        String,

        /// <summary>
        /// 变量类型(变量)
        /// </summary>
        StringEx,

        /// <summary>
        /// 定时器变量类型
        /// </summary>
        Timer,

        /// <summary>
        /// 计数器变量类型
        /// </summary>
        Counter,

        /// <summary>
        /// DateTIme变量类型
        /// </summary>
        DateTime
    }


    public enum SendDataType
    {

        /// <summary>
        /// S7变量类型(Int)
        /// </summary>
        Int,

        /// <summary>
        /// S7位变量类型(UInt32)
        /// </summary>
        UInt32,

        /// <summary>
        /// S7变量类型(Short)
        /// </summary>
        Short,

        /// <summary>
        /// S7变量类型(Ushort)
        /// </summary>
        Ushort,

        /// <summary>
        /// S7变量类型(Float)
        /// </summary>
        Float,
    }
}
