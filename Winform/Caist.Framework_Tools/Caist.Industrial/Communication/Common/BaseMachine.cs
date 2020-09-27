﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace Communication.Common
{
    /// <summary>
    ///     读写设备值的方式
    /// </summary>
    public enum MachineDataType
    {
        /// <summary>
        ///     地址
        /// </summary>
        Address,

        /// <summary>
        ///     通讯标识
        /// </summary>
        CommunicationTag,

        /// <summary>
        ///     名称
        /// </summary>
        Name,

        /// <summary>
        ///     Id
        /// </summary>
        Id
    }

    /// <summary>
    ///     获取设备值的方式
    /// </summary>
    public enum MachineGetDataType
    {
        /// <summary>
        ///     地址
        /// </summary>
        Address,

        /// <summary>
        ///     通讯标识
        /// </summary>
        CommunicationTag,

        /// <summary>
        ///     名称
        /// </summary>
        Name,

        /// <summary>
        ///     Id
        /// </summary>
        Id
    }

    /// <summary>
    ///     向设备设置值的方式
    /// </summary>
    public enum MachineSetDataType
    {
        /// <summary>
        ///     地址
        /// </summary>
        Address,

        /// <summary>
        ///     通讯标识
        /// </summary>
        CommunicationTag,

        /// <summary>
        ///     名称
        /// </summary>
        Name,

        /// <summary>
        ///     Id
        /// </summary>
        Id
    }

    /// <summary>
    ///     设备
    /// </summary>
    public abstract class BaseMachine : BaseMachine<string, string>
    {
        /// <summary>
        ///     构造器
        /// </summary>
        /// <param name="getAddresses">需要与设备通讯的地址</param>
        protected BaseMachine(IEnumerable<AddressUnit> getAddresses) : base(getAddresses)
        {
        }

        /// <summary>
        ///     构造器
        /// </summary>
        /// <param name="getAddresses">需要与设备通讯的地址</param>
        /// <param name="keepConnect">是否保持连接</param>
        protected BaseMachine(IEnumerable<AddressUnit> getAddresses, bool keepConnect)
            : base(getAddresses, keepConnect)
        {
        }

        /// <summary>
        ///     构造器
        /// </summary>
        /// <param name="getAddresses">需要与设备通讯的地址</param>
        /// <param name="keepConnect">是否保持连接</param>
        /// <param name="slaveAddress">从站地址</param>
        /// <param name="masterAddress">主站地址</param>
        protected BaseMachine(IEnumerable<AddressUnit> getAddresses, bool keepConnect, byte slaveAddress,
            byte masterAddress) : base(getAddresses, keepConnect, slaveAddress, masterAddress)
        {
        }
    }

    /// <summary>
    ///     设备
    /// </summary>
    /// <typeparam name="TKey">设备的Id类型</typeparam>
    /// <typeparam name="TUnitKey">设备中使用的AddressUnit的Id类型</typeparam>
    public abstract class BaseMachine<TKey, TUnitKey> : IMachineMethodData, IMachineProperty<TKey>
        where TKey : IEquatable<TKey>
        where TUnitKey : IEquatable<TUnitKey>
    {
        private readonly int _maxErrorCount = 3;

        /// <summary>
        ///     构造器
        /// </summary>
        /// <param name="getAddresses">需要与设备通讯的地址</param>
        protected BaseMachine(IEnumerable<AddressUnit<TUnitKey>> getAddresses)
            : this(getAddresses, false)
        {
        }

        /// <summary>
        ///     构造器
        /// </summary>
        /// <param name="getAddresses">需要与设备通讯的地址</param>
        /// <param name="keepConnect">是否保持连接</param>
        protected BaseMachine(IEnumerable<AddressUnit<TUnitKey>> getAddresses, bool keepConnect)
        {
            GetAddresses = getAddresses;
            KeepConnect = keepConnect;
        }

        /// <summary>
        ///     构造器
        /// </summary>
        /// <param name="getAddresses">需要与设备通讯的地址</param>
        /// <param name="keepConnect">是否保持连接</param>
        /// <param name="slaveAddress">从站地址</param>
        /// <param name="masterAddress">主站地址</param>
        protected BaseMachine(IEnumerable<AddressUnit<TUnitKey>> getAddresses, bool keepConnect, byte slaveAddress,
            byte masterAddress) : this(getAddresses, keepConnect)
        {
            SlaveAddress = slaveAddress;
            MasterAddress = masterAddress;
        }

        private int ErrorCount { get; set; }

        /// <summary>
        ///     地址编码器
        /// </summary>
        public AddressFormater AddressFormater { get; set; }

        /// <summary>
        ///     获取地址组合器
        /// </summary>
        public AddressCombiner<TUnitKey> AddressCombiner { get; set; }

        /// <summary>
        ///     写入地址组合器
        /// </summary>
        public AddressCombiner<TUnitKey> AddressCombinerSet { get; set; }

        /// <summary>
        ///     地址转换器
        /// </summary>
        public AddressTranslator AddressTranslator
        {
            get { return BaseUtility.AddressTranslator; }
            set { BaseUtility.AddressTranslator = value; }
        }

        /// <summary>
        ///     与设备实际通讯的连续地址
        /// </summary>
        protected IEnumerable<CommunicationUnit<TUnitKey>> CommunicateAddresses
            => GetAddresses != null ? AddressCombiner.Combine(GetAddresses) : null;

        /// <summary>
        ///     描述需要与设备通讯的地址
        /// </summary>
        public IEnumerable<AddressUnit<TUnitKey>> GetAddresses { get; set; }

        /// <summary>
        ///     从站号
        /// </summary>
        public byte SlaveAddress { get; set; } = 2;

        /// <summary>
        ///     主站号
        /// </summary>
        public byte MasterAddress { get; set; }

        /// <summary>
        ///     读取数据
        /// </summary>
        /// <returns>从设备读取的数据</returns>
        public Dictionary<string, ReturnUnit> GetDatas(MachineGetDataType getDataType)
        {
            return AsyncHelper.RunSync(() => GetDatasAsync(getDataType));
        }


        /// <summary>
        ///     读取数据
        /// </summary>
        /// <returns>从设备读取的数据</returns>
        public async Task<Dictionary<string, ReturnUnit>> GetDatasAsync(MachineGetDataType getDataType)
        {
            try
            {
                var ans = new Dictionary<string, ReturnUnit>();
                //检测并连接设备
                if (!BaseUtility.IsConnected)
                    await BaseUtility.ConnectAsync();
                //如果无法连接，终止
                if (!BaseUtility.IsConnected) return null;
                //遍历每一个实际向设备获取数据的连续地址
                foreach (var communicateAddress in CommunicateAddresses)
                {
                    //获取数据
                    var datas =
                        await
                            BaseUtility.GetUtilityMethods<IUtilityMethodData>().GetDatasAsync(
                                AddressFormater.FormatAddress(communicateAddress.Area, communicateAddress.Address,
                                    communicateAddress.SubAddress),
                                (int)
                                Math.Ceiling(communicateAddress.GetCount *
                                             BigEndianValueHelper.Instance.ByteLength[
                                                 communicateAddress.DataType.FullName]));


                    //如果没有数据，终止
                    if (datas == null || datas.Length != 0 && datas.Length <
                        (int)
                        Math.Ceiling(communicateAddress.GetCount *
                                     BigEndianValueHelper.Instance.ByteLength[
                                         communicateAddress.DataType.FullName]))
                        return null;


                    foreach (var address in communicateAddress.OriginalAddresses)
                    {
                        //字节坐标的位置
                        var localPos = AddressHelper.MapProtocalCoordinateToAbstractCoordinate(address.Address,
                                           communicateAddress.Address,
                                           AddressTranslator.GetAreaByteLength(communicateAddress.Area)) +
                                       address.SubAddress * 0.125;
                        //字节坐标的主地址位置
                        var localMainPos = (int) localPos;
                        //字节坐标的子地址位置
                        var localSubPos = (int) ((localPos - localMainPos) * 8);

                        //根据类型选择返回结果的键是通讯标识还是地址
                        string key;
                        switch (getDataType)
                        {
                            case MachineGetDataType.CommunicationTag:
                            {
                                key = address.CommunicationTag;
                                break;
                            }
                            case MachineGetDataType.Address:
                            {
                                key = AddressFormater.FormatAddress(address.Area, address.Address, address.SubAddress);
                                break;
                            }
                            case MachineGetDataType.Name:
                            {
                                key = address.Name;
                                break;
                            }
                            case MachineGetDataType.Id:
                            {
                                key = address.Id.ToString();
                                break;
                            }
                            default:
                            {
                                key = address.CommunicationTag;
                                break;
                            }
                        }

                        try
                        {
                            //如果没有数据返回空
                            if (datas.Length == 0)
                                ans.Add(key, new ReturnUnit
                                {
                                    PlcValue = null,
                                    UnitExtend = address.UnitExtend
                                });
                            else
                                ans.Add(key,
                                    new ReturnUnit
                                    {
                                        PlcValue =
                                            Convert.ToDouble(
                                                ValueHelper.GetInstance(BaseUtility.Endian)
                                                    .GetValue(datas, ref localMainPos, ref localSubPos,
                                                        address.DataType)) * address.Zoom,
                                        UnitExtend = address.UnitExtend
                                    });
                        }
                        catch (Exception e)
                        {
                            ErrorCount++;
                            Log.Error(e, $"BaseMachine -> GetDatas, Id:{Id} Connection:{ConnectionToken} key {key} existing. ErrorCount {ErrorCount}.");

                            if (ErrorCount >= _maxErrorCount)
                                Disconnect();
                            return null;
                        }
                    }
                }
                //如果不保持连接，断开连接
                if (!KeepConnect)
                    BaseUtility.Disconnect();
                //返回数据
                if (ans.All(p => p.Value.PlcValue == null)) ans = null;
                ErrorCount = 0;
                return ans;
            }
            catch (Exception e)
            {
                ErrorCount++;
                Log.Error(e, $"BaseMachine -> GetDatas, Id:{Id} Connection:{ConnectionToken} error. ErrorCount {ErrorCount}.");
                
                if (ErrorCount >= _maxErrorCount)
                    Disconnect();
                return null;
            }
        }

        /// <summary>
        ///     写入数据
        /// </summary>
        /// <param name="setDataType">写入类型</param>
        /// <param name="values">需要写入的数据字典，当写入类型为Address时，键为需要写入的地址，当写入类型为CommunicationTag时，键为需要写入的单元的描述</param>
        /// <returns>是否写入成功</returns>
        public bool SetDatas(MachineSetDataType setDataType, Dictionary<string, double> values)
        {
            return AsyncHelper.RunSync(() => SetDatasAsync(setDataType, values));
        }

        /// <summary>
        ///     写入数据
        /// </summary>
        /// <param name="setDataType">写入类型</param>
        /// <param name="values">需要写入的数据字典，当写入类型为Address时，键为需要写入的地址，当写入类型为CommunicationTag时，键为需要写入的单元的描述</param>
        /// <returns>是否写入成功</returns>
        public async Task<bool> SetDatasAsync(MachineSetDataType setDataType, Dictionary<string, double> values)
        {
            try
            {
                //检测并连接设备
                if (!BaseUtility.IsConnected)
                    await BaseUtility.ConnectAsync();
                //如果设备无法连接，终止
                if (!BaseUtility.IsConnected) return false;
                var addresses = new List<AddressUnit<TUnitKey>>();
                //遍历每个要设置的值
                foreach (var value in values)
                {
                    //根据设置类型找到对应的地址描述
                    AddressUnit<TUnitKey> address = null;
                    switch (setDataType)
                    {
                        case MachineSetDataType.Address:
                        {
                            address =
                                GetAddresses.SingleOrDefault(
                                    p =>
                                        AddressFormater.FormatAddress(p.Area, p.Address, p.SubAddress) == value.Key ||
                                        p.DataType != typeof(bool) &&
                                        AddressFormater.FormatAddress(p.Area, p.Address) == value.Key);
                            break;
                        }
                        case MachineSetDataType.CommunicationTag:
                        {
                            address =
                                GetAddresses.SingleOrDefault(p => p.CommunicationTag == value.Key);
                            break;
                        }
                        case MachineSetDataType.Name:
                        {
                            address = GetAddresses.SingleOrDefault(p => p.Name == value.Key);
                            break;
                        }
                        case MachineSetDataType.Id:
                        {
                            address = GetAddresses.SingleOrDefault(p => p.Id.ToString() == value.Key);
                            break;
                        }
                        default:
                        {
                            address =
                                GetAddresses.SingleOrDefault(p => p.CommunicationTag == value.Key);
                            break;
                        }
                    }
                    //地址为空报错
                    if (address == null)
                    {
                        Log.Error($"Machine {ConnectionToken} Address {value.Key} doesn't exist.");
                        continue;
                    }
                    //不能写报错
                    if (!address.CanWrite)
                    {
                        Log.Error($"Machine {ConnectionToken} Address {value.Key} cannot write.");
                        continue;
                    }
                    addresses.Add(address);
                }
                //将地址编码成与实际设备通讯的地址
                var communcationUnits = AddressCombinerSet.Combine(addresses);
                //遍历每条通讯的连续地址
                foreach (var communicateAddress in communcationUnits)
                {
                    //编码开始地址
                    var addressStart = AddressFormater.FormatAddress(communicateAddress.Area,
                        communicateAddress.Address);

                    var datasReturn =
                        await BaseUtility.GetUtilityMethods<IUtilityMethodData>().GetDatasAsync(
                            AddressFormater.FormatAddress(communicateAddress.Area, communicateAddress.Address, 0),
                            (int)
                            Math.Ceiling(communicateAddress.GetCount *
                                         BigEndianValueHelper.Instance.ByteLength[
                                             communicateAddress.DataType.FullName]));

                    var valueHelper = ValueHelper.GetInstance(BaseUtility.Endian);
                    //如果设备本身能获取到数据但是没有数据
                    var datas = datasReturn;

                    //如果没有数据，终止
                    if (datas == null || datas.Length <
                        (int)
                        Math.Ceiling(communicateAddress.GetCount *
                                     BigEndianValueHelper.Instance.ByteLength[
                                         communicateAddress.DataType.FullName]))
                        return false;

                    foreach (var addressUnit in communicateAddress.OriginalAddresses)
                    {
                        //字节坐标地址
                        var byteCount =
                            AddressHelper.MapProtocalGetCountToAbstractByteCount(
                                addressUnit.Address - communicateAddress.Address +
                                addressUnit.SubAddress * 0.125 /
                                AddressTranslator.GetAreaByteLength(communicateAddress.Area),
                                AddressTranslator.GetAreaByteLength(communicateAddress.Area), 0);
                        //字节坐标主地址
                        var mainByteCount = (int) byteCount;
                        //字节坐标自地址
                        var localByteCount = (int) ((byteCount - (int) byteCount) * 8);

                        //协议坐标地址
                        var localPos = byteCount / AddressTranslator.GetAreaByteLength(communicateAddress.Area);
                        //协议坐标子地址
                        var subPos =
                            (int)
                            ((localPos - (int) localPos) /
                             (0.125 / AddressTranslator.GetAreaByteLength(communicateAddress.Area)));
                        //协议主地址字符串
                        var address = AddressFormater.FormatAddress(communicateAddress.Area,
                            communicateAddress.Address + (int) localPos, subPos);
                        //协议完整地址字符串
                        var address2 = subPos != 0
                            ? null
                            : AddressFormater.FormatAddress(communicateAddress.Area,
                                communicateAddress.Address + (int) localPos);
                        //获取写入类型
                        var dataType = addressUnit.DataType;
                        KeyValuePair<string, double> value;
                        switch (setDataType)
                        {
                            case MachineSetDataType.Address:
                            {
                                //获取要写入的值
                                value =
                                    values.SingleOrDefault(
                                        p => p.Key == address || address2 != null && p.Key == address2);
                                break;
                            }
                            case MachineSetDataType.CommunicationTag:
                            {
                                value = values.SingleOrDefault(p => p.Key == addressUnit.CommunicationTag);
                                break;
                            }
                            case MachineSetDataType.Name:
                            {
                                value = values.SingleOrDefault(p => p.Key == addressUnit.Name);
                                break;
                            }
                            case MachineSetDataType.Id:
                            {
                                value = values.SingleOrDefault(p => p.Key == addressUnit.Id.ToString());
                                break;
                            }
                            default:
                            {
                                value = values.SingleOrDefault(p => p.Key == addressUnit.CommunicationTag);
                                break;
                            }
                        }
                        //将要写入的值加入队列
                        var data = Convert.ChangeType(value.Value / addressUnit.Zoom, dataType);

                        if (!valueHelper.SetValue(datas, mainByteCount, localByteCount, data))
                            return false;
                    }
                    //写入数据
                    await
                        BaseUtility.GetUtilityMethods<IUtilityMethodData>().SetDatasAsync(addressStart,
                            valueHelper.ByteArrayToObjectArray(datas,
                                new KeyValuePair<Type, int>(communicateAddress.DataType, communicateAddress.GetCount)));
                }
                //如果不保持连接，断开连接
                if (!KeepConnect)
                    BaseUtility.Disconnect();
            }
            catch (Exception e)
            {
                ErrorCount++;
                Log.Error(e, $"BaseMachine -> SetDatas, Id:{Id} Connection:{ConnectionToken} error. ErrorCount {ErrorCount}.");

                if (ErrorCount >= _maxErrorCount)
                    Disconnect();
                return false;
            }
            return true;
        }

        /// <summary>
        ///     是否处于连接状态
        /// </summary>
        public bool IsConnected => BaseUtility.IsConnected;

        /// <summary>
        ///     是否保持连接
        /// </summary>
        public bool KeepConnect { get; set; }

        /// <summary>
        ///     设备的连接器
        /// </summary>
        public IUtilityProperty BaseUtility { get; protected set; }

        /// <summary>
        ///     设备的Id
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        ///     设备所在工程的名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        ///     设备的名称
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        ///     标识设备的连接关键字
        /// </summary>
        public string ConnectionToken => BaseUtility.ConnectionToken;

        /// <summary>
        ///     获取设备的方法集合
        /// </summary>
        /// <typeparam name="TMachineMethod">方法集合的类型</typeparam>
        /// <returns>设备的方法集合</returns>
        public TMachineMethod GetMachineMethods<TMachineMethod>() where TMachineMethod : class, IMachineMethod
        {
            if (this is TMachineMethod)
            {
                return this as TMachineMethod;
            }
            return null;
        }

        /// <summary>
        ///     连接设备
        /// </summary>
        /// <returns>是否连接成功</returns>
        public bool Connect()
        {
            return BaseUtility.Connect();
        }

        /// <summary>
        ///     连接设备
        /// </summary>
        /// <returns>是否连接成功</returns>
        public async Task<bool> ConnectAsync()
        {
            return await BaseUtility.ConnectAsync();
        }

        /// <summary>
        ///     断开设备
        /// </summary>
        /// <returns>是否断开成功</returns>
        public bool Disconnect()
        {
            return BaseUtility.Disconnect();
        }

        /// <summary>
        ///     获取设备的Id，字符串格式
        /// </summary>
        /// <returns></returns>
        public string GetMachineIdString()
        {
            return Id.ToString();
        }

        /// <summary>
        ///     通过Id获取数据字段定义
        /// </summary>
        /// <param name="addressUnitId">数据字段Id</param>
        /// <returns>数据字段</returns>
        public AddressUnit<TUnitKey> GetAddressUnitById(TUnitKey addressUnitId)
        {
            try
            {
                return GetAddresses.SingleOrDefault(p => p.Id.Equals(addressUnitId));
            }
            catch (Exception e)
            {
                Log.Error(e, $"BaseMachine -> GetAddressUnitById Id:{Id} ConnectionToken:{ConnectionToken} addressUnitId:{addressUnitId} Repeated");
                return null;
            }
        }

        /// <summary>
        ///     获取Utility
        /// </summary>
        /// <typeparam name="TUtilityMethod">Utility实现的接口名称</typeparam>
        /// <returns></returns>
        public TUtilityMethod GetUtility<TUtilityMethod>() where TUtilityMethod : class, IUtilityMethod
        {
            return BaseUtility as TUtilityMethod;
        }
    }

    internal class BaseMachineEqualityComparer<TKey> : IEqualityComparer<IMachineProperty<TKey>>
        where TKey : IEquatable<TKey>
    {
        public bool Equals(IMachineProperty<TKey> x, IMachineProperty<TKey> y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(IMachineProperty<TKey> obj)
        {
            return obj.GetHashCode();
        }
    }

    /// <summary>
    ///     通讯单元
    /// </summary>
    public class CommunicationUnit : CommunicationUnit<string>
    {
    }

    /// <summary>
    ///     通讯单元
    /// </summary>
    public class CommunicationUnit<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        ///     地址
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        ///     子地址
        /// </summary>
        public int SubAddress { get; set; } = 0;

        /// <summary>
        ///     获取个数
        /// </summary>
        public int GetCount { get; set; }

        /// <summary>
        ///     数据类型
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        ///     原始的地址
        /// </summary>
        public IEnumerable<AddressUnit<TKey>> OriginalAddresses { get; set; }
    }

    /// <summary>
    ///     数据单元扩展，返回数据时会同时将其返回
    /// </summary>
    public class UnitExtend
    {
    }

    /// <summary>
    ///     返回的数据单元
    /// </summary>
    public class ReturnUnit
    {
        /// <summary>
        ///     返回的数据
        /// </summary>
        public double? PlcValue { get; set; }

        /// <summary>
        ///     数据的扩展
        /// </summary>
        public UnitExtend UnitExtend { get; set; }
    }

    /// <summary>
    ///     地址单元
    /// </summary>
    public class AddressUnit : AddressUnit<string>
    {
    }

    /// <summary>
    ///     地址单元
    /// </summary>
    public class AddressUnit<TKey> : IEquatable<AddressUnit<TKey>> where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     数据单元Id
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        ///     数据所属的区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        ///     地址
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        ///     bit位地址
        /// </summary>
        public int SubAddress { get; set; } = 0;

        /// <summary>
        ///     数据类型
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        ///     放缩比例
        /// </summary>
        public double Zoom { get; set; } = 1;

        /// <summary>
        ///     小数位数
        /// </summary>
        public int DecimalPos { get; set; }

        /// <summary>
        ///     通讯标识名称
        /// </summary>
        public string CommunicationTag { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        ///     是否可写，默认可写
        /// </summary>
        public bool CanWrite { get; set; } = true;

        /// <summary>
        ///     扩展
        /// </summary>
        public UnitExtend UnitExtend { get; set; }

        /// <summary>
        ///     两个地址是否一致
        /// </summary>
        /// <param name="other">另一个地址</param>
        /// <returns>是否一致</returns>
        public bool Equals(AddressUnit<TKey> other)
        {
            return Area.ToUpper() == other.Area.ToUpper() && Address == other.Address || Id.Equals(other.Id);
        }
    }

    /// <summary>
    ///     没有Id的设备属性
    /// </summary>
    public interface IMachinePropertyWithoutKey
    {
        /// <summary>
        ///     工程名
        /// </summary>
        string ProjectName { get; set; }

        /// <summary>
        ///     设备名
        /// </summary>
        string MachineName { get; set; }

        /// <summary>
        ///     标识设备的连接关键字
        /// </summary>
        string ConnectionToken { get; }

        /// <summary>
        ///     是否处于连接状态
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        ///     是否保持连接
        /// </summary>
        bool KeepConnect { get; set; }

        /// <summary>
        ///     设备的连接器
        /// </summary>
        IUtilityProperty BaseUtility { get; }

        /// <summary>
        ///     获取设备的方法集合
        /// </summary>
        /// <typeparam name="TMachineMethod">方法集合的类型</typeparam>
        /// <returns>设备的方法集合</returns>
        TMachineMethod GetMachineMethods<TMachineMethod>() where TMachineMethod : class, IMachineMethod;

        /// <summary>
        ///     连接设备
        /// </summary>
        /// <returns>是否连接成功</returns>
        bool Connect();

        /// <summary>
        ///     连接设备
        /// </summary>
        /// <returns>是否连接成功</returns>
        Task<bool> ConnectAsync();

        /// <summary>
        ///     断开设备
        /// </summary>
        /// <returns>是否断开成功</returns>
        bool Disconnect();

        /// <summary>
        ///     获取设备的Id的字符串
        /// </summary>
        /// <returns></returns>
        string GetMachineIdString();
    }

    /// <summary>
    ///     设备的抽象
    /// </summary>
    public interface IMachineProperty<TKey> : IMachinePropertyWithoutKey where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Id
        /// </summary>
        TKey Id { get; set; }
    }
}