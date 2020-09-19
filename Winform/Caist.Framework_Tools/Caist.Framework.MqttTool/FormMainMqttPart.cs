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
        public System.Threading.Timer _hisTimer;
        //是否有历史数据
        bool IsHistory = false;
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
        private async Task LoadMqtt(object sender, EventArgs e)
        {
            try
            {
                var MqtList = await MqThemeBLL.GetList();
                mqtOption = MqtList.Where(v => v.MqStutas == 1).FirstOrDefault();
                tsTbMqName.Text = mqtOption?.MqCollieryName;
                tsTbServer.Text = mqtOption?.MqHost;
                tsTbMqPort.Text = mqtOption?.MqPort.ToString();
                tsTbMqUserName.Text = mqtOption?.MqUser;
#if !DEBUG
                if (!string.IsNullOrEmpty(tsTbMqName.Text))
                    MqttStart_Click(sender, e);
#endif
            }
            catch (Exception ex)
            {
                Message("-----------------初始化数据异常----------------------");
                Message(ex.Message);

                Thread t = new Thread(o => Thread.Sleep(60000));
                t.Start(this);
                while (t.IsAlive)
                {
                    //防止UI假死
                    Application.DoEvents();
                }
                await LoadMqtt(sender, e);            
            }

        }
        void Work(object state)
        {
            Task.Run(async () =>
            {
                await PushData(MqtCLient);
            });

        }

        void HistoryWork(object state)
        {
            if (DateTimeHelper.GetMinuteinterval(lastDateTime.Value, DateTime.Now) > 0.5 * 30)
            {
                IsHistory = true;
                Task.Run(async () =>
                {
                    await PushData(MqtCLient);
                });
            }
            else
            {
                IsHistory = false;
                _hisTimer.Change(Timeout.Infinite, Timeout.Infinite);
                //马上暂停
                MqtCLient.MqtMessage("-----------------历史数据补充上传完成----------------------");
                Thread t = new Thread(o => Thread.Sleep(2000));//补录完成暂停2秒
                t.Start(this);
                while (t.IsAlive)
                {
                    //防止UI假死
                    Application.DoEvents();
                }
                MqtCLient.MqtMessage("-------------------切换实时数据上传-----------------------");
                if (_timer!=null)
                {
                    //立即开启 定时推送数据 
                    _timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(Convert.ToInt32(mqtTimer)));
                    //传输定时器，监测使用历史数据定时器还是实时数据定时器
                    MqtCLient.SetTimer(_timer, _hisTimer);
                }
                else
                {
                    //立即开启 定时推送数据 
                    _timer = new System.Threading.Timer(Work, "执行", TimeSpan.Zero, TimeSpan.FromSeconds(Convert.ToInt32(mqtTimer)));
                    //传输定时器，监测使用历史数据定时器还是实时数据定时器
                    MqtCLient.SetTimer(_timer, _hisTimer);
                }
              

            }


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
                    lastDateTime = mqtOption.LastUpdateTime = DateTime.Now;
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
                    history();

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
        //历史数据上传
        private void history()
        {
            _hisTimer = new System.Threading.Timer(HistoryWork, "执行", TimeSpan.Zero, TimeSpan.FromSeconds(2));
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
                Parallel.For(0, mkCodeList.Length, (i) =>
                {
                    //for (int i = 0; i < mkCodeList.Length; i++)
                    //{

                    int sysCode = GetEnumValue(typeof(MkSystemCode), mkCodeList.GetValue(i).ToString());
                    //筛选出当前系统的配置  单个系统上传到单个主题，所以一次只上传一个系统的数据
                    var SettingList = settingCodeList.Where(n => n.SystemCode == (sysCode < 10 ? "0" + sysCode.ToString() : sysCode.ToString()));
                    if (SettingList != null && SettingList.Count() > 0)
                    {
                        //发送数据   
                        //数据格式 {"timestamp[时间戳]":xxxx,"systemcode[系统编码]":"xxxx"，"values":[{"code[传感器编码]":" xxxx","v[实时值]":xxxx}]}
                        //如: {"timestamp":1523116807014,"systemcode":"03","values":[{"code":"5224230100600621101310301000","v":0}，{"code":"5224230100600621101310301001","v":0}{....}
                        long timestamp = 0;
                        if (!IsHistory)//不再是历史数据
                        {
                            timestamp = ConvertDateTimeToInt(DateTime.Now);
                        }
                        else
                        {
                            timestamp = ConvertDateTimeToInt(lastUpTime);
                        }

                        M2MqtPushEntity<decimal> PushData = new M2MqtPushEntity<decimal>();
                        List<PushValues<decimal>> PushList = new List<PushValues<decimal>>();

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
                                        #region  bool值类型转换  0是 true  1是 false  数据管理平台规定  0就是开启，没有报警，在运行。 1：关闭，有报警，停止运行
                                        PointValueConvert(pointValue);
                                        #endregion

                                        AlarmStatus = (pointValue != null && pointValue.DictValue.ParseToInt() == 0);
                                        if (AlarmStatus)
                                            break;
                                    }

                                }
                                //默认值 状态默认为false
                                if (pointValue == null)
                                {
                                    pointValue = new MqtPlcDataEntity();
                                    pointValue.DictValue = "1";
                                }
                            }
                            else if (setting.CodeType == 3) // 运行总控 ： 通讯状态、运行状态 等1个或者多个的数据选择处理
                            {
                                bool ctrStatus = false;
                                var ctrs = setting.AlarmPoint.Split(',');
                                foreach (var ctr in ctrs)
                                {
                                    pointValue = pointValueList.Result.FindLast(n => n.DictId == ctr);
                                    #region  bool值类型转换  0是 true  1是 false  数据管理平台规定  0就是开启，没有报警，在运行。 1：关闭，有报警，停止运行
                                    PointValueConvert(pointValue);

                                    #endregion
                                    ctrStatus = (pointValue != null && pointValue.DictValue.ParseToInt() == 0);
                                    if (ctrStatus)
                                        break;
                                }

                                //默认值 状态默认为false
                                if (pointValue == null)
                                {
                                    pointValue = new MqtPlcDataEntity();
                                    pointValue.DictValue = "1";
                                }

                            }
                            #endregion

                            if (pointValue == null)
                            {
                                pointValue = new MqtPlcDataEntity();
                                pointValue.DictValue = "0";
                            }
                            if (pointValue != null && pointValue.DictValue != null)
                            {
                                decimal upValue;
                                #region 数据格式转换
                                if (setting.DecimalPlaces == 0) //整数  转换为int
                                {
                                    #region  bool值类型转换  0是 true  1是 false  数据管理平台规定  0就是开启，没有报警，在运行。 1：关闭，有报警，停止运行
                                    PointValueConvert(pointValue);
                                    #endregion

                                    //没有小数位数的  是true 和 false 先转换为int  0,1  在转换成为没有小数位数的数值
                                    upValue = Convert.ToDecimal(pointValue.DictValue.ParseToInt().ParseToFloat().ToString("f" + setting.DecimalPlaces.ToString() + ""));

                                }
                                else  //带小数
                                {
                                    //没有小数位数的默认位0位
                                    //ChangeDataToD
                                    upValue = Convert.ToDecimal(pointValue.DictValue.ChangeDataToD().ParseToFloat().ToString("f" + setting.DecimalPlaces.ToString() + ""));
                                }
                                if (upValue >= 0) //先转换 如果成功了表示数据是对
                                {
                                    //判断总长度是否满足长度要求 总长度：除去小数点的数字位数
                                    if (upValue.ToString().Length <= setting.ValueLength + 1)
                                    {
                                        PushList.Add(new PushValues<decimal>(SensorCode, upValue));
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
                            M2Mqtt.MqtMessage($"向主题：{Topic} 请求发送{PushData.values.Count}个点位数据,线程id：{result}");
                            PushCount++; //记录有效推送次数
                        }
                        else
                        {
                            M2Mqtt.MqtMessage($"{((MkSystemCode)Convert.ToInt32(sysCode)).GetDescription()}： 没有新的数据");
                        }

                    }

                    //}
                });
                #endregion


                if (PushCount > 0)
                {
                    getMqtSetting(true);
                }
                else
                {
                    if ((PushCount == 0 && !IsFirstUpload) || IsHistory)  //之前有上传成功记录，表示本次上传没有获取到最新数据，跳过执行下一个时间段的上传
                    {
                        getMqtSetting(true);
                    }
                }
            }

        }

        /// <summary>
        /// 状态点位数值转换
        /// </summary>
        /// <param name="pointValue"></param>
        private static void PointValueConvert(MqtPlcDataEntity pointValue)
        {
            if (pointValue != null)
            {
                if (pointValue.DictValue.ToLower() == "true")
                {
                    pointValue.DictValue = "0";
                }
                else if (pointValue.DictValue.ToLower() == "false")
                {
                    pointValue.DictValue = "1";
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
            if (MqtCLient != null)
            {
                if (MqtCLient.IsConnected)
                    //释放mqt连接资源
                    MqtCLient.Disconnect();
                MqtCLient.Dispose();
            }

            //base.OnClosed(e);
        }


        private void Message(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                richMQT.Invoke(new Action(() =>
                {
                    string value = string.Format(">时间：{0}  内容：{1}\r", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), str);
                    richMQT.SelectionFont = new Font("宋体", 10, FontStyle.Regular);  //设置SelectionFont属性实现控件中的文本为楷体，大小为12，字样是粗体
                    richMQT.SelectionColor = System.Drawing.Color.Black;    //设置SelectionColor属性实现控件中的文本颜色为红色
                    richMQT.AppendText(value);
                }));
            }
        }
        #endregion
    }
}
