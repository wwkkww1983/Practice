using Polly;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Caist.Framework.M2MQTT
{
    public class M2MqttServer
    {

        /// <summary>
        /// 表示是否已经释放
        /// </summary>
        private bool IsDisposed;

        private MqttClient client;
        private string ClientId;
        private string UserName;
        private string PassWord;
        private string Host;
        private int Port;
        private ToolStripButton MqttStart;
        private RichTextBox richMQT;
        public bool IsConnected
        {
            get
            {

                if (this.client != null)
                    return this.client.IsConnected;
                return false;

            }
        }



        public M2MqttServer(string Host, int Port, string ClientId, string UserName, string PassWord, ToolStripButton MqttStart, RichTextBox richMQT)
        {
            this.Host = Host;
            this.Port = Port;
            this.ClientId = ClientId;
            this.UserName = UserName;
            this.PassWord = PassWord;
            this.PassWord = PassWord;
            this.MqttStart = MqttStart; //控件
            this.richMQT = richMQT;
            Initializer();


        }
        /// <summary>
        /// 初始化链接构造器
        /// </summary>
        private void Initializer()
        {
            //初始化连接对象
            client = new MqttClient(this.Host, this.Port, false, null, null, MqttSslProtocols.None);
            //注册客户端事件回调
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.MqttMsgSubscribed += client_MqttMsgSubscribed;
            client.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;
            client.MqttMsgPublished += client_MqttMsgPublished;
            client.ConnectionClosed += client_ConnectionClosed;

        }


        public async Task<byte> Connect()
        {
            return await CreateConnect();
        }
        /// <summary>
        /// 建立通讯连接
        /// </summary>
        /// <returns></returns>
        private Task<byte> CreateConnect()
        {
            try
            {
                byte code = client.Connect(this.ClientId, this.UserName, this.PassWord);
                return Task.FromResult(code);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <param name="topics"></param>
        /// <param name="qosLevels"></param>
        public void Subscribe(string[] topics, byte[] qosLevels)
        {
            client?.Subscribe(topics, qosLevels);
        }

        public ushort Publish(string Topic, string Content)
        {
            byte[] msg = Encoding.UTF8.GetBytes(Content);

            return client.Publish(Topic, msg);
        }

        //public async Task Publish(string Topic, string Content)
        //{
        //    byte[] msg = Encoding.UTF8.GetBytes(Content);

        //    await PublishAsync(Topic, msg);
        //}
        private Task PublishAsync(string Topic, byte[] msg)
        {
            return Task.Factory.StartNew(() =>
            {
                client.Publish(Topic, msg);
            });
        }
        public void Disconnect()
        {

            this.client?.Disconnect();

        }

        /// <summary>
        /// 连接包装
        /// </summary>
        /// <returns></returns>
        public byte ConnectWrap()
        {
            if (IsDisposed)
            {
                return 0;
            }

            var result = Policy
                .HandleResult<byte>(x => this.client.IsConnected == false && x != 0)
                .WaitAndRetryForever(x => TimeSpan.FromSeconds(5))
                .Execute(() =>
                {
                    try
                    {

                        MqtMessage("### 正在重连 ###");
                        byte tmp = this.client.Connect(
                            this.ClientId,
                            this.UserName,
                            this.PassWord);
                        //tmp = FrmMian.client.Connect(
                        //    FrmMian.mqtOption.MqClientid,
                        //   "admin",
                        //    "public");
                        MqtMessage($"Connection Result Code:{tmp}{Environment.NewLine}IsConnected:{client.IsConnected}");

                        return 0;
                    }
                    catch (Exception ex)
                    {
                        MqtMessage($"重连失败:{ex.Message}");
                        return 255;
                        throw (ex);
                    }
                });

            return result;
        }

        /// <summary>
        /// 释放重连资源
        /// </summary>
        public void Dispose()
        {
            MqtMessage("停止任务连接资源");

            IsDisposed = true;

        }
        /// <summary>
        /// 被动关闭资源，进入重连机制
        /// </summary>
        public void DisposeClose()
        {
            MqtMessage("连接资源被动断开，准备重连");
            IsDisposed = false;
        }
        public void MqtMessage(string str, string client = "")
        {
            if (!string.IsNullOrEmpty(client) && !string.IsNullOrEmpty(str))
            {
                richMQT.Invoke(new Action(() =>
                {
                    string value = string.Format("-{0} - 时间：{1}  内容：{2}\r", client, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), str);
                    richMQT.SelectionFont = new Font("宋体", 10, FontStyle.Regular);  //设置SelectionFont属性实现控件中的文本为楷体，大小为12，字样是粗体
                    richMQT.SelectionColor = System.Drawing.Color.Red;    //设置SelectionColor属性实现控件中的文本颜色为红色
                    richMQT.AppendText(value);
                }));
            }
            else
            {
                if (!string.IsNullOrEmpty(str) && string.IsNullOrEmpty(client))
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
        }


        //  客户端收到来自服务端的消息后触发
        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            MqtMessage($"上传主题：{e.Topic} 数据:{ReceivedMessage}成功，收到服务器回调");
        }



        // sub后的操作
        public void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            MqtMessage("Subscribed for id = " + e.MessageId.ToString());
        }
        // 发布消息后的操作
        public void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            MqtMessage("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
        }
        // 关闭连接后的操作
        public void client_ConnectionClosed(object sender, EventArgs e)
        {
            if (!MqttStart.Enabled)
            {
                DisposeClose();
            }
            MqtMessage("connect closed");
            Task.Factory.StartNew(() =>
            {
                ConnectWrap();
            });
        }
        // 取消sub后的操作
        public void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            MqtMessage("subscribed closed");
        }

        public static string ModifyStr(string str)
        {
            StringBuilder tempStr = new StringBuilder();
            if (str.Length > 8)
            {
                tempStr.Append(str.Substring(0, 8));
            }
            else
            {
                tempStr.Append(str);
                for (int i = 0; i < 8 - str.Length; i++)
                {
                    tempStr.Append("0");
                }
            }
            return tempStr.ToString();
        }

        /// <summary>  
        /// AES encrypt ( no IV)  
        /// </summary>  
        /// <param name="data">Raw data</param>  
        /// <param name="key">Key, requires 32 bits</param>  
        /// <returns>Encrypted string</returns>  
        public static string AESEncrypt(string data, string key)
        {

            using (MemoryStream Memory = new MemoryStream())
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(data);
                    byte[] bKey = new byte[32];
                    Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);

                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.KeySize = 256;
                    aes.Key = bKey;

                    using (CryptoStream cryptoStream = new CryptoStream(Memory, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        try
                        {
                            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            return ByteToHexStr(Memory.ToArray());
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                    }
                }
            }
        }

        /// <summary>  
        /// AES decrypt( no IV)  
        /// </summary>  
        /// <param name="data">Encrypted data</param>  
        /// <param name="key">Key, requires 32 bits</param>  
        /// <returns>Decrypted string</returns>  
        public static string AESDecrypt(string data, string key)
        {

            byte[] encryptedBytes = Convert.FromBase64String(data);
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);

            using (MemoryStream Memory = new MemoryStream(encryptedBytes))
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.KeySize = 256;
                    aes.Key = bKey;

                    using (CryptoStream cryptoStream = new CryptoStream(Memory, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        try
                        {
                            byte[] tmp = new byte[encryptedBytes.Length];
                            int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length);
                            byte[] ret = new byte[len];
                            Array.Copy(tmp, 0, ret, 0, len);

                            return Encoding.UTF8.GetString(ret, 0, len);
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexStr(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }
    }


    public class M2MqtPushEntity<T>
    {
        public long timestamp { get; set; }
        public string systemcode { get; set; }
        public List<PushValues<T>> values { get; set; }
    }
    public class PushValues<T>
    {
        public PushValues(string code, T v)
        {
            this.code = code;
            this.v = v;
        }

        public string code { get; set; }
        public T v { get; set; }
    }
}
