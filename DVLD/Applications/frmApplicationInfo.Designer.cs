namespace DVLD
{
    partial class frmApplicationInfo
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
            this.ai = new DVLD.ApplicationInfo();
            this.SuspendLayout();
            // 
            // ai
            // 
            this.ai.DLAppID = 0;
            this.ai.Location = new System.Drawing.Point(3, 1);
            this.ai.Name = "ai";
            this.ai.Size = new System.Drawing.Size(878, 440);
            this.ai.TabIndex = 0;
            this.ai.Load += new System.EventHandler(this.applicationInfo1_Load);
            // 
            // frmApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 426);
            this.Controls.Add(this.ai);
            this.Name = "frmApplicationInfo";
            this.Text = "frmApplicationInfo";
            this.Load += new System.EventHandler(this.frmApplicationInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ApplicationInfo ai;
    }
}