namespace SyncFiles
{
    partial class FormFiber
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
            this.btnSync = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtReadNodeId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSub = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.txtSubNodeId = new System.Windows.Forms.TextBox();
            this.txtReadNodeIdVal = new System.Windows.Forms.TextBox();
            this.txtSubNodeIdVal = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(22, 39);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(75, 23);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(122, 39);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSync);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Location = new System.Drawing.Point(51, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 85);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "光纤测温";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtAddress);
            this.groupBox2.Controls.Add(this.btnConnect);
            this.groupBox2.Location = new System.Drawing.Point(51, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 318);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "供配电";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(22, 20);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "链接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(62, 50);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(481, 21);
            this.txtAddress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "地址：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRead);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtReadNodeIdVal);
            this.groupBox3.Controls.Add(this.txtReadNodeId);
            this.groupBox3.Location = new System.Drawing.Point(22, 90);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(521, 100);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "读取方式";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.btnSub);
            this.groupBox4.Controls.Add(this.txtSubNodeIdVal);
            this.groupBox4.Controls.Add(this.txtSubNodeId);
            this.groupBox4.Location = new System.Drawing.Point(22, 212);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(521, 100);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "订阅方式";
            // 
            // txtReadNodeId
            // 
            this.txtReadNodeId.Location = new System.Drawing.Point(74, 20);
            this.txtReadNodeId.Name = "txtReadNodeId";
            this.txtReadNodeId.Size = new System.Drawing.Size(432, 21);
            this.txtReadNodeId.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "NodeId：";
            // 
            // btnSub
            // 
            this.btnSub.Location = new System.Drawing.Point(22, 56);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(75, 23);
            this.btnSub.TabIndex = 0;
            this.btnSub.Text = "订阅";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "NodeId：";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(22, 61);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txtSubNodeId
            // 
            this.txtSubNodeId.Location = new System.Drawing.Point(74, 14);
            this.txtSubNodeId.Name = "txtSubNodeId";
            this.txtSubNodeId.Size = new System.Drawing.Size(432, 21);
            this.txtSubNodeId.TabIndex = 0;
            // 
            // txtReadNodeIdVal
            // 
            this.txtReadNodeIdVal.Location = new System.Drawing.Point(100, 61);
            this.txtReadNodeIdVal.Name = "txtReadNodeIdVal";
            this.txtReadNodeIdVal.Size = new System.Drawing.Size(122, 21);
            this.txtReadNodeIdVal.TabIndex = 0;
            // 
            // txtSubNodeIdVal
            // 
            this.txtSubNodeIdVal.Location = new System.Drawing.Point(100, 56);
            this.txtSubNodeIdVal.Name = "txtSubNodeIdVal";
            this.txtSubNodeIdVal.Size = new System.Drawing.Size(122, 21);
            this.txtSubNodeIdVal.TabIndex = 0;
            // 
            // FormFiber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 578);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormFiber";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.TextBox txtSubNodeId;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReadNodeId;
        private System.Windows.Forms.TextBox txtSubNodeIdVal;
        private System.Windows.Forms.TextBox txtReadNodeIdVal;
    }
}

