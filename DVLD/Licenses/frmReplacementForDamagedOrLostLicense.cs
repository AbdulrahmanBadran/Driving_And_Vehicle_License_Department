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
    public partial class frmReplacementForDamagedOrLostLicense : Form
    {
        public frmReplacementForDamagedOrLostLicense()
        {
            InitializeComponent();
            basicControllFilling();
            rdDamaged.Checked = true;
        }

        private void basicControllFilling()
        {

            lbAppDate.Text = DateTime.Now.ToShortDateString();
            lbCreatedBy.Text = Convert.ToString(GlobalSettings.CurrentUser.Rows[0]["UserName"]);
        }


        private void InitialControlFilling(int OldlicenseID, int DLAppID)
        {
            DataTable OldLicense = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveLicense(OldlicenseID);

            lbOldLicenseID.Text = Convert.ToString(OldLicense.Rows[0]["LicenseID"]);

            int LicenseClassID = Convert.ToInt32(OldLicense.Rows[0]["LicenseClass"]);

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

               

                btnReplacement.Enabled = true;
            }
        }

        private void rdDamaged_CheckedChanged(object sender, EventArgs e)
        {
            
                lbAppFees.Text = DVLDBusinessLayer.clsManageApplication.RetrieveApplicationFees(4).ToString();
            
        }

        private void rdLost_CheckedChanged(object sender, EventArgs e)
        {
          
                lbAppFees.Text = DVLDBusinessLayer.clsManageApplication.RetrieveApplicationFees(3).ToString();
        }

        private void frmReplacementForDamagedOrLostLicense_Load(object sender, EventArgs e)
        {

        }

        private void llLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LDLAppID = LI.LDLAppID;
            string nationalNo = LI.GetNationalNo();

            Form frm = new frmLicenseHistory(LDLAppID, nationalNo);
            frm.ShowDialog();
        }

        private void btnReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You want to Replace The License?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {

                return;

            }


            int OldlicenseID = Convert.ToInt32(tbFilter.Text);
            int DriverID = Convert.ToInt32(LI.GetDriverID());

            int personID = DVLDBusinessLayer.clsManagePeople.retreivePersonID(DriverID);
            double applicationFees = Convert.ToDouble(lbAppFees.Text);
            int UserID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["UserID"]);

            int AppTypeID;
            if(rdDamaged.Checked)
                AppTypeID = 4;
            else
                AppTypeID = 3;

            if (DVLDBusinessLayer.clsManageApplication.AddNewApplication(personID, applicationFees, UserID, AppTypeID))
            {
                int applicationID = DVLDBusinessLayer.clsManageApplication.getLastApplicationID();
               
                if (DVLDBusinessLayer.clsDriversAndLicenses.ReplaceLicense(OldlicenseID, applicationID,AppTypeID))
                {
                    int LDLAppID = DVLDBusinessLayer.clsManageApplication.getLastLocalDrivingLicenseApplicationID();

                    int licenseID = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveLicenseID(LDLAppID);

                    MessageBox.Show($"License Replaced Successfully With ID={licenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lbRLAppID.Text = applicationID.ToString();
                    lbReplacedLicenseID.Text = licenseID.ToString();
                    llLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show($"Something went wrong in replacing The License", "License Issuing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show($"Something went wrong", "License Issuing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            btnReplacement.Enabled = false;
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LDLAppID = DVLDBusinessLayer.clsManageApplication.getLastLocalDrivingLicenseApplicationID();

            Form frm = new frmShowLicenseInfo(LDLAppID);
            frm.ShowDialog();
        }
    }
}
