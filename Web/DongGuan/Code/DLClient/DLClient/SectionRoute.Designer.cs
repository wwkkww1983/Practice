namespace DLClient
{
    partial class SectionRoute
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
            this.mapControl1 = new DLClient.MapControl();
            this.SuspendLayout();
            // 
            // mapControl1
            // 
            this.mapControl1._SectionId = null;
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl1.Location = new System.Drawing.Point(0, 0);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.Size = new System.Drawing.Size(1398, 687);
            this.mapControl1.TabIndex = 0;
            // 
            // SectionRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1398, 687);
            this.Controls.Add(this.mapControl1);
            this.Name = "SectionRoute";
            this.Text = "SectionRoute";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SectionRoute_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private MapControl mapControl1;
    }
}