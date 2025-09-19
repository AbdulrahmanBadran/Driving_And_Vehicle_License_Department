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
    public partial class frmRenewLicenseApplication : Form
    {
       

        public frmRenewLicenseApplication()
        {
            InitializeComponent();
             basicControllFilling();
        }
        public frmRenewLicenseApplication(int licenseID)
        {
            InitializeComponent();
            tbFilter.Text = licenseID.ToString().Trim();
            tbFilter.Enabled = false;
            basicControllFilling();
        }

        private void InitialControlFilling(int OldlicenseID, int DLAppID)
        {
            DataTable OldLicense = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveLicense(OldlicenseID);

            lbIssueDate.Text = Convert.ToDateTime(OldLicense.Rows[0]["IssueDate"]).ToShortDateString();
            lbExpDate.Text = Convert.ToDateTime(OldLicense.Rows[0]["ExpirationDate"]).ToShortDateString();
            lbOldLicenseID.Text = Convert.ToString(OldLicense.Rows[0]["LicenseID"]);

            int LicenseClassID = Convert.ToInt32(OldLicense.Rows[0]["LicenseClass"]);

            lbLicenseFees.Text = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveLicenseClassFees(LicenseClassID).ToString();

            lbTotalFees.Text = Convert.ToString(Convert.ToInt32(lbAppFees.Text) + Convert.ToInt32(lbLicenseFees.Text));


            LI.LDLAppID = DLAppID;
            LI.Search();
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

                InitialControlFilling(OldlicenseID, DLAppID);
                llLicenseHistory.Enabled = true;

                if (!DVLDBusinessLayer.clsDriversAndLicenses.isLicenseActive(OldlicenseID))
                {
                    MessageBox.Show($"This License Is Enactive!", "License Enactive", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!DVLDBusinessLayer.clsDriversAndLicenses.isLicenseExpired(OldlicenseID))
                {
                    MessageBox.Show($"This License Has Not Expired Yet!", "License Not Expierd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                 btnRenew.Enabled = true;
            }
        }


        private void basicControllFilling()
        {

            lbAppDate.Text = DateTime.Now.ToShortDateString();
            lbAppFees.Text = DVLDBusinessLayer.clsManageApplication.RetrieveApplicationFees(2).ToString();
            lbCreatedBy.Text = Convert.ToString(GlobalSettings.CurrentUser.Rows[0]["UserName"]);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void frmRenewLicenseApplication_Load(object sender, EventArgs e)
        {

        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You want to renew The License?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.No)
            {

                return;

            }


            int OldlicenseID = Convert.ToInt32(tbFilter.Text);
            int DriverID = Convert.ToInt32(LI.GetDriverID());

            int personID = DVLDBusinessLayer.clsManagePeople.retreivePersonID(DriverID);
            double applicationFees = Convert.ToDouble(lbAppFees.Text);
            int UserID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["UserID"]);

            if (DVLDBusinessLayer.clsManageApplication.AddNewApplication(personID, applicationFees, UserID, 2))
            {
                int applicationID = DVLDBusinessLayer.clsManageApplication.getLastApplicationID();

                if (DVLDBusinessLayer.clsDriversAndLicenses.renewLicense(OldlicenseID, applicationID))
                {
                    int LDLAppID = DVLDBusinessLayer.clsManageApplication.getLastLocalDrivingLicenseApplicationID();

                    int licenseID = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveLicenseID(LDLAppID);

                    MessageBox.Show($"License Renewed Successfully With ID={licenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lbRLAppID.Text = applicationID.ToString();
                    lbRenewdLicenseID.Text= licenseID.ToString();
                   
                }
                else
                {
                    MessageBox.Show($"Something went wrong in renewing The License", "License Issuing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show($"Something went wrong", "License Issuing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            btnRenew.Enabled= false;
        }

        private void llLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LDLAppID = LI.LDLAppID;
            string nationalNo=LI.GetNationalNo();

            Form frm = new frmLicenseHistory(LDLAppID,nationalNo);
            frm.ShowDialog();
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LDLAppID = DVLDBusinessLayer.clsManageApplication.getLastLocalDrivingLicenseApplicationID();

            Form frm = new frmShowLicenseInfo(LDLAppID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
