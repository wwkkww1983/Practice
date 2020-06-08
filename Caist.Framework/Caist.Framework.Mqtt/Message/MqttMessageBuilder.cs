using Caist.Framework.Mqtt.Extensions;
using Caist.Framework.Mqtt.Topic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Caist.Framework.Mqtt.Message
{
    public class MqttMessageBuilder
    {
        private string _topic;
        private bool _retain = false;
        private byte[] _payload;
        private MqttQosLevel _qos = MqttQosLevel.AtMostOnce;

        public MqttMessageBuilder WithTopic(string topic)
        {
            _topic = topic;
            return this;
        }

        public MqttMessageBuilder WithTopic(params IMqttTopic[] topics)
        {
            _topic = topics.Build();
            return this;
        }

        public MqttMessageBuilder WithPayload(IEnumerable<byte> payload)
        {
            if (payload == null)
            {
                _payload = null;
                return this;
            }

            _payload = payload.ToArray();
            return this;
        }

        public MqttMessageBuilder WithPayload(Stream payload)
        {
            return WithPayload(payload, payload.Length - payload.Position);
        }

        public MqttMessageBuilder WithPayload(Stream payload, long length)
        {
            if (payload == null)
            {
                _payload = null;
                return this;
            }

            if (payload.Length == 0)
            {
                _payload = new byte[0];
            }
            else
            {
                _payload = new byte[length];
                var readCount = payload.Read(_payload, 0, _payload.Length);
            }

            return this;
        }

        public MqttMessageBuilder WithPayload(string payload)
        {
            if (payload == null)
            {
                _payload = null;
                return this;
            }

            _payload = payload.Equals("") ? new byte[0] : Encoding.UTF8.GetBytes(payload);
            return this;
        }

        public MqttMessageBuilder WithRetainFlag(bool value = true)
        {
            _retain = value;
            return this;
        }

        public MqttMessageBuilder WithQos(MqttQosLevel qos)
        {
            _qos = qos;
            return this;
        }

        public MqttMessageBuilder WithAtMostOnceQos()
        {
            WithQos(MqttQosLevel.AtMostOnce);
            return this;
        }

        public MqttMessageBuilder WithAtLeastOnceQos()
        {
            WithQos(MqttQosLevel.AtLeastOnce);
            return this;
        }

        public MqttMessageBuilder WithExactlyOnceQos()
        {
            WithQos(MqttQosLevel.ExactlyOnce);
            return this;
        }

        public MqttMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_topic))
            {
                throw new ArgumentNullException("未设置topic");
            }

            return new MqttMessage
            {
                Topic = _topic,
                Payload = _payload,
                Qos = _qos,
                Retain = _retain
            };
        }
    }
}
