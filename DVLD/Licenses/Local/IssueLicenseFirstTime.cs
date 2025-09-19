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
    public partial class IssueLicenseFirstTime : Form
    {
        string NationalNo { get; set; }

        public IssueLicenseFirstTime(int LDLAppID,string nationalNo)
        {
            InitializeComponent();

            ai.DLAppID = LDLAppID;


            this.NationalNo = nationalNo;
        }

        private void IssueLicenseFirstTime_Load(object sender, EventArgs e)
        {

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            DataTable person = DVLDBusinessLayer.clsManagePeople.GetPerson(NationalNo);

            int personID = Convert.ToInt32(person.Rows[0]["PersonID"]);
            int createdByUserID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["userID"]);


            if (!DVLDBusinessLayer.clsDriversAndLicenses.isPersonADriver(personID))
            {

                DVLDBusinessLayer.clsDriversAndLicenses.addNewDriver(personID, createdByUserID);
            }

            int DriverID=DVLDBusinessLayer.clsDriversAndLicenses.getDriverID(personID);

           DataTable license= DVLDBusinessLayer.clsManageApplication.getLicenseClassByName(ai.GetLicense());

           int LicenseID=Convert.ToInt32(license.Rows[0]["LicenseClassID"]);

            int applicationID=Convert.ToInt32(ai.GetAppID());

            DateTime IssueDate=DateTime.Now;

            int validatyLength= Convert.ToInt32(license.Rows[0]["DefaultValidityLength"]);

            DateTime ExpirationDate= IssueDate.AddYears(validatyLength);

            string notes = tbNotes.Text;

            double paidFees= Convert.ToInt32(ai.GetFees());

            bool isActive = true;

            int IssueReason = 1;



            if(DVLDBusinessLayer.clsDriversAndLicenses.IssueLicense(applicationID,
                DriverID,LicenseID,IssueDate,ExpirationDate,notes,paidFees,
                isActive,IssueReason,createdByUserID))
            {
                MessageBox.Show(
                "The license has been issued successfully",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

                DVLDBusinessLayer.clsManageApplication.UpdateApplicationStatus(applicationID, 3);

            }
            else
            {
                MessageBox.Show("There is an error in Issuing the License",
                    "error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


        }
    }
}
