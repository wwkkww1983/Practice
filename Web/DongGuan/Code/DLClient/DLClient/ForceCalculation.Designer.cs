namespace DLClient
{
    partial class ForceCalculation
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Section = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lengths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuryType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ForceValue1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ForceValue2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfQualify = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProposedProg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cmsection = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dataGridView1.Location = new System.Drawing.Point(0, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1054, 222);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "所属工程";
            this.Column2.Name = "Column2";
            this.Column2.Width = 240;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "点名称";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "连接点号";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "点特征";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "X坐标";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Y坐标";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "地面";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "材质";
            this.Column9.Name = "Column9";
            this.Column9.Width = 80;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(482, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "受力结果分析";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(423, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "选择分段再上传轨迹点";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(848, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "开始计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.Color.LightCyan;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Section,
            this.Lengths,
            this.BuryType,
            this.ForceValue1,
            this.ForceValue2,
            this.IfQualify,
            this.ProposedProg,
            this.Material,
            this.StartX,
            this.StartY,
            this.StartZ,
            this.EndX,
            this.EndY,
            this.EndZ});
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(0, 304);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(1054, 234);
            this.dataGridView2.TabIndex = 5;
            // 
            // id
            // 
            this.id.HeaderText = "序号";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 55;
            // 
            // Section
            // 
            this.Section.HeaderText = "区段";
            this.Section.Name = "Section";
            this.Section.ReadOnly = true;
            this.Section.Width = 80;
            // 
            // Lengths
            // 
            this.Lengths.HeaderText = "长度";
            this.Lengths.Name = "Lengths";
            this.Lengths.ReadOnly = true;
            this.Lengths.Width = 75;
            // 
            // BuryType
            // 
            this.BuryType.HeaderText = "敷设方式";
            this.BuryType.Name = "BuryType";
            this.BuryType.ReadOnly = true;
            this.BuryType.Width = 90;
            // 
            // ForceValue1
            // 
            this.ForceValue1.HeaderText = "侧压力/N";
            this.ForceValue1.Name = "ForceValue1";
            this.ForceValue1.ReadOnly = true;
            this.ForceValue1.Width = 90;
            // 
            // ForceValue2
            // 
            this.ForceValue2.HeaderText = "牵引力/N";
            this.ForceValue2.Name = "ForceValue2";
            this.ForceValue2.ReadOnly = true;
            this.ForceValue2.Width = 90;
            // 
            // IfQualify
            // 
            this.IfQualify.HeaderText = "是否合格";
            this.IfQualify.Name = "IfQualify";
            this.IfQualify.ReadOnly = true;
            this.IfQualify.Width = 90;
            // 
            // ProposedProg
            // 
            this.ProposedProg.HeaderText = "建议控制措施";
            this.ProposedProg.Name = "ProposedProg";
            this.ProposedProg.ReadOnly = true;
            this.ProposedProg.Width = 134;
            // 
            // Material
            // 
            this.Material.HeaderText = "材质";
            this.Material.Name = "Material";
            this.Material.ReadOnly = true;
            this.Material.Width = 78;
            // 
            // StartX
            // 
            this.StartX.HeaderText = "起点X";
            this.StartX.Name = "StartX";
            this.StartX.ReadOnly = true;
            this.StartX.Width = 85;
            // 
            // StartY
            // 
            this.StartY.HeaderText = "起点Y";
            this.StartY.Name = "StartY";
            this.StartY.ReadOnly = true;
            this.StartY.Width = 85;
            // 
            // StartZ
            // 
            this.StartZ.HeaderText = "起点Z";
            this.StartZ.Name = "StartZ";
            this.StartZ.ReadOnly = true;
            this.StartZ.Width = 85;
            // 
            // EndX
            // 
            this.EndX.HeaderText = "终点X";
            this.EndX.Name = "EndX";
            this.EndX.ReadOnly = true;
            this.EndX.Width = 85;
            // 
            // EndY
            // 
            this.EndY.HeaderText = "终点Y";
            this.EndY.Name = "EndY";
            this.EndY.ReadOnly = true;
            this.EndY.Width = 85;
            // 
            // EndZ
            // 
            this.EndZ.HeaderText = "终点Z";
            this.EndZ.Name = "EndZ";
            this.EndZ.ReadOnly = true;
            this.EndZ.Width = 85;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(951, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 30);
            this.button2.TabIndex = 6;
            this.button2.Text = "三维轨迹";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(741, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 30);
            this.button3.TabIndex = 7;
            this.button3.Text = "导入轨迹点";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cmsection
            // 
            this.cmsection.FormattingEnabled = true;
            this.cmsection.Location = new System.Drawing.Point(641, 10);
            this.cmsection.Name = "cmsection";
            this.cmsection.Size = new System.Drawing.Size(74, 20);
            this.cmsection.TabIndex = 8;
            // 
            // ForceCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1055, 538);
            this.Controls.Add(this.cmsection);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ForceCalculation";
            this.Text = "受力计算";
            this.Load += new System.EventHandler(this.ForceCalculation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Section;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lengths;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuryType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ForceValue1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ForceValue2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IfQualify;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProposedProg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Material;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartX;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartY;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndX;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndY;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndZ;
        private System.Windows.Forms.ComboBox cmsection;
    }
}