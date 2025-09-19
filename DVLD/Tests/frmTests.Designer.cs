namespace DVLD
{
    partial class frmTests
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
            this.lbVision = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTests = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.applicationInfo1 = new DVLD.ApplicationInfo();
            this.lbWritten = new System.Windows.Forms.Label();
            this.lbStreet = new System.Windows.Forms.Label();
            this.pbStreet = new System.Windows.Forms.PictureBox();
            this.pbWritten = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewTestAppointment = new System.Windows.Forms.Button();
            this.pbVision = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTests)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStreet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWritten)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVision)).BeginInit();
            this.SuspendLayout();
            // 
            // lbVision
            // 
            this.lbVision.AutoSize = true;
            this.lbVision.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVision.ForeColor = System.Drawing.Color.Firebrick;
            this.lbVision.Location = new System.Drawing.Point(205, 156);
            this.lbVision.Name = "lbVision";
            this.lbVision.Size = new System.Drawing.Size(465, 42);
            this.lbVision.TabIndex = 19;
            this.lbVision.Text = "Vision Test Appointments";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 611);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 25);
            this.label1.TabIndex = 21;
            this.label1.Text = "Appointments:";
            // 
            // dgvTests
            // 
            this.dgvTests.AllowUserToAddRows = false;
            this.dgvTests.AllowUserToDeleteRows = false;
            this.dgvTests.AllowUserToOrderColumns = true;
            this.dgvTests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTests.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvTests.Location = new System.Drawing.Point(8, 652);
            this.dgvTests.Name = "dgvTests";
            this.dgvTests.ReadOnly = true;
            this.dgvTests.RowHeadersWidth = 51;
            this.dgvTests.RowTemplate.Height = 24;
            this.dgvTests.Size = new System.Drawing.Size(865, 223);
            this.dgvTests.TabIndex = 24;
            this.dgvTests.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTests_CellContentClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(142, 56);
            // 
            // applicationInfo1
            // 
            this.applicationInfo1.DLAppID = 0;
            this.applicationInfo1.Location = new System.Drawing.Point(8, 197);
            this.applicationInfo1.Name = "applicationInfo1";
            this.applicationInfo1.Size = new System.Drawing.Size(888, 379);
            this.applicationInfo1.TabIndex = 0;
            this.applicationInfo1.Load += new System.EventHandler(this.applicationInfo1_Load);
            // 
            // lbWritten
            // 
            this.lbWritten.AutoSize = true;
            this.lbWritten.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWritten.ForeColor = System.Drawing.Color.Firebrick;
            this.lbWritten.Location = new System.Drawing.Point(205, 156);
            this.lbWritten.Name = "lbWritten";
            this.lbWritten.Size = new System.Drawing.Size(481, 42);
            this.lbWritten.TabIndex = 26;
            this.lbWritten.Text = "Written Test Appointments";
            // 
            // lbStreet
            // 
            this.lbStreet.AutoSize = true;
            this.lbStreet.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStreet.ForeColor = System.Drawing.Color.Firebrick;
            this.lbStreet.Location = new System.Drawing.Point(208, 156);
            this.lbStreet.Name = "lbStreet";
            this.lbStreet.Size = new System.Drawing.Size(462, 42);
            this.lbStreet.TabIndex = 28;
            this.lbStreet.Text = "Street Test Appointments";
            // 
            // pbStreet
            // 
            this.pbStreet.Image = global::DVLD.Properties.Resources.driving_test_512;
            this.pbStreet.Location = new System.Drawing.Point(339, 22);
            this.pbStreet.Name = "pbStreet";
            this.pbStreet.Size = new System.Drawing.Size(212, 122);
            this.pbStreet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbStreet.TabIndex = 29;
            this.pbStreet.TabStop = false;
            this.pbStreet.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pbWritten
            // 
            this.pbWritten.Image = global::DVLD.Properties.Resources.Written_Test_512;
            this.pbWritten.Location = new System.Drawing.Point(339, 22);
            this.pbWritten.Name = "pbWritten";
            this.pbWritten.Size = new System.Drawing.Size(212, 122);
            this.pbWritten.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbWritten.TabIndex = 27;
            this.pbWritten.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(714, 881);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(163, 54);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::DVLD.Properties.Resources.edit_32;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Test_32;
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // btnNewTestAppointment
            // 
            this.btnNewTestAppointment.Image = global::DVLD.Properties.Resources.New_Application_64;
            this.btnNewTestAppointment.Location = new System.Drawing.Point(778, 567);
            this.btnNewTestAppointment.Name = "btnNewTestAppointment";
            this.btnNewTestAppointment.Size = new System.Drawing.Size(99, 79);
            this.btnNewTestAppointment.TabIndex = 22;
            this.btnNewTestAppointment.UseVisualStyleBackColor = true;
            this.btnNewTestAppointment.Click += new System.EventHandler(this.btnNewTestAppointment_Click);
            // 
            // pbVision
            // 
            this.pbVision.Image = global::DVLD.Properties.Resources.Vision_512;
            this.pbVision.Location = new System.Drawing.Point(339, 22);
            this.pbVision.Name = "pbVision";
            this.pbVision.Size = new System.Drawing.Size(212, 122);
            this.pbVision.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbVision.TabIndex = 20;
            this.pbVision.TabStop = false;
            // 
            // frmVisionTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 939);
            this.Controls.Add(this.pbStreet);
            this.Controls.Add(this.lbStreet);
            this.Controls.Add(this.pbWritten);
            this.Controls.Add(this.lbWritten);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvTests);
            this.Controls.Add(this.btnNewTestAppointment);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbVision);
            this.Controls.Add(this.lbVision);
            this.Controls.Add(this.applicationInfo1);
            this.Name = "frmVisionTest";
            this.Text = "frmVisionTest";
            this.Load += new System.EventHandler(this.frmVisionTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTests)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbStreet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWritten)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVision)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ApplicationInfo applicationInfo1;
        private System.Windows.Forms.Label lbVision;
        private System.Windows.Forms.PictureBox pbVision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNewTestAppointment;
        private System.Windows.Forms.DataGridView dgvTests;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbWritten;
        private System.Windows.Forms.Label lbWritten;
        private System.Windows.Forms.PictureBox pbStreet;
        private System.Windows.Forms.Label lbStreet;      
    }
}