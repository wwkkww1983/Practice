using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HikvisionDemo
{
    public class HttpHelper
    {
        public static async Task<HttpResponseMessage> Get(string url)
        {
            HttpResponseMessage responseMessage = null;
            if (url.HasValue())
            {
                using (HttpClient client = new HttpClient())
                {
                    responseMessage = await client.GetAsync(url);
                }
            }
            else
            {
                responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }
            return responseMessage;
        }

        public static async Task<HttpResponseMessage> Post(string url, PostParam param)
        {
            HttpResponseMessage responseMessage = null;
            if (url.HasValue())
            {
                using (HttpClient client = new HttpClient())
                {
                    responseMessage = await client.PostAsync(url, new StringContent(param.ToJson(), Encoding.UTF8, "application/json"));
                }
            }
            else
            {
                responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }
            return responseMessage;
        }
    }
}
