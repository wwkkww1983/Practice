using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Mqtt;
using Caist.Framework.Mqtt.Extensions;
using Caist.Framework.Util;
using Caist.Framework.Util.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Caist.Framework.WebService.Services
{
    public class MQTTTaskService : IHostedService, IDisposable
    {
        private MqThemeBLL mqThemeBLL = new MqThemeBLL();
        private Timer _timer;

        /// <summary>
        /// 定时任务启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Work, cancellationToken, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        /// <summary>
        /// 定时任务结束
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 执行定时任务
        /// </summary>
        /// <param name="state"></param>
        void Work(object state)
        {
            IServiceCollection services = new ServiceCollection();
            var topicSubscribers = new List<TopicSubscriber>();
            var token = (CancellationToken)state;
            if (token.IsCancellationRequested) return;
            MqThemeListParam param = new MqThemeListParam();
            var val = GetMQTTInit(param).Result.Result.Where(v => v.MqStutas == 1).FirstOrDefault();
            services.AddMqttClient(new MqttClientOptions
            {
                ClientId = val.MqClientid,
                UserName = val.MqUser,
                Password = EncrypHelper.Encrypt(val.MqPassword,val.MqEncryption),
                AutoReconnectedDelay = TimeSpan.FromSeconds(5),
                Host = val.MqHost,
                Port = int.Parse(val.MqPort.ToString()),
                Subscribers = topicSubscribers.ToArray(),
                CleanSession = false
            });
            IServiceProvider provider = services.BuildServiceProvider();
            var mqttClient = provider.GetService<IMysoftMqttClient>();
            if (mqttClient.IsConnected)
            {
                mqttClient.MessageReceived += MqttClient_MessageReceived;
            }
            
        }

        /// <summary>
        /// 发送煤矿人员消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_MessageReceived(object sender, Mqtt.Message.MqttMessage e)
        {
            //发送煤矿人员消息
        }

        /// <summary>
        /// 获取MQTT链接参数
        /// </summary>
        /// <returns></returns>
        public async Task<TData<List<MqThemeEntity>>> GetMQTTInit(MqThemeListParam param)
        {
            TData<List<MqThemeEntity>> obj = await mqThemeBLL.GetList(param);
            return obj;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
