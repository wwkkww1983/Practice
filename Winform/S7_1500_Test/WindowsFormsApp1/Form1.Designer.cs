namespace WindowsFormsApp1
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Slot = new System.Windows.Forms.TextBox();
            this.Rack = new System.Windows.Forms.TextBox();
            this.IP_Address = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Write_Value = new System.Windows.Forms.NumericUpDown();
            this.Data_Type = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Current_Value = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Write_Data = new System.Windows.Forms.Button();
            this.Read_Data = new System.Windows.Forms.Button();
            this.Start_Address = new System.Windows.Forms.TextBox();
            this.DB_Number = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Write_Value)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.Slot);
            this.groupBox1.Controls.Add(this.Rack);
            this.groupBox1.Controls.Add(this.IP_Address);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 172);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接PLC";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(132, 125);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 35);
            this.button2.TabIndex = 7;
            this.button2.Text = "断开";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Slot
            // 
            this.Slot.Location = new System.Drawing.Point(78, 81);
            this.Slot.Name = "Slot";
            this.Slot.Size = new System.Drawing.Size(65, 21);
            this.Slot.TabIndex = 5;
            // 
            // Rack
            // 
            this.Rack.Location = new System.Drawing.Point(78, 54);
            this.Rack.Name = "Rack";
            this.Rack.Size = new System.Drawing.Size(65, 21);
            this.Rack.TabIndex = 4;
            this.Rack.Text = "0";
            // 
            // IP_Address
            // 
            this.IP_Address.Location = new System.Drawing.Point(78, 27);
            this.IP_Address.Name = "IP_Address";
            this.IP_Address.Size = new System.Drawing.Size(149, 21);
            this.IP_Address.TabIndex = 3;
            this.IP_Address.Text = "192.168.200.57";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "PLC 插槽";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "PLC 机架";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "PLC IP地址";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Write_Value);
            this.groupBox2.Controls.Add(this.Data_Type);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.Current_Value);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.Write_Data);
            this.groupBox2.Controls.Add(this.Read_Data);
            this.groupBox2.Controls.Add(this.Start_Address);
            this.groupBox2.Controls.Add(this.DB_Number);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(327, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(245, 172);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作数据";
            // 
            // Write_Value
            // 
            this.Write_Value.Location = new System.Drawing.Point(183, 54);
            this.Write_Value.Name = "Write_Value";
            this.Write_Value.Size = new System.Drawing.Size(53, 21);
            this.Write_Value.TabIndex = 9;
            // 
            // Data_Type
            // 
            this.Data_Type.FormattingEnabled = true;
            this.Data_Type.Location = new System.Drawing.Point(62, 82);
            this.Data_Type.Name = "Data_Type";
            this.Data_Type.Size = new System.Drawing.Size(68, 20);
            this.Data_Type.TabIndex = 2;
            this.Data_Type.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(138, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "写入值";
            // 
            // Current_Value
            // 
            this.Current_Value.Location = new System.Drawing.Point(183, 27);
            this.Current_Value.Name = "Current_Value";
            this.Current_Value.Size = new System.Drawing.Size(53, 21);
            this.Current_Value.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(138, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "当前值";
            // 
            // Write_Data
            // 
            this.Write_Data.Location = new System.Drawing.Point(141, 125);
            this.Write_Data.Name = "Write_Data";
            this.Write_Data.Size = new System.Drawing.Size(95, 35);
            this.Write_Data.TabIndex = 13;
            this.Write_Data.Text = "写数据";
            this.Write_Data.UseVisualStyleBackColor = true;
            this.Write_Data.Click += new System.EventHandler(this.Write_Data_Click);
            // 
            // Read_Data
            // 
            this.Read_Data.Location = new System.Drawing.Point(11, 125);
            this.Read_Data.Name = "Read_Data";
            this.Read_Data.Size = new System.Drawing.Size(95, 35);
            this.Read_Data.TabIndex = 8;
            this.Read_Data.Text = "读数据";
            this.Read_Data.UseVisualStyleBackColor = true;
            this.Read_Data.Click += new System.EventHandler(this.Read_Data_Click);
            // 
            // Start_Address
            // 
            this.Start_Address.Location = new System.Drawing.Point(62, 54);
            this.Start_Address.Name = "Start_Address";
            this.Start_Address.Size = new System.Drawing.Size(68, 21);
            this.Start_Address.TabIndex = 11;
            // 
            // DB_Number
            // 
            this.DB_Number.Location = new System.Drawing.Point(62, 27);
            this.DB_Number.Name = "DB_Number";
            this.DB_Number.Size = new System.Drawing.Size(68, 21);
            this.DB_Number.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "数据类型";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "起始地址";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "DB 块号";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 201);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(560, 35);
            this.textBox1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 238);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "S7_1500_Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Write_Value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Slot;
        private System.Windows.Forms.TextBox Rack;
        private System.Windows.Forms.TextBox IP_Address;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Write_Data;
        private System.Windows.Forms.Button Read_Data;
        private System.Windows.Forms.TextBox Start_Address;
        private System.Windows.Forms.TextBox DB_Number;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Current_Value;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Data_Type;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown Write_Value;
    }
}

