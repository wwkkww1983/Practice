namespace Caist.Framework.Video
{
    partial class Form
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lvdevice = new System.Windows.Forms.ListView();
            this.deviceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deviceCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.btncopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(13, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(107, 13);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lvdevice
            // 
            this.lvdevice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvdevice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.deviceName,
            this.deviceCode});
            this.lvdevice.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvdevice.FullRowSelect = true;
            this.lvdevice.GridLines = true;
            this.lvdevice.HideSelection = false;
            this.lvdevice.Location = new System.Drawing.Point(13, 56);
            this.lvdevice.MultiSelect = false;
            this.lvdevice.Name = "lvdevice";
            this.lvdevice.Size = new System.Drawing.Size(584, 535);
            this.lvdevice.TabIndex = 2;
            this.lvdevice.UseCompatibleStateImageBehavior = false;
            this.lvdevice.View = System.Windows.Forms.View.Details;
            // 
            // deviceName
            // 
            this.deviceName.Text = "设备名称";
            this.deviceName.Width = 200;
            // 
            // deviceCode
            // 
            this.deviceCode.Text = "设备编号";
            this.deviceCode.Width = 350;
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(603, 56);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(373, 535);
            this.tbMessage.TabIndex = 3;
            // 
            // btncopy
            // 
            this.btncopy.Location = new System.Drawing.Point(206, 13);
            this.btncopy.Name = "btncopy";
            this.btncopy.Size = new System.Drawing.Size(75, 23);
            this.btncopy.TabIndex = 4;
            this.btncopy.Text = "复制设备信息";
            this.btncopy.UseVisualStyleBackColor = true;
            this.btncopy.Click += new System.EventHandler(this.btncopy_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 603);
            this.Controls.Add(this.btncopy);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.lvdevice);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "Form";
            this.Text = "海康视频资源同步程序";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ListView lvdevice;
        private System.Windows.Forms.ColumnHeader deviceName;
        private System.Windows.Forms.ColumnHeader deviceCode;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Button btncopy;
    }
}

