using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Enum;
using Caist.Framework.ThreadPool;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Caist.Framework.Service
{
    /// <summary>
    /// mqtt
    /// </summary>
    public partial class FrmMian
    {
        public static MqThemeEntity mqtOption = new MqThemeEntity();
        private M2Mqtt MqtCLient;
        public Timer _timer;
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
            tsTbMqName.Text = mqtOption?.MqCtl;
            tsTbServer.Text = mqtOption?.MqHost;
            tsTbMqPort.Text = mqtOption?.MqPort.ToString();
            tsTbMqUserName.Text = mqtOption?.MqUser;

        }
        void Work(object state)
        {
            Task.Run(() =>
            {
                PushData(MqtCLient);
            });

        }
        private async Task StartMqtt()
        {

            try
            {
                //建立mqtt连接

                string BrokerAddress = tsTbServer.Text;
                MqtCLient = new M2Mqtt(BrokerAddress, Convert.ToInt32(tsTbMqPort.Text), mqtOption.MqClientid, mqtOption.MqUser, mqtOption.MqPassword);

                byte code = await MqtCLient.Connect();
                if (MqtCLient.IsConnected)
                {
                    ////创建重连监听
                    //M2Mqtt.ConnectWrap();
                    //定时推送数据
                    var mqtTimer = "MQTPushTimer".GetConfigrationStr();
                    _timer = new Timer(Work, null, TimeSpan.Zero, TimeSpan.FromSeconds(Convert.ToInt32(mqtTimer)));

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
        private async Task PushData(M2Mqtt M2Mqtt)
        {

            if (M2Mqtt.IsConnected)
            {
                var mkCodeList = Enum.GetNames(typeof(MkSystemCode));

                #region
                //并行推送上传数据
                Parallel.For(0, mkCodeList.Length, (i) =>
                {


                    int mkCode = GetEnumValue(typeof(MkSystemCode), mkCodeList.GetValue(i).ToString());
                    // 主题内容  使用数字+“/”组成，不可使用其它字符。  主题：/集团编码/矿井编码/系统编码。
                    string Topic = "/" + mqtOption.MqCode + "/" + mqtOption.MqCollieryCode + "/" + (mkCode < 10 ? "0" + mkCode.ToString() : mkCode.ToString());
                    // 订阅主题
                    M2Mqtt.Subscribe(new string[] { Topic }, new byte[] { 2 });

                    //发送数据   
                    //数据格式 {"timestamp[时间戳]":xxx,"values":[{ "code[点编码]":xxx,"v[实时值]": xxx}]}
                    //如: {"timestamp":1523116807014,"values":[{"code":030101001,"v":true}，{"code": 030101300,"v":2}，{……}]}
                    long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                    M2MqtPushEntity<long> PushData = new M2MqtPushEntity<long>();
                    List<PushValues<long>> PushList = new List<PushValues<long>>();
                    PushList.Add(new PushValues<long>(030101001, 0));
                    PushData.timestamp = timestamp;

                    string PushMsg = M2Mqtt.AESEncrypt(PushData.ToJson(), M2Mqtt.ModifyStr(mqtOption.MqEncryption));

                    ushort result =  M2Mqtt.Publish(Topic, PushMsg);

                    M2Mqtt.MqtMessage($"向主题：{Topic} 请求发送：{PushMsg},线程id：{result}");

                });
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
