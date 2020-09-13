namespace Caist.Framework.Mqtt
{
    partial class FrmMian
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMian));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMqtt = new System.Windows.Forms.TabPage();
            this.richMQT = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsTbMqName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsTbServer = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsTbMqPort = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tsTbMqUserName = new System.Windows.Forms.ToolStripTextBox();
            this.MqttStart = new System.Windows.Forms.ToolStripButton();
            this.MqttStop = new System.Windows.Forms.ToolStripButton();
            this.SystemImage = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1.SuspendLayout();
            this.tabMqtt.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabMqtt);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.SystemImage;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(863, 499);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabMqtt
            // 
            this.tabMqtt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabMqtt.Controls.Add(this.richMQT);
            this.tabMqtt.Controls.Add(this.toolStrip1);
            this.tabMqtt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabMqtt.ImageIndex = 0;
            this.tabMqtt.Location = new System.Drawing.Point(4, 26);
            this.tabMqtt.Name = "tabMqtt";
            this.tabMqtt.Padding = new System.Windows.Forms.Padding(3);
            this.tabMqtt.Size = new System.Drawing.Size(855, 469);
            this.tabMqtt.TabIndex = 0;
            this.tabMqtt.Text = "MQTT上传";
            this.tabMqtt.UseVisualStyleBackColor = true;
            // 
            // richMQT
            // 
            this.richMQT.Location = new System.Drawing.Point(7, 42);
            this.richMQT.Name = "richMQT";
            this.richMQT.Size = new System.Drawing.Size(839, 381);
            this.richMQT.TabIndex = 1;
            this.richMQT.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsTbMqName,
            this.toolStripLabel3,
            this.tsTbServer,
            this.toolStripLabel2,
            this.tsTbMqPort,
            this.toolStripLabel4,
            this.tsTbMqUserName,
            this.MqttStart,
            this.MqttStop});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(847, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel1.Text = "矿区名称：";
            // 
            // tsTbMqName
            // 
            this.tsTbMqName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tsTbMqName.Name = "tsTbMqName";
            this.tsTbMqName.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel3.Text = "上传IP：";
            // 
            // tsTbServer
            // 
            this.tsTbServer.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tsTbServer.Name = "tsTbServer";
            this.tsTbServer.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel2.Text = "端口：";
            // 
            // tsTbMqPort
            // 
            this.tsTbMqPort.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tsTbMqPort.Name = "tsTbMqPort";
            this.tsTbMqPort.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel4.Text = "账号：";
            // 
            // tsTbMqUserName
            // 
            this.tsTbMqUserName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tsTbMqUserName.Name = "tsTbMqUserName";
            this.tsTbMqUserName.Size = new System.Drawing.Size(100, 25);
            // 
            // MqttStart
            // 
            this.MqttStart.Image = global::Caist.Framework.MqttTool.Properties.Resources.start;
            this.MqttStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MqttStart.Name = "MqttStart";
            this.MqttStart.Size = new System.Drawing.Size(52, 22);
            this.MqttStart.Text = "启动";
            this.MqttStart.Click += new System.EventHandler(this.MqttStart_Click);
            // 
            // MqttStop
            // 
            this.MqttStop.Enabled = false;
            this.MqttStop.Image = global::Caist.Framework.MqttTool.Properties.Resources.stop;
            this.MqttStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MqttStop.Name = "MqttStop";
            this.MqttStop.Size = new System.Drawing.Size(52, 22);
            this.MqttStop.Text = "停止";
            this.MqttStop.Click += new System.EventHandler(this.MqttStop_Click);
            // 
            // SystemImage
            // 
            this.SystemImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SystemImage.ImageStream")));
            this.SystemImage.TransparentColor = System.Drawing.Color.Transparent;
            this.SystemImage.Images.SetKeyName(0, "Mqt-push.png");
            this.SystemImage.Images.SetKeyName(1, "start.png");
            this.SystemImage.Images.SetKeyName(2, "stop.png");
            // 
            // FrmMian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 499);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMian";
            this.Text = "MQTT数据上传工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMian_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMian_FormClosed);
            this.Load += new System.EventHandler(this.FrmMian_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabMqtt.ResumeLayout(false);
            this.tabMqtt.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMqtt;
        private System.Windows.Forms.ImageList SystemImage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tsTbMqName;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox tsTbServer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tsTbMqPort;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox tsTbMqUserName;
        public System.Windows.Forms.ToolStripButton MqttStart;
        private System.Windows.Forms.ToolStripButton MqttStop;
        public System.Windows.Forms.RichTextBox richMQT;
    }
}