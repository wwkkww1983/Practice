using Caist.Framework.Mqtt;
using Caist.Framework.Mqtt.Extensions;
using Caist.Framework.Mqtt.Message;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Test.MQTT
{
    class Mqtt
    {
        //static void Main(string[] args)
        //{
        //    System.Console.WriteLine("Hello World!");

        //    var host = "127.0.0.1";
        //    int port = 1883;

        //    var clientId = "CC";

        //    IServiceCollection services = new ServiceCollection();

        //    var topicSubscribers = new List<TopicSubscriber>();
        //    //topicSubscribers.Add(new TopicSubscriber("rdc/#", MqttQosLevel.ExactlyOnce));
        //    //topicSubscribers.Add(new TopicSubscriber("rdc/#", MqttQosLevel.ExactlyOnce, true));

        //    services.AddMqttClient(new MqttClientOptions
        //        {
        //            ClientId = clientId,
        //            AutoReconnectedDelay = TimeSpan.FromSeconds(5),
        //            Host = host,
        //            Port = port,
        //            Subscribers = topicSubscribers.ToArray(),
        //            CleanSession = false
        //        })
        //        //.AddMqttHandler<TestHandler>()
        //        ;

        //    IServiceProvider provider = services.BuildServiceProvider();

        //    var mqttClient = provider.GetService<IMysoftMqttClient>();

        //    mqttClient.MessageReceived += MqttClient_MessageReceived;

        //    //Console.ReadKey();

        //    while (true)
        //    {
        //    Start:
        //        System.Console.WriteLine("0.退出;1.继续");

        //        var action = System.Console.ReadLine();

        //        if (action.Equals("0"))
        //        {
        //            mqttClient.Dispose();
        //            break;
        //        }
        //        else if (action != "1")
        //        {
        //            goto Start;
        //        }

        //        System.Console.WriteLine("topic:");

        //        var topic = System.Console.ReadLine();

        //        System.Console.WriteLine("payload:");

        //        var payload = System.Console.ReadLine();

        //        Task.Factory.StartNew(async () =>
        //        {
        //            await mqttClient.PublishAsync(
        //                new MqttMessageBuilder()
        //                    .WithTopic(topic)
        //                    .WithPayload(payload)
        //                    .WithExactlyOnceQos());
        //        });

        //        System.Console.ReadKey();
        //    }

        //    mqttClient.Dispose();

        //    System.Console.ReadKey();
        //}

        private static void MqttClient_MessageReceived(object sender, MqttMessage e)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("------------------------------------------------------");
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} Mqttnet Application Message Received");
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} Topic:{e.Topic}");
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} Payload:{e.ConvertPayloadToString()}");
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} Retain:{e.Retain}");
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} QualityOfServiceLevel:{e.Qos.ToString()}");
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append("------------------------------------------------------");
            strBuilder.Append(Environment.NewLine);

            System.Console.WriteLine(strBuilder);
        }
    }
}
