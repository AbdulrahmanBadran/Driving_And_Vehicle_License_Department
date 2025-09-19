using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class frmInternationalLicenseApplication : Form
    {
        public frmInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmInternationalLicenseApplication_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
            if(!String.IsNullOrWhiteSpace(tbFilter.Text))
            {
                int licenseID=Convert.ToInt32(tbFilter.Text);
            int DLAppID= DVLDBusinessLayer.clsDriversAndLicenses.retreiveLDLAppID(licenseID);

                if (!DVLDBusinessLayer.clsDriversAndLicenses.isLicenseExist(licenseID))
                {
                    MessageBox.Show($"There is no License with license ID={licenseID}", "License Does Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                LI.LDLAppID = DLAppID;
                ai.DLAppID = DLAppID;




                LI.Search();
                ai.Search();

                int DriverID = Convert.ToInt32(LI.GetDriverID());
                int applicationID = Convert.ToInt32(ai.GetAppID());


                if (DVLDBusinessLayer.clsDriversAndLicenses.isDriverHasTheInternationalLicense(DriverID))
                {
                    int IntlicenseID = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveInternationalLicenseID(applicationID);

                    MessageBox.Show($"This Driver has Already An International License with ID={IntlicenseID}", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    llLicenseInfo.Enabled = true;
                    return;
                }


                DataTable licenseClass = DVLDBusinessLayer.clsManageApplication.getLicenseClassByID(3);

                string licenseClassName = Convert.ToString(licenseClass.Rows[0]["ClassName"]);

                if (ai.GetLicense() != licenseClassName)
                {
                    MessageBox.Show("The License Must be \"Class 3 - Ordinary driving license\"!", "Wrong License Class", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!DVLDBusinessLayer.clsDriversAndLicenses.isLicenseActive(licenseID))
                {

                    MessageBox.Show("The Licnese is Not Active", "activation error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (DVLDBusinessLayer.clsDriversAndLicenses.isLicenseExpired(licenseID))
                {

                    MessageBox.Show("The Licnese is Expired", "License Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;

                }

                if (DVLDBusinessLayer.clsDriversAndLicenses.isLicenseDetained(licenseID))
                {

                    MessageBox.Show("The Licnese is Detain", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;

                }

                btnIssue.Enabled = true;




            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(LI.GetDriverID());

           

            if (MessageBox.Show("Are You Sure You Want To Issue This License?", "confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int applicationID = Convert.ToInt32(ai.GetAppID());
            int LocalLicenseID = Convert.ToInt32(LI.GetLicenseID());
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = IssueDate.AddYears(1);
            bool isActive = true;
            int createdByUserID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["UserID"]);


           


           
            if (DVLDBusinessLayer.clsDriversAndLicenses.issueInterNationalLicense(applicationID,
                DriverID, LocalLicenseID, IssueDate, ExpirationDate, isActive, createdByUserID))
            {

                int LicenseID=DVLDBusinessLayer.clsDriversAndLicenses.RetrieveInternationalLicenseID(applicationID);

                MessageBox.Show($"International License Issued Successfully with ID={LicenseID}","License Issued",MessageBoxButtons.OK,MessageBoxIcon.Information);
                llLicenseInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show($"Error Issuing the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void llLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LDLAppID = Convert.ToInt32(ai.GetDLAppID());
            string nationalNo=LI.GetNationalNo();


            Form frm = new frmLicenseHistory(LDLAppID, nationalNo);
            frm.ShowDialog();
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            int DriverID=Convert.ToInt32(LI.GetDriverID());

            Form frm = new frmInternationalLicenseInfo( DriverID);
            frm.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LI_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
