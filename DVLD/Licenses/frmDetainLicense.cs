using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
            basicControllFilling();
        }

        private void basicControllFilling()
        {

            lbDetainDate.Text = DateTime.Now.ToShortDateString();
            lbCreatedBy.Text = Convert.ToString(GlobalSettings.CurrentUser.Rows[0]["UserName"]);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tbFilter.Text))
            {
                int OldlicenseID = Convert.ToInt32(tbFilter.Text);
                int DLAppID = DVLDBusinessLayer.clsDriversAndLicenses.retreiveLDLAppID(OldlicenseID);




                if (!DVLDBusinessLayer.clsDriversAndLicenses.isLicenseExist(OldlicenseID))
                {
                    MessageBox.Show($"There is no License with license ID={OldlicenseID}", "License Does Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LI.LDLAppID = DLAppID;
                LI.Search();

                llLicenseHistory.Enabled = true;

                int licenseID = Convert.ToInt32(tbFilter.Text);

                if (DVLDBusinessLayer.clsDriversAndLicenses.isLicenseDetained(licenseID))
                {
                    MessageBox.Show("The License is already detained!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;

                }

                if (!DVLDBusinessLayer.clsDriversAndLicenses.isLicenseActive(OldlicenseID))
                {
                    MessageBox.Show($"This License Is Enactive!", "License Enactive", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                

                btnDetain.Enabled = true;
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            int licenseID = Convert.ToInt32(tbFilter.Text);

            


            if (!Double.TryParse(tbFineFees.Text, out double fees) || fees < 0)
            {
                MessageBox.Show("The fees you enterd is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (MessageBox.Show("Are You Sure You want to Detain This License?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {

                return;

            }

           


           
            int DriverID = Convert.ToInt32(LI.GetDriverID());

            int personID = DVLDBusinessLayer.clsManagePeople.retreivePersonID(DriverID);
           
            int UserID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["UserID"]);

            DateTime detainDate= DateTime.Now;

            

            if(DVLDBusinessLayer.clsDriversAndLicenses.detainLicense(licenseID,
                detainDate,fees,UserID))
            {
                MessageBox.Show("License Detained!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                MessageBox.Show("error in detaining this License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            lbDeatinID.Text = DVLDBusinessLayer.clsDriversAndLicenses.retrieveDetainID(licenseID).ToString();
            lbLicenseID.Text = licenseID.ToString();

            llLicenseInfo.Enabled = true;

            btnDetain.Enabled = false;
        }

        private void llLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LDLAppID = LI.LDLAppID;
            string nationalNo = LI.GetNationalNo();

            Form frm = new frmLicenseHistory(LDLAppID, nationalNo);
            frm.ShowDialog();
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LDLAppID = DVLDBusinessLayer.clsManageApplication.getLastLocalDrivingLicenseApplicationID();

            Form frm = new frmShowLicenseInfo(LDLAppID);
            frm.ShowDialog();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
