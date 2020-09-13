namespace SyncDataBase
{
    partial class Form1
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
            this.btnStart = new System.Windows.Forms.Button();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblWaring = new System.Windows.Forms.Label();
            this.lblwarn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(85, 181);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "同步数据";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(117, 121);
            this.txtInterval.MaxLength = 5;
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(136, 21);
            this.txtInterval.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "定时设置:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "(分钟/次)";
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(324, 119);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(197, 181);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "定时停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(164, 154);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(77, 12);
            this.lblHint.TabIndex = 2;
            this.lblHint.Text = "同步中......";
            this.lblHint.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(296, 181);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "终止同步";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblWaring
            // 
            this.lblWaring.AutoSize = true;
            this.lblWaring.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWaring.ForeColor = System.Drawing.Color.Black;
            this.lblWaring.Location = new System.Drawing.Point(51, 68);
            this.lblWaring.Name = "lblWaring";
            this.lblWaring.Size = new System.Drawing.Size(0, 12);
            this.lblWaring.TabIndex = 2;
            // 
            // lblwarn
            // 
            this.lblwarn.AutoSize = true;
            this.lblwarn.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblwarn.ForeColor = System.Drawing.Color.Red;
            this.lblwarn.Location = new System.Drawing.Point(12, 68);
            this.lblwarn.Name = "lblwarn";
            this.lblwarn.Size = new System.Drawing.Size(38, 12);
            this.lblwarn.TabIndex = 2;
            this.lblwarn.Text = "警告:";
            this.lblwarn.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 379);
            this.Controls.Add(this.lblwarn);
            this.Controls.Add(this.lblWaring);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblWaring;
        private System.Windows.Forms.Label lblwarn;
    }
}

