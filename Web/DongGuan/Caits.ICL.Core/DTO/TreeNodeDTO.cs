using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace JL.Done.Core.DTO
{
    /// <summary>
    /// easyuitreenode
    /// </summary>
    public class TreeNodeDTO
    {
        private string _id;
        private string _text;
        private int _state;
        private bool _checked;
        private string _iconCls;
        private List<TreeNodeDTO> _children = new List<TreeNodeDTO>();

        public string id { get => _id; set => _id = value; }
        public string text { get => _text; set => _text = value; }
        public int state { get { return _state; } set { _state = value; } }

        [DataMember(Name = "checked")]
        public bool @checked { get { return _checked; } set { _checked = value; } }
        public string iconCls { get { return _iconCls; } set { _iconCls = value; } }

        public List<TreeNodeDTO> children { get { return _children; } set { _children = value; } }
    }

    /// <summary>
    /// LayUi树节点型
    /// </summary>
    public class LayUiTreeNodeDTO
    {
        private string _id;
        private string _name;
        private string _href;
        private string _iconCls;
        private string _url;
        private List<LayUiTreeNodeDTO> _children = new List<LayUiTreeNodeDTO>();

        public string id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = value; }
        public string href { get { return _href; } set { _href = value; } }
        public string Url { get { return _url; } set { _url = value; } }
        public string IconCls { get { return _iconCls; } set { _iconCls = value; } }

        public List<LayUiTreeNodeDTO> children { get { return _children; } set { _children = value; } }
    }
}
