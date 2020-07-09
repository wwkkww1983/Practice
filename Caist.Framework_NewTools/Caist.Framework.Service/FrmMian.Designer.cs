using Caist.Framework.Service.Control;

namespace Caist.Framework.Service
{
    partial class FrmMian
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMian));
            this.SystemMenu = new System.Windows.Forms.MenuStrip();
            this.系统设置SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLC参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SystemStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txttiming = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPLC = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TreeDevice = new System.Windows.Forms.TreeView();
            this.ImageDevice = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lvPoint = new Caist.Framework.Service.Control.ListViewLoad();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.PlcTool = new System.Windows.Forms.ToolStrip();
            this.btnPLCStrats = new System.Windows.Forms.ToolStripButton();
            this.btnPLCStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabDataBase = new System.Windows.Forms.TabPage();
            this.lvSyncLog = new System.Windows.Forms.RichTextBox();
            this.lblwarn = new System.Windows.Forms.Label();
            this.lblWaring = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabMQT = new System.Windows.Forms.TabPage();
            this.MqtTool = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsTbMqName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tsTbServer = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tsTbMqPort = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.tsTbMqUserName = new System.Windows.Forms.ToolStripTextBox();
            this.MqttStart = new System.Windows.Forms.ToolStripButton();
            this.MqttStop = new System.Windows.Forms.ToolStripButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richMQT = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.webMessage = new System.Windows.Forms.RichTextBox();
            this.webContent = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.webAddress = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.webPort = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.webStart = new System.Windows.Forms.ToolStripButton();
            this.webStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.webSend = new System.Windows.Forms.ToolStripButton();
            this.SystemImage = new System.Windows.Forms.ImageList(this.components);
            this.timing = new System.Windows.Forms.Timer(this.components);
            this.timerSyncData = new System.Windows.Forms.Timer(this.components);
            this.SystemMenu.SuspendLayout();
            this.SystemStatus.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPLC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.PlcTool.SuspendLayout();
            this.tabDataBase.SuspendLayout();
            this.tabMQT.SuspendLayout();
            this.MqtTool.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SystemMenu
            // 
            this.SystemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统设置SToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.SystemMenu.Location = new System.Drawing.Point(0, 0);
            this.SystemMenu.Name = "SystemMenu";
            this.SystemMenu.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.SystemMenu.Size = new System.Drawing.Size(1006, 26);
            this.SystemMenu.TabIndex = 0;
            this.SystemMenu.Text = "menuStrip1";
            // 
            // 系统设置SToolStripMenuItem
            // 
            this.系统设置SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pLC参数设置ToolStripMenuItem,
            this.btnClose});
            this.系统设置SToolStripMenuItem.Name = "系统设置SToolStripMenuItem";
            this.系统设置SToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.系统设置SToolStripMenuItem.Text = "系统设置(&S)";
            // 
            // pLC参数设置ToolStripMenuItem
            // 
            this.pLC参数设置ToolStripMenuItem.Name = "pLC参数设置ToolStripMenuItem";
            this.pLC参数设置ToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.pLC参数设置ToolStripMenuItem.Text = "服务设置";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Name = "btnClose";
            this.btnClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.btnClose.Size = new System.Drawing.Size(157, 22);
            this.btnClose.Text = "退出(&X)";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("帮助HToolStripMenuItem.Image")));
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // SystemStatus
            // 
            this.SystemStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.txttiming,
            this.toolStripStatusLabel3,
            this.txtStatus});
            this.SystemStatus.Location = new System.Drawing.Point(0, 552);
            this.SystemStatus.Name = "SystemStatus";
            this.SystemStatus.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.SystemStatus.Size = new System.Drawing.Size(1006, 22);
            this.SystemStatus.TabIndex = 1;
            this.SystemStatus.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(98, 17);
            this.toolStripStatusLabel1.Text = "当前运行时长：";
            // 
            // txttiming
            // 
            this.txttiming.Name = "txttiming";
            this.txttiming.Size = new System.Drawing.Size(63, 17);
            this.txttiming.Text = "--：--：--";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Enabled = false;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(93, 17);
            this.toolStripStatusLabel3.Text = "PLC当前状态：";
            this.toolStripStatusLabel3.Visible = false;
            // 
            // txtStatus
            // 
            this.txtStatus.Enabled = false;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(37, 17);
            this.txtStatus.Text = "------";
            this.txtStatus.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPLC);
            this.tabControl1.Controls.Add(this.tabDataBase);
            this.tabControl1.Controls.Add(this.tabMQT);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.SystemImage;
            this.tabControl1.Location = new System.Drawing.Point(0, 26);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1006, 526);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 2;
            // 
            // tabPLC
            // 
            this.tabPLC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPLC.Controls.Add(this.splitContainer1);
            this.tabPLC.Controls.Add(this.PlcTool);
            this.tabPLC.ImageIndex = 0;
            this.tabPLC.Location = new System.Drawing.Point(4, 29);
            this.tabPLC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPLC.Name = "tabPLC";
            this.tabPLC.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPLC.Size = new System.Drawing.Size(998, 493);
            this.tabPLC.TabIndex = 0;
            this.tabPLC.Text = "PLC采集";
            this.tabPLC.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TreeDevice);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(990, 458);
            this.splitContainer1.SplitterDistance = 282;
            this.splitContainer1.TabIndex = 1;
            // 
            // TreeDevice
            // 
            this.TreeDevice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeDevice.ImageIndex = 0;
            this.TreeDevice.ImageList = this.ImageDevice;
            this.TreeDevice.Location = new System.Drawing.Point(0, 0);
            this.TreeDevice.Name = "TreeDevice";
            this.TreeDevice.SelectedImageIndex = 0;
            this.TreeDevice.Size = new System.Drawing.Size(280, 456);
            this.TreeDevice.TabIndex = 0;
            // 
            // ImageDevice
            // 
            this.ImageDevice.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageDevice.ImageStream")));
            this.ImageDevice.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageDevice.Images.SetKeyName(0, "Example_16x16.png");
            this.ImageDevice.Images.SetKeyName(1, "Publish_16x16.png");
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvPoint);
            this.splitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtMessage);
            this.splitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.Size = new System.Drawing.Size(704, 458);
            this.splitContainer2.SplitterDistance = 259;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 1;
            // 
            // lvPoint
            // 
            this.lvPoint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvPoint.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPoint.FullRowSelect = true;
            this.lvPoint.GridLines = true;
            this.lvPoint.HideSelection = false;
            this.lvPoint.Location = new System.Drawing.Point(0, 0);
            this.lvPoint.MultiSelect = false;
            this.lvPoint.Name = "lvPoint";
            this.lvPoint.Size = new System.Drawing.Size(702, 257);
            this.lvPoint.TabIndex = 0;
            this.lvPoint.UseCompatibleStateImageBehavior = false;
            this.lvPoint.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "指令名称";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "描述";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类型";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "地址";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "值";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "读/写";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMessage
            // 
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(702, 192);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.Text = "";
            // 
            // PlcTool
            // 
            this.PlcTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.PlcTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPLCStrats,
            this.btnPLCStop,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.PlcTool.Location = new System.Drawing.Point(3, 4);
            this.PlcTool.Name = "PlcTool";
            this.PlcTool.Size = new System.Drawing.Size(990, 25);
            this.PlcTool.TabIndex = 0;
            this.PlcTool.Text = "toolStrip1";
            // 
            // btnPLCStrats
            // 
            this.btnPLCStrats.Image = ((System.Drawing.Image)(resources.GetObject("btnPLCStrats.Image")));
            this.btnPLCStrats.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPLCStrats.Name = "btnPLCStrats";
            this.btnPLCStrats.Size = new System.Drawing.Size(53, 22);
            this.btnPLCStrats.Text = "启动";
            this.btnPLCStrats.Click += new System.EventHandler(this.btnPLCStrats_Click);
            // 
            // btnPLCStop
            // 
            this.btnPLCStop.Image = ((System.Drawing.Image)(resources.GetObject("btnPLCStop.Image")));
            this.btnPLCStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPLCStop.Name = "btnPLCStop";
            this.btnPLCStop.Size = new System.Drawing.Size(53, 22);
            this.btnPLCStop.Text = "停止";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(105, 22);
            this.toolStripButton1.Text = "手动发送指令";
            // 
            // tabDataBase
            // 
            this.tabDataBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabDataBase.Controls.Add(this.lvSyncLog);
            this.tabDataBase.Controls.Add(this.lblwarn);
            this.tabDataBase.Controls.Add(this.lblWaring);
            this.tabDataBase.Controls.Add(this.lblHint);
            this.tabDataBase.Controls.Add(this.label2);
            this.tabDataBase.Controls.Add(this.label1);
            this.tabDataBase.Controls.Add(this.txtInterval);
            this.tabDataBase.Controls.Add(this.btnSave);
            this.tabDataBase.Controls.Add(this.btnStop);
            this.tabDataBase.Controls.Add(this.btnStart);
            this.tabDataBase.ImageIndex = 2;
            this.tabDataBase.Location = new System.Drawing.Point(4, 29);
            this.tabDataBase.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabDataBase.Name = "tabDataBase";
            this.tabDataBase.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabDataBase.Size = new System.Drawing.Size(998, 493);
            this.tabDataBase.TabIndex = 1;
            this.tabDataBase.Text = "数据同步";
            this.tabDataBase.UseVisualStyleBackColor = true;
            // 
            // lvSyncLog
            // 
            this.lvSyncLog.Location = new System.Drawing.Point(8, 138);
            this.lvSyncLog.Name = "lvSyncLog";
            this.lvSyncLog.Size = new System.Drawing.Size(982, 348);
            this.lvSyncLog.TabIndex = 13;
            this.lvSyncLog.Text = "";
            // 
            // lblwarn
            // 
            this.lblwarn.AutoSize = true;
            this.lblwarn.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblwarn.ForeColor = System.Drawing.Color.Red;
            this.lblwarn.Location = new System.Drawing.Point(248, 9);
            this.lblwarn.Name = "lblwarn";
            this.lblwarn.Size = new System.Drawing.Size(38, 12);
            this.lblwarn.TabIndex = 8;
            this.lblwarn.Text = "警告:";
            this.lblwarn.Visible = false;
            // 
            // lblWaring
            // 
            this.lblWaring.AutoSize = true;
            this.lblWaring.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWaring.ForeColor = System.Drawing.Color.Black;
            this.lblWaring.Location = new System.Drawing.Point(287, 9);
            this.lblWaring.Name = "lblWaring";
            this.lblWaring.Size = new System.Drawing.Size(0, 12);
            this.lblWaring.TabIndex = 9;
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(410, 73);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(62, 17);
            this.lblHint.TabIndex = 10;
            this.lblHint.Text = "同步中......";
            this.lblHint.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(533, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "(分钟/次)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(286, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "定时设置:";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(346, 40);
            this.txtInterval.MaxLength = 5;
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(181, 23);
            this.txtInterval.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(598, 38);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(496, 104);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 25);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "定时停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(378, 104);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 25);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "同步数据";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tabMQT
            // 
            this.tabMQT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabMQT.Controls.Add(this.MqtTool);
            this.tabMQT.Controls.Add(this.richTextBox1);
            this.tabMQT.Controls.Add(this.richMQT);
            this.tabMQT.ImageIndex = 4;
            this.tabMQT.Location = new System.Drawing.Point(4, 29);
            this.tabMQT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabMQT.Name = "tabMQT";
            this.tabMQT.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabMQT.Size = new System.Drawing.Size(998, 493);
            this.tabMQT.TabIndex = 3;
            this.tabMQT.Text = "MQT上传";
            this.tabMQT.UseVisualStyleBackColor = true;
            // 
            // MqtTool
            // 
            this.MqtTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tsTbMqName,
            this.toolStripLabel4,
            this.tsTbServer,
            this.toolStripLabel5,
            this.tsTbMqPort,
            this.toolStripLabel6,
            this.tsTbMqUserName,
            this.MqttStart,
            this.MqttStop});
            this.MqtTool.Location = new System.Drawing.Point(3, 4);
            this.MqtTool.Name = "MqtTool";
            this.MqtTool.Size = new System.Drawing.Size(990, 25);
            this.MqtTool.TabIndex = 9;
            this.MqtTool.Text = "toolStrip2";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(72, 22);
            this.toolStripLabel3.Text = "矿区名称：";
            // 
            // tsTbMqName
            // 
            this.tsTbMqName.Name = "tsTbMqName";
            this.tsTbMqName.Size = new System.Drawing.Size(150, 25);
            this.tsTbMqName.Text = "贵阳瓮安县陡山煤矿";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabel4.Text = "服务器IP：";
            // 
            // tsTbServer
            // 
            this.tsTbServer.Name = "tsTbServer";
            this.tsTbServer.Size = new System.Drawing.Size(100, 25);
            this.tsTbServer.Text = "127.0.0.1";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel5.Text = "端口：";
            // 
            // tsTbMqPort
            // 
            this.tsTbMqPort.Name = "tsTbMqPort";
            this.tsTbMqPort.Size = new System.Drawing.Size(50, 25);
            this.tsTbMqPort.Text = "8000";
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(72, 22);
            this.toolStripLabel6.Text = "提交账号：";
            // 
            // tsTbMqUserName
            // 
            this.tsTbMqUserName.Name = "tsTbMqUserName";
            this.tsTbMqUserName.Size = new System.Drawing.Size(100, 25);
            this.tsTbMqUserName.Text = "admin";
            // 
            // MqttStart
            // 
            this.MqttStart.Image = global::Caist.Framework.Service.Properties.Resources.start;
            this.MqttStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MqttStart.Name = "MqttStart";
            this.MqttStart.Size = new System.Drawing.Size(53, 22);
            this.MqttStart.Text = "启动";
            this.MqttStart.Click += new System.EventHandler(this.MqttStart_Click);
            // 
            // MqttStop
            // 
            this.MqttStop.Enabled = false;
            this.MqttStop.Image = global::Caist.Framework.Service.Properties.Resources.stop;
            this.MqttStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MqttStop.Name = "MqttStop";
            this.MqttStop.Size = new System.Drawing.Size(53, 22);
            this.MqttStop.Text = "停止";
            this.MqttStop.Click += new System.EventHandler(this.MqttStop_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(-388, 782);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(995, 261);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // richMQT
            // 
            this.richMQT.Location = new System.Drawing.Point(-1, 46);
            this.richMQT.Name = "richMQT";
            this.richMQT.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richMQT.Size = new System.Drawing.Size(995, 442);
            this.richMQT.TabIndex = 0;
            this.richMQT.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.splitContainer3);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.ImageIndex = 3;
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(998, 493);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "消息历史";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 4);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.webMessage);
            this.splitContainer3.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.webContent);
            this.splitContainer3.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer3.Size = new System.Drawing.Size(990, 483);
            this.splitContainer3.SplitterDistance = 311;
            this.splitContainer3.TabIndex = 1;
            // 
            // webMessage
            // 
            this.webMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.webMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webMessage.Location = new System.Drawing.Point(0, 0);
            this.webMessage.Name = "webMessage";
            this.webMessage.ReadOnly = true;
            this.webMessage.Size = new System.Drawing.Size(988, 309);
            this.webMessage.TabIndex = 1;
            this.webMessage.Text = "";
            // 
            // webContent
            // 
            this.webContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.webContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webContent.Location = new System.Drawing.Point(0, 0);
            this.webContent.Name = "webContent";
            this.webContent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.webContent.Size = new System.Drawing.Size(988, 166);
            this.webContent.TabIndex = 0;
            this.webContent.Text = "";
            this.webContent.TextChanged += new System.EventHandler(this.webContent_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.webAddress,
            this.toolStripLabel2,
            this.webPort,
            this.toolStripSeparator2,
            this.webStart,
            this.webStop,
            this.toolStripSeparator3,
            this.webSend});
            this.toolStrip1.Location = new System.Drawing.Point(3, 4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(990, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "IP地址：";
            // 
            // webAddress
            // 
            this.webAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.webAddress.Name = "webAddress";
            this.webAddress.Size = new System.Drawing.Size(120, 25);
            this.webAddress.Text = "0.0.0.0";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel2.Text = "端口：";
            // 
            // webPort
            // 
            this.webPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.webPort.Name = "webPort";
            this.webPort.Size = new System.Drawing.Size(60, 25);
            this.webPort.Text = "8000";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // webStart
            // 
            this.webStart.Image = ((System.Drawing.Image)(resources.GetObject("webStart.Image")));
            this.webStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.webStart.Name = "webStart";
            this.webStart.Size = new System.Drawing.Size(53, 22);
            this.webStart.Text = "启动";
            this.webStart.Click += new System.EventHandler(this.webSocketStart_Click);
            // 
            // webStop
            // 
            this.webStop.Enabled = false;
            this.webStop.Image = ((System.Drawing.Image)(resources.GetObject("webStop.Image")));
            this.webStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.webStop.Name = "webStop";
            this.webStop.Size = new System.Drawing.Size(53, 22);
            this.webStop.Text = "关闭";
            this.webStop.Click += new System.EventHandler(this.webStop_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // webSend
            // 
            this.webSend.Enabled = false;
            this.webSend.Image = ((System.Drawing.Image)(resources.GetObject("webSend.Image")));
            this.webSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.webSend.Name = "webSend";
            this.webSend.Size = new System.Drawing.Size(53, 22);
            this.webSend.Text = "发送";
            this.webSend.Click += new System.EventHandler(this.webSend_Click);
            // 
            // SystemImage
            // 
            this.SystemImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SystemImage.ImageStream")));
            this.SystemImage.TransparentColor = System.Drawing.Color.Transparent;
            this.SystemImage.Images.SetKeyName(0, "Example_16x16.png");
            this.SystemImage.Images.SetKeyName(1, "OpenHighLowCloseCandleStick_16x16.png");
            this.SystemImage.Images.SetKeyName(2, "Database_16x16.png");
            this.SystemImage.Images.SetKeyName(3, "OperatingSyste_16x16.png");
            this.SystemImage.Images.SetKeyName(4, "Mqt-push.png");
            // 
            // timing
            // 
            this.timing.Interval = 1;
            this.timing.Tick += new System.EventHandler(this.timing_Tick);
            // 
            // timerSyncData
            // 
            this.timerSyncData.Interval = 1000;
            // 
            // FrmMian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 574);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.SystemStatus);
            this.Controls.Add(this.SystemMenu);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.SystemMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FrmMian";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中航信息-煤矿数据采集服务端";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMian_FormClosed);
            this.Load += new System.EventHandler(this.FrmMian_Load);
            this.SystemMenu.ResumeLayout(false);
            this.SystemMenu.PerformLayout();
            this.SystemStatus.ResumeLayout(false);
            this.SystemStatus.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPLC.ResumeLayout(false);
            this.tabPLC.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.PlcTool.ResumeLayout(false);
            this.PlcTool.PerformLayout();
            this.tabDataBase.ResumeLayout(false);
            this.tabDataBase.PerformLayout();
            this.tabMQT.ResumeLayout(false);
            this.tabMQT.PerformLayout();
            this.MqtTool.ResumeLayout(false);
            this.MqtTool.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip SystemMenu;
        private System.Windows.Forms.StatusStrip SystemStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDataBase;
        private System.Windows.Forms.ImageList SystemImage;
        private System.Windows.Forms.TabPage tabPLC;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip PlcTool;
        private System.Windows.Forms.TreeView TreeDevice;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.ToolStripMenuItem 系统设置SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pLC参数设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnClose;
        private System.Windows.Forms.ToolStripButton btnPLCStrats;
        private System.Windows.Forms.ToolStripButton btnPLCStop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel txttiming;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private ListViewLoad lvPoint;
        private System.Windows.Forms.ImageList ImageDevice;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel txtStatus;
        private System.Windows.Forms.Timer timing;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox webAddress;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox webPort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton webStart;
        private System.Windows.Forms.ToolStripButton webStop;
        private System.Windows.Forms.RichTextBox webMessage;
        private System.Windows.Forms.RichTextBox webContent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton webSend;
        private System.Windows.Forms.Timer timerSyncData;
        private System.Windows.Forms.TabPage tabMQT;
        private System.Windows.Forms.ToolStrip MqtTool;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox tsTbMqName;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox tsTbServer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox tsTbMqPort;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripTextBox tsTbMqUserName;
        public System.Windows.Forms.ToolStripButton MqttStart;
        public System.Windows.Forms.RichTextBox richMQT;
        public System.Windows.Forms.RichTextBox richTextBox1;
        public System.Windows.Forms.ToolStripButton MqttStop;
        private System.Windows.Forms.RichTextBox lvSyncLog;
        private System.Windows.Forms.Label lblwarn;
        private System.Windows.Forms.Label lblWaring;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
    }
}

