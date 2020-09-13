using System.IO;
using System.Net;
using System.Text;

namespace Caist.Framework.Util
{
    public class HttpHelper
    {

        #region 模拟GET
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="Url">The URL.</param>
        /// <param name="postDataStr">The post data string.</param>
        /// <returns>System.String.</returns>
        public static string HttpGet(string Url, int timeout = 10 * 1000)
        {
            Stream myResponseStream = null;
            StreamReader myStreamReader = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                request.Timeout = timeout;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                myResponseStream = response.GetResponseStream();
                myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();

                return retString;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myStreamReader != null)
                {
                    myStreamReader.Close();
                }
                if (myResponseStream != null)
                {
                    myResponseStream.Close();
                }
            }
        }
        #endregion
    }
}
