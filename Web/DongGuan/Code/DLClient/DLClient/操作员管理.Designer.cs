namespace DLClient
{
    partial class 操作员管理
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
            this.textXm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.butTc = new System.Windows.Forms.Button();
            this.butQd = new System.Windows.Forms.Button();
            this.textqrmm = new System.Windows.Forms.TextBox();
            this.textMM = new System.Windows.Forms.TextBox();
            this.textYhm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textXm
            // 
            this.textXm.Location = new System.Drawing.Point(100, 108);
            this.textXm.Margin = new System.Windows.Forms.Padding(2);
            this.textXm.Name = "textXm";
            this.textXm.Size = new System.Drawing.Size(120, 21);
            this.textXm.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 110);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "新密码";
            // 
            // butTc
            // 
            this.butTc.Location = new System.Drawing.Point(153, 190);
            this.butTc.Margin = new System.Windows.Forms.Padding(2);
            this.butTc.Name = "butTc";
            this.butTc.Size = new System.Drawing.Size(75, 32);
            this.butTc.TabIndex = 17;
            this.butTc.Text = "关 闭";
            this.butTc.UseVisualStyleBackColor = true;
            this.butTc.Click += new System.EventHandler(this.butTc_Click);
            // 
            // butQd
            // 
            this.butQd.Location = new System.Drawing.Point(47, 190);
            this.butQd.Margin = new System.Windows.Forms.Padding(2);
            this.butQd.Name = "butQd";
            this.butQd.Size = new System.Drawing.Size(71, 32);
            this.butQd.TabIndex = 16;
            this.butQd.Text = "确定修改";
            this.butQd.UseVisualStyleBackColor = true;
            // 
            // textqrmm
            // 
            this.textqrmm.Location = new System.Drawing.Point(100, 153);
            this.textqrmm.Margin = new System.Windows.Forms.Padding(2);
            this.textqrmm.Name = "textqrmm";
            this.textqrmm.Size = new System.Drawing.Size(120, 21);
            this.textqrmm.TabIndex = 15;
            // 
            // textMM
            // 
            this.textMM.Location = new System.Drawing.Point(100, 63);
            this.textMM.Margin = new System.Windows.Forms.Padding(2);
            this.textMM.Name = "textMM";
            this.textMM.Size = new System.Drawing.Size(120, 21);
            this.textMM.TabIndex = 14;
            // 
            // textYhm
            // 
            this.textYhm.Location = new System.Drawing.Point(100, 20);
            this.textYhm.Margin = new System.Windows.Forms.Padding(2);
            this.textYhm.Name = "textYhm";
            this.textYhm.Size = new System.Drawing.Size(120, 21);
            this.textYhm.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 156);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "确认密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "旧密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "登录名";
            // 
            // 操作员管理
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 238);
            this.Controls.Add(this.textXm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.butTc);
            this.Controls.Add(this.butQd);
            this.Controls.Add(this.textqrmm);
            this.Controls.Add(this.textMM);
            this.Controls.Add(this.textYhm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "操作员管理";
            this.Text = "修改密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textXm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button butTc;
        public System.Windows.Forms.Button butQd;
        public System.Windows.Forms.TextBox textqrmm;
        public System.Windows.Forms.TextBox textMM;
        public System.Windows.Forms.TextBox textYhm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}