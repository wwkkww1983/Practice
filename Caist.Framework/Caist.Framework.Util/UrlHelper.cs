using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Util
{
    public static class UrlHelper
    {
        /// <summary>
        /// 替换URL中域名地址
        /// </summary>
        /// <param name="domain">替换的域名地址</param>
        /// <param name="url">原始url</param>
        /// <returns></returns>
        public static String ReplaceDomain(String domain, String url)
        {
            String url_bak = "";
            if (url.IndexOf("//") != -1)
            {
                String[] splitTemp = url.Split("//");
                url_bak = splitTemp[0] + "//";
             
                url_bak = url_bak + domain;
                if (splitTemp.Length >= 1 && splitTemp[1].IndexOf("/") != -1)
                {
                    String[] urlTemp2 = splitTemp[1].Split("/");
                    if (urlTemp2.Length > 1)
                    {
                        for (int i = 1; i < urlTemp2.Length; i++)
                        {
                            url_bak = url_bak + "/" + urlTemp2[i];
                        }
                    }
                }
            }
            return url_bak;
        }
    }
}
