using Newtonsoft.Json;
using System;

namespace Caist.Framework.Mqtt
{
    [Serializable]
    public class ClientStatusModel
    {
        [JsonIgnore]
        public bool IsOnline { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// 最后连接时间
        /// </summary>
        [JsonProperty("lastConnectedAt")]
        public string LastConnectedAt { get; set; }

        [JsonProperty("online")]
        public int Online
        {
            get
            {
                return IsOnline ? 1 : 0;
            }
        }

        public static ClientStatusModel Create(string clientId, bool isOnline)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentNullException("clientId");
            }

            return new ClientStatusModel
            {
                ClientId = clientId,
                IsOnline = isOnline,
                LastConnectedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
    }
}
