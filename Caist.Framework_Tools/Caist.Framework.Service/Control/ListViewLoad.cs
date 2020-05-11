using System.Windows.Forms;

namespace Caist.Framework.Service.Control
{
    public partial class ListViewLoad : System.Windows.Forms.ListView
    {
        public ListViewLoad()
        {
            // 开启双缓冲
            this.SetStyle(ControlStyles.DoubleBuffer |
                  ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.AllPaintingInWmPaint, true);
            // 启用OnNotifyMessage事件，以便我们有机会筛选出
            // Windows消息在到达窗体的WndProc之前
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            UpdateStyles();
        }

        protected override void OnNotifyMessage(Message m)
        {
            //过滤掉WM_ERASEBKGND消息
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }

        }
    }
}
