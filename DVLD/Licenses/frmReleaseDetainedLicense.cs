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
    public partial class frmReleaseDetainedLicense : Form
    {
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
            
        }

        public frmReleaseDetainedLicense(int licenseID)
        {
            InitializeComponent();

            tbFilter.Text = licenseID.ToString().Trim();
            tbFilter.Enabled = false;
            SearchLicense();

        }

        private void ControllFilling(int licenseID)
        {

            DataTable DetainedInfo = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveDetainedLicenseInfo(licenseID);

            lbDetainID.Text = Convert.ToString(DetainedInfo.Rows[0]["DetainID"]);
            lbLicenseID.Text= Convert.ToString(DetainedInfo.Rows[0]["LicenseID"]);
            lbDetainDate.Text = Convert.ToDateTime(DetainedInfo.Rows[0]["DetainDate"]).ToShortDateString();
            lbCreatedBy.Text = Convert.ToString(DetainedInfo.Rows[0]["CreatedByUserID"]);
            lbAppFees.Text = DVLDBusinessLayer.clsManageApplication.RetrieveApplicationFees(5).ToString();
            lbFineFees.Text= Convert.ToString(DetainedInfo.Rows[0]["FineFees"]);
            lbTotalFees.Text = Convert.ToString(Convert.ToDouble(lbAppFees.Text) + Convert.ToDouble(lbFineFees.Text));


            lbDetainDate.Text = DateTime.Now.ToShortDateString();
            lbCreatedBy.Text = Convert.ToString(GlobalSettings.CurrentUser.Rows[0]["UserName"]);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchLicense();
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {

        }

        private void SearchLicense()
        {
            if (!String.IsNullOrWhiteSpace(tbFilter.Text))
            {
                int licenseID = Convert.ToInt32(tbFilter.Text);
                int DLAppID = DVLDBusinessLayer.clsDriversAndLicenses.retreiveLDLAppID(licenseID);

                if (!DVLDBusinessLayer.clsDriversAndLicenses.isLicenseExist(licenseID))
                {
                    MessageBox.Show($"There is no License with license ID={licenseID}", "License Does Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LI.LDLAppID = DLAppID;
                LI.Search();

                llLicenseHistory.Enabled = true;

                if (!DVLDBusinessLayer.clsDriversAndLicenses.isLicenseDetained(licenseID))
                {
                    MessageBox.Show($"There is no License detained with ID={licenseID}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;

                }

                if (!DVLDBusinessLayer.clsDriversAndLicenses.isLicenseActive(licenseID))
                {
                    MessageBox.Show($"This License Is Enactive!", "License Enactive", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ControllFilling(licenseID);

                llLicenseHistory.Enabled = true;

                btnRelease.Enabled = true;
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You want to release The License?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {

                return;

            }


            int licenseID = Convert.ToInt32(tbFilter.Text);
            int DriverID = Convert.ToInt32(LI.GetDriverID());

            int personID = DVLDBusinessLayer.clsManagePeople.retreivePersonID(DriverID);
            double applicationFees = Convert.ToDouble(lbAppFees.Text);
            int UserID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["UserID"]);

            if (DVLDBusinessLayer.clsManageApplication.AddNewApplication(personID, applicationFees, UserID, 5))
            {
                int applicationID = DVLDBusinessLayer.clsManageApplication.getLastApplicationID();
                DateTime releaseDate= DateTime.Now;
                int releaseByUserID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["UserID"]);

                if (DVLDBusinessLayer.clsDriversAndLicenses.releaseLicense(licenseID,releaseDate
                    , applicationID,releaseByUserID))
                {
                  

                    MessageBox.Show($"License Released Successfully ", "License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lbAppID.Text = applicationID.ToString();

                }
                else
                {
                    MessageBox.Show($"Something went wrong in releasing The License", "License Releasing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show($"Something went wrong", "License Issuing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            llLicenseInfo.Enabled = true;
            btnRelease.Enabled = false;


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
    }
}
