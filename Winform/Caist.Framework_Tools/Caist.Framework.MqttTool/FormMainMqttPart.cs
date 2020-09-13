using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Enum;
using Caist.Framework.M2MQTT;
using Caist.Framework.ThreadPool;
using Caist.Framework.Util;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Caist.Framework.Mqtt
{
    /// <summary>
    /// mqtt
    /// </summary>
    public partial class FrmMian
    {
        public static MqThemeEntity mqtOption = new MqThemeEntity();
        private M2MqttServer MqtCLient;
        public System.Threading.Timer _timer;
        public string mqtTimer = "MQTPushTimer".GetConfigrationStr();
        public DateTime? lastDateTime = null;
        bool IsFirstUpload = true;
        private void MqttStart_Click(object sender, EventArgs e)
        {
            this.MqttStart.Enabled = false;
            this.MqttStop.Enabled = true;
            Task.Run(async () =>
            {
                await StartMqtt();
            });
        }

        private void MqttStop_Click(object sender, EventArgs e)
        {
            this.MqttStart.Enabled = true;
            this.MqttStop.Enabled = false;
            StopMqtt(e);

        }


        #region  mqtt
        /// <summary>
        /// 获取MQT登录信息
        /// </summary>
        /// <returns></returns>
        private async Task LoadMqtt()
        {
            var MqtList = await MqThemeBLL.GetList();
            mqtOption = MqtList.Where(v => v.MqStutas == 1).FirstOrDefault();
            tsTbMqName.Text = mqtOption?.MqCollieryName;
            tsTbServer.Text = mqtOption?.MqHost;
            tsTbMqPort.Text = mqtOption?.MqPort.ToString();
            tsTbMqUserName.Text = mqtOption?.MqUser;
        }
        void Work(object state)
        {
            CancellationTokenSource ts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                await PushData(MqtCLient);
            });

        }
        /// <summary>
        /// 获取最后上传时间   如果上传成功修改数据库最后上传时间
        /// </summary>
        /// <param name="IsUpdate">是否更新数据库上传时间</param>
        /// <returns></returns>
        public DateTime getMqtSetting(bool IsUpdate = false)
        {
            if (!lastDateTime.HasValue) //首次启动程序获取更新时间戳
            {
                var MqtList = MqThemeBLL.GetList();
                mqtOption = MqtList.Result.Where(v => v.MqStutas == 1).FirstOrDefault();
                lastDateTime = mqtOption.LastUpdateTime.HasValue ? mqtOption.LastUpdateTime.Value : DateTime.Now.AddDays(-10);
            }
            if (IsUpdate && lastDateTime.HasValue) //是否更新上传时间
            {
                mqtOption.LastUpdateTime = lastDateTime.Value.AddSeconds(Convert.ToInt32(mqtTimer));  //记录最后更新时间，如果是历史数据就要将每间隔30秒的数据点为续传上去
                lastDateTime = mqtOption.LastUpdateTime; //同时更新当前记录时间
                if (mqtOption.LastUpdateTime > DateTime.Now)  //避免最后更新时间超过当前时间
                {
                    mqtOption.LastUpdateTime = DateTime.Now;
                }
                try
                {
                    MqThemeBLL.UpdateEntity(mqtOption);
                }
                catch (Exception ex)
                {

                    MqtCLient.MqtMessage("记录上传时间数据异常：" + ex.Message);
                }
            }
            return lastDateTime.Value;

        }

        /// <summary>
        /// 获取13位时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        private async Task StartMqtt()
        {
            try
            {
                //建立mqtt连接
                string BrokerAddress = tsTbServer.Text;
                MqtCLient = new M2MqttServer(BrokerAddress, Convert.ToInt32(tsTbMqPort.Text), mqtOption.MqClientid, mqtOption.MqUser, mqtOption.MqPassword, this.MqttStart, this.richMQT);

                byte code = await MqtCLient.Connect();
                if (MqtCLient.IsConnected)
                {
                    MqtCLient.MqtMessage("建立Mqtt连接成功:" + code);
                    ////创建重连监听
                    //M2Mqtt.ConnectWrap();
            
                    getMqtSetting();

                    //历史数据上传
                    while (DateTimeHelper.GetMinuteinterval(lastDateTime.Value, DateTime.Now) > 3 * 60)
                    {
                        mqtTimer = "60";  //历史数据默认1分钟间隔
                        Work(null);
                        Thread.Sleep(3000);
                    }
                    MqtCLient.MqtMessage("-----------------历史数据补充上传完成----------------------");
                    //恢复原有间隔时间差
                    mqtTimer = "MQTPushTimer".GetConfigrationStr();
                    Thread.Sleep(3000); //补录数据完成，睡眠等待3秒
                    //定时推送数据
                    _timer = new System.Threading.Timer(Work, "执行", TimeSpan.Zero, TimeSpan.FromSeconds(Convert.ToInt32(mqtTimer)));

                }
                else
                {
                    MqtCLient.MqtMessage("与服务器建立连接失败:" + code);
                    this.MqttStop_Click(null, null);
                }

            }
            catch (Exception ex)
            {

                MqtCLient.MqtMessage("连接失败:" + ex.Message);
                this.MqttStop_Click(null, null);
            }

        }
        /// <summary>
        /// 推送数据逻辑
        /// </summary>
        /// <param name="M2Mqtt"></param>
        /// <returns></returns>
        private async Task PushData(M2MqttServer M2Mqtt)
        {

            if (M2Mqtt.IsConnected)
            {
                var mkCodeList = Enum.GetNames(typeof(MkSystemCode));

                //查询出所有的数据，并且根据系统并行循环上传点表数据
                var settingCodeList = await MqttCodeSettingBLL.GetList();
                int PushCount = 0;
                //获取上一次上传时间
                DateTime lastUpTime = getMqtSetting();
                #region
                //并行推送上传数据  每次上传单个主题 =  单个系统数据
                //Parallel.For(0, mkCodeList.Length, (i) =>
                //{
                for (int i = 0; i < mkCodeList.Length; i++)
                {

                    int sysCode = GetEnumValue(typeof(MkSystemCode), mkCodeList.GetValue(i).ToString());
                    //筛选出当前系统的配置  单个系统上传到单个主题，所以一次只上传一个系统的数据
                    var SettingList = settingCodeList.Where(n => n.SystemCode == (sysCode < 10 ? "0" + sysCode.ToString() : sysCode.ToString()));
                    if (SettingList != null && SettingList.Count() > 0)
                    {
                        //发送数据   
                        //数据格式 {"timestamp[时间戳]":xxxx,"systemcode[系统编码]":"xxxx"，"values":[{"code[传感器编码]":" xxxx","v[实时值]":xxxx}]}
                        //如: {"timestamp":1523116807014,"systemcode":"03","values":[{"code":"5224230100600621101310301000","v":0}，{"code":"5224230100600621101310301001","v":0}{....}
                        long timestamp = ConvertDateTimeToInt(lastUpTime);
                        M2MqtPushEntity<float> PushData = new M2MqtPushEntity<float>();
                        List<PushValues<float>> PushList = new List<PushValues<float>>();

                        #region  根据配置信息中的表名查询出数据
                        var pointValueList = MqttCodeSettingBLL.GetUpLoadDataList(SettingList.FirstOrDefault().TabName, lastUpTime);
                        #endregion
                        //循环组装单个系统需要上传的数据
                        foreach (var setting in SettingList)
                        {
                            //传感器编码
                            //煤矿编码（15）+地址编码（6）+系统编码（2）+设备顺序编码（2）+传感器类型编码（3）= 28位传感器编码
                            //采区编码  +地址类型编码	+安装设备地点编码 =地址编码
                            var SensorCode = mqtOption.MqCollieryCode + setting.AddressAreCode + setting.AddressTypeCode + setting.AddressDeviceCode + setting.SystemCode +
                            setting.DeviceCode + setting.SensorTypeCode;
                            var pointValue = pointValueList.Result.FindLast(n => n.DictId == setting.CodePointSetting);
                            #region  根据数据类型处理上传业务逻辑
                            if (setting.CodeType == 2) //报警数据
                            {
                                bool AlarmStatus = false;
                                var AlarmPointS = setting.AlarmPoint.Split(',');
                                foreach (var alarm in AlarmPointS) //报警为多个数据中有任意一个状态为true则上传
                                {
                                    pointValue = pointValueList.Result.FindLast(n => n.DictId == alarm);
                                    if (pointValue != null)
                                    {
                                        AlarmStatus = (pointValue != null && pointValue.DictValue.ParseToInt() == 0);
                                        if (AlarmStatus)
                                            break;
                                    }

                                }
                            }
                            else if (setting.CodeType == 3) // 运行总控 ： 通讯状态、运行状态 等1个或者多个的数据选择处理
                            {
                                bool ctrStatus = false;
                                var ctrs = setting.AlarmPoint.Split(',');
                                foreach (var ctr in ctrs)
                                {
                                    pointValue = pointValueList.Result.FindLast(n => n.DictId == ctr);
                                    ctrStatus = (pointValue != null && pointValue.DictValue.ParseToInt() == 0);
                                    if (ctrStatus)
                                        break;
                                }
                            }
                            #region 空值处理
                            if (pointValue == null)
                            {
                                continue;
                            }
                            if (pointValue.DictValue == null)
                            {
                                continue;
                            }
                            #endregion
                            #endregion

                            if (pointValue != null && pointValue.DictValue != null)
                            {
                                float upValue;
                                #region 数据格式转换
                                if (setting.DecimalPlaces == 0) //整数  转换为int
                                {
                                    //没有小数位数的  是true 和 false 先转换为int  0,1  在转换成为没有小数位数的数值
                                    upValue = pointValue.DictValue.ParseToInt().ParseToFloat().ToString("f" + setting.DecimalPlaces.ToString() + "").ParseToFloat();

                                }
                                else  //带小数
                                {
                                    //没有小数位数的默认位0位
                                    //ChangeDataToD
                                    upValue = pointValue.DictValue.ChangeDataToD().ParseToFloat().ToString("f" + setting.DecimalPlaces.ToString() + "").ParseToFloat();
                                }
                                if (upValue >= 0) //先转换 如果成功了表示数据是对
                                {
                                    //判断总长度是否满足长度要求 总长度：除去小数点的数字位数
                                    if (upValue.ToString().Length <= setting.ValueLength + 1)
                                    {
                                        PushList.Add(new PushValues<float>(SensorCode, upValue));
                                    }
                                }

                                #endregion
                            }
                        }
                        PushData.timestamp = timestamp;
                        PushData.systemcode = (sysCode < 10 ? "0" + sysCode.ToString() : sysCode.ToString());
                        PushData.values = PushList;
                        //新版本中没有提到需要加密，暂时注释
                        //string PushMsg = M2Mqtt.AESEncrypt(PushData.ToJson(), M2Mqtt.ModifyStr(mqtOption.MqEncryption));
                        if (PushData != null && PushData.values != null && PushData.values.Count() > 0)
                        {
                            // 主题内容  使用数字+“/”组成，不可使用其它字符。  主题：/煤矿编码/系统编码。
                            string Topic = "/" + mqtOption.MqCollieryCode + "/" + (sysCode < 10 ? "0" + sysCode.ToString() : sysCode.ToString());
                            // 订阅主题
                            M2Mqtt.Subscribe(new string[] { Topic }, new byte[] { 2 });
                            IsFirstUpload = false;
                            string pushdata = PushData.ToNoDecimalJson();
                            ushort result = M2Mqtt.Publish(Topic, pushdata);
                            M2Mqtt.MqtMessage($"向主题：{Topic} 请求发送：{pushdata},线程id：{result}");
                            PushCount++; //记录有效推送次数
                        }
                        else
                        {
                            M2Mqtt.MqtMessage($"{((MkSystemCode)Convert.ToInt32(sysCode)).GetDescription()}： 没有新的数据");
                        }

                    }

                }
                //});
                #endregion
                #region 同步推送
                //foreach (int myCode in Enum.GetValues(typeof(MkSystemCode)))
                //{
                //    string strName = Enum.GetName(typeof(MkSystemCode), myCode);//获取名称
                //    int mkCode = myCode;
                //    // 主题内容  使用数字+“/”组成，不可使用其它字符。  主题：/集团编码/矿井编码/系统编码。
                //    string Topic = "/" + mqtOption.MqCode + "/" + mqtOption.MqCollieryCode + "/" + (mkCode < 10 ? "0" + mkCode.ToString() : mkCode.ToString());
                //    // 订阅主题
                //    M2Mqtt.Subscribe(new string[] { Topic }, new byte[] { 2 });

                //    //发送数据   
                //    //数据格式 {"timestamp[时间戳]":xxx,"values":[{ "code[点编码]":xxx,"v[实时值]": xxx}]}
                //    //如: {"timestamp":1523116807014,"values":[{"code":030101001,"v":true}，{"code": 030101300,"v":2}，{……}]}
                //    long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                //    M2MqtPushEntity<long> PushData = new M2MqtPushEntity<long>();
                //    List<PushValues<long>> PushList = new List<PushValues<long>>();
                //    PushList.Add(new PushValues<long>(030101001, 0));
                //    PushData.timestamp = timestamp;

                //    string PushMsg = M2Mqtt.AESEncrypt(PushData.ToJson(), M2Mqtt.ModifyStr(mqtOption.MqEncryption));

                //    await M2Mqtt.Publish(Topic, PushMsg);

                //    M2Mqtt.MqtMessage($"向主题：{Topic} 请求发送：{PushMsg}");
                //}
                #endregion

                if (PushCount > 0)
                {
                    getMqtSetting(true);
                }
                else
                {
                    if (PushCount==0 && !IsFirstUpload)  //之前有上传成功记录，表示本次上传没有获取到最新数据，跳过执行下一个时间段的上传
                    {
                        getMqtSetting(true);
                    }
                }
            }

        }



        public static int GetEnumValue(Type enumType, string enumName)
        {
            try
            {
                if (!enumType.IsEnum)
                    throw new ArgumentException("enumType必须是枚举类型");
                var values = Enum.GetValues(enumType);
                var ht = new Hashtable();
                foreach (var val in values)
                {
                    ht.Add(Enum.GetName(enumType, val), val);
                }
                return (int)ht[enumName];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void StopMqtt(EventArgs e)
        {
            if (_timer != null)
                //释放定时器
                _timer.Dispose();

            //释放资源，避免重连
            MqtCLient.Dispose();
            if (MqtCLient.IsConnected)
            {
                //释放mqt连接资源
                MqtCLient.Disconnect();
            }


            //base.OnClosed(e);
        }


        private void richMQT_TextChanged(object sender, EventArgs e)
        {
            LimitLine(200, this.richMQT);
            this.richMQT.SelectionStart = this.richMQT.Text.Length;
            this.richMQT.SelectionLength = 0;
        }

        #endregion
    }
}
