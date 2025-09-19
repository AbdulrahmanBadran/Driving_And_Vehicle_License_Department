namespace DVLD
{
    partial class showPersonInfo
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
            this.personInformationCard1 = new DVLD.personInformationCard();
            this.SuspendLayout();
            // 
            // personInformationCard1
            // 
            this.personInformationCard1.ID = 0;
            this.personInformationCard1.Location = new System.Drawing.Point(79, 77);
            this.personInformationCard1.Name = "personInformationCard1";
            this.personInformationCard1.Size = new System.Drawing.Size(874, 310);
            this.personInformationCard1.TabIndex = 0;
            // 
            // showPersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 399);
            this.Controls.Add(this.personInformationCard1);
            this.Name = "showPersonInfo";
            this.Text = "showPersonInfo";
            this.Load += new System.EventHandler(this.showPersonInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private personInformationCard personInformationCard1;
    }
}