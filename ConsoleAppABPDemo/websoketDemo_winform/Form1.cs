using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace websoketDemo_winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitSocket();
        }

        private WebSocket ws;
        public void InitSocket()
        {
            ws = new WebSocket("ws://127.0.0.1:8181");
            ws.Connect();
            ws.OnMessage += Ws_OnMessage;
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            SetText($"接收: {e.Data}");
        }

        //delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                //SetTextCallback d = new SetTextCallback(SetText);
                //this.Invoke(d, new object[] { text });
                this.Invoke(new Action<string>((txt) =>
                {
                    SetText(txt);
                }), new object[] { text });
            }
            else
            {
                richTextBox1.Text += $"\r\n {text}";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var txt = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(txt))
            {
                ws.Send(txt); // working
                SetText($"发送: {txt}");
                textBox1.Text = string.Empty;
                textBox1.Focus();

            }
        }
    }
}
