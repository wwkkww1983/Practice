namespace DLClient
{
    partial class 登录
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(登录));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.denglu = new System.Windows.Forms.Button();
            this.textMm = new System.Windows.Forms.TextBox();
            this.textYhm = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(429, 347);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(423, 269);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "用户名";
            // 
            // denglu
            // 
            this.denglu.BackColor = System.Drawing.SystemColors.Highlight;
            this.denglu.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.denglu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.denglu.Location = new System.Drawing.Point(476, 406);
            this.denglu.Margin = new System.Windows.Forms.Padding(2);
            this.denglu.Name = "denglu";
            this.denglu.Size = new System.Drawing.Size(95, 44);
            this.denglu.TabIndex = 11;
            this.denglu.Text = "登 录";
            this.denglu.UseVisualStyleBackColor = false;
            this.denglu.Click += new System.EventHandler(this.denglu_Click);
            // 
            // textMm
            // 
            this.textMm.Location = new System.Drawing.Point(486, 345);
            this.textMm.Margin = new System.Windows.Forms.Padding(2);
            this.textMm.Name = "textMm";
            this.textMm.PasswordChar = '*';
            this.textMm.Size = new System.Drawing.Size(124, 21);
            this.textMm.TabIndex = 10;
            // 
            // textYhm
            // 
            this.textYhm.Location = new System.Drawing.Point(486, 267);
            this.textYhm.Margin = new System.Windows.Forms.Padding(2);
            this.textYhm.Name = "textYhm";
            this.textYhm.Size = new System.Drawing.Size(124, 21);
            this.textYhm.TabIndex = 9;
            // 
            // 登录
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1018, 595);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.denglu);
            this.Controls.Add(this.textMm);
            this.Controls.Add(this.textYhm);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "登录";
            this.Text = "电缆敷设受力计算与监控";
            this.Load += new System.EventHandler(this.登录_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.登录_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button denglu;
        private System.Windows.Forms.TextBox textMm;
        private System.Windows.Forms.TextBox textYhm;
    }
}