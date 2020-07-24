using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Util.Model
{
    public class SystemConfig
    {
        /// <summary>
        /// 是否是Demo模式
        /// </summary>
        public bool Demo { get; set; }
        /// <summary>
        /// 是否是调试模式
        /// </summary>
        public bool Debug { get; set; }
        /// <summary>
        /// 允许一个用户在多个电脑同时登录
        /// </summary>
        public bool LoginMultiple { get; set; }
        public string LoginProvider { get; set; }
        /// <summary>
        ///  数据库超时间（秒）
        /// </summary>
        public int CommandTimeout { get; set; }
        /// <summary>
        /// Snow Flake Worker Id
        /// </summary>
        public int SnowFlakeWorkerId { get; set; }
        /// <summary>
        /// api地址
        /// </summary>
        public string ApiSite { get; set; }
        /// <summary>
        /// 允许跨域的站点
        /// </summary>
        public string AllowCorsSite { get; set; }
        /// <summary>
        /// 数据库备份路径
        /// </summary>
        public string DatabaseBackup { get; set; }
        /// <summary>
        /// 网站虚拟目录
        /// </summary>
        public string VirtualDirectory { get; set; }
        /// <summary>
        /// 限制主题模块添加数据条数
        /// </summary>
        public int AddModelLimit { get; set; }

        public string AppId { get; set; }
        public string AppSecret { get; set; }
        /// <summary>
        /// 商户Id
        /// </summary>
        public string MerchantId { get; set; }
        public string PayKey { get; set; }
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 配置的后端站点URL
        /// </summary>
        public string WebUrI { get; set; }
        /// <summary>
        /// 提供给省平台视频Header 头部Caller
        /// </summary>
        public string VideoCaller { get; set; }
        /// <summary>
        /// 提供给省平台的加密key
        /// </summary>
        public string VideoKey { get; set; }

        /// <summary>
        /// 配置的煤矿名称
        /// </summary>
        public string MkName { get; set; }

        /// <summary>
        /// 视频外网IP
        /// </summary>
        public string VideoExtranetIP { get; set; }
        /// <summary>
        /// 信息发布模板路径
        /// </summary>
        public string InformationPublishTemplatePath { get; set; }
        /// <summary>
        /// 信息发布路径
        /// </summary>
        public string InformationPublishPath { get; set; }
        /// <summary>
        /// 上传服务器地址
        /// </summary>
        public string FTPServer { get; set; }
        /// <summary>
        /// 上传服务器用户名
        /// </summary>
        public string FTPUser { get; set; }
        /// <summary>
        /// 上传服务器密码
        /// </summary>
        public string FTPPwd { get; set; }

    }
}
