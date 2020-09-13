namespace Caist.Siemens
{
    internal class PLCAddress
    {
        private DataType dataType;
        private int dbNumber;
        private int startByte;
        private int bitNumber;
        private VarType varType;

        public DataType DataType
        {
            get => dataType;
            set => dataType = value;
        }

        public int DbNumber
        {
            get => dbNumber;
            set => dbNumber = value;
        }

        public int StartByte
        {
            get => startByte;
            set => startByte = value;
        }

        public int BitNumber
        {
            get => bitNumber;
            set => bitNumber = value;
        }

        public VarType VarType
        {
            get => varType;
            set => varType = value;
        }

        public PLCAddress(string address)
        {
            Parse(address, out dataType, out dbNumber, out varType, out startByte, out bitNumber);
        }

        public static void Parse(string input, out DataType dataType, out int dbNumber, out VarType varType, out int address, out int bitNumber)
        {
            bitNumber = -1;
            dbNumber = 0;

            switch (input.Substring(0, 2))
            {
                case "DB":
                    string[] strings = input.Split(new char[] { '.' });
                    if (strings.Length < 2)
                        throw new InvalidAddressException("为DB地址的几个周期");
                    dataType = DataType.DataBlock;//DB16.DBD12
                    dbNumber = int.Parse(strings[0].Substring(2));
                    address = int.Parse(strings[1].Substring(3));
                    string dbType = strings[1].Substring(0, 3);
                    switch (dbType)
                    {
                        case "DBB":
                            varType = VarType.Byte;
                            return;
                        case "DBW":
                            varType = VarType.Word;
                            return;
                        case "DBD":
                            varType = VarType.DWord;
                            return;
                        case "DBX":
                            bitNumber = int.Parse(strings[2]);
                            if (bitNumber > 7)
                                throw new InvalidAddressException("位只能是0-7");
                            varType = VarType.Bit;
                            return;
                        default:
                            throw new InvalidAddressException();
                    }
                case "IB":
                case "EB":
                    dataType = DataType.Input;
                    dbNumber = 0;
                    address = int.Parse(input.Substring(2));
                    varType = VarType.Byte;
                    return;
                case "IW":
                case "EW":
                    dataType = DataType.Input;
                    dbNumber = 0;
                    address = int.Parse(input.Substring(2));
                    varType = VarType.Word;
                    return;
                case "ID":
                case "ED":
                    dataType = DataType.Input;
                    dbNumber = 0;
                    address = int.Parse(input.Substring(2));
                    varType = VarType.DWord;
                    return;
                case "QB":
                case "AB":
                case "OB":
                    dataType = DataType.Output;
                    dbNumber = 0;
                    address = int.Parse(input.Substring(2));
                    varType = VarType.Byte;
                    return;
                case "QW":
                case "AW":
                case "OW":
                    dataType = DataType.Output;
                    dbNumber = 0;
                    address = int.Parse(input.Substring(2));
                    varType = VarType.Word;
                    return;
                case "QD":
                case "AD":
                case "OD":
                    dataType = DataType.Output;
                    dbNumber = 0;
                    address = int.Parse(input.Substring(2));
                    varType = VarType.DWord;
                    return;
                case "MB":
                    dataType = DataType.Memory;
                    dbNumber = 0;
                    address = int.Parse(input.Substring(2));
                    varType = VarType.Byte;
                    return;
                case "MW":
                    dataType = DataType.Memory;
                    dbNumber = 0;
                    address = int.Parse(input.Substring(2));
                    varType = VarType.Word;
                    return;
                case "MD":
                    dataType = DataType.Memory;
                    dbNumber = 0;
                    address = int.Parse(input.Substring(2));
                    varType = VarType.DWord;
                    return;
                default:
                    switch (input.Substring(0, 1))
                    {
                        case "E":
                        case "I":
                            dataType = DataType.Input;
                            varType = VarType.Bit;
                            break;
                        case "Q":
                        case "A":
                        case "O":
                            dataType = DataType.Output;
                            varType = VarType.Bit;
                            break;
                        case "M":
                            dataType = DataType.Memory;
                            varType = VarType.Bit;
                            break;
                        case "T":
                            dataType = DataType.Timer;
                            dbNumber = 0;
                            address = int.Parse(input.Substring(1));
                            varType = VarType.Timer;
                            return;
                        case "Z":
                        case "C":
                            dataType = DataType.Timer;
                            dbNumber = 0;
                            address = int.Parse(input.Substring(1));
                            varType = VarType.Counter;
                            return;
                        default:
                            throw new InvalidAddressException(string.Format("{0} 是不是一个有效的地址", input.Substring(0, 1)));
                    }

                    string txt2 = input.Substring(1);
                    if (txt2.IndexOf(".") == -1)
                        throw new InvalidAddressException("为DB地址的几个周期");

                    address = int.Parse(txt2.Substring(0, txt2.IndexOf(".")));
                    bitNumber = int.Parse(txt2.Substring(txt2.IndexOf(".") + 1));
                    if (bitNumber > 7)
                        throw new InvalidAddressException("位只能是0-7");
                    return;
            }
        }
    }
}