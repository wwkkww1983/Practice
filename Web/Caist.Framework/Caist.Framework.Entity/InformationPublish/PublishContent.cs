using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.Framework.Entity
{
    public class PublishContent
    {
        public string moveSpeed { get; set; }
        public string fontName { get; set; }
        public string fontColor { get; set; }
        public string showMode { get; set; }
        public string attribute { get; set; }
        public string linkContent { get; set; }
        public string fontSize { get; set; }
        public string deviceUID { get; set; }
        public string stopTime { get; set; }
    }


    /// <summary>
    /// 提供给页面显示的数据对象
    /// </summary>
    public class PublishContentPage
    {
        public string moveSpeed { get; set; }
        public string fontName { get; set; }
        public string fontColor { get; set; }
        public string showMode { get; set; }
        public string attribute { get; set; }
        public string linkContent { get; set; }
        public string fontSize { get; set; }
        public string deviceUID { get; set; }
        public string stopTime { get; set; }
        public string deviceName { get; set; }
    }
}
