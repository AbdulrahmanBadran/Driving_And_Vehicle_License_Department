namespace DVLD
{
    partial class AddUpdateScreen
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
            this.userCard1 = new DVLD.PersonCard();
            this.SuspendLayout();
            // 
            // userCard1
            // 
            this.userCard1.enMode = DVLD.PersonCard.eMode.enUpdate;
            this.userCard1.ID = 0;
            this.userCard1.Location = new System.Drawing.Point(146, 33);
            this.userCard1.Name = "userCard1";
            this.userCard1.NationalNo = null;
            this.userCard1.Size = new System.Drawing.Size(872, 523);
            this.userCard1.TabIndex = 0;
            this.userCard1.Load += new System.EventHandler(this.userCard1_Load_1);
            // 
            // AddUpdateScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 568);
            this.Controls.Add(this.userCard1);
            this.Name = "AddUpdateScreen";
            this.Text = "AddUpdateScreen";
            this.Load += new System.EventHandler(this.AddUpdateScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PersonCard userCard1;
    }
}