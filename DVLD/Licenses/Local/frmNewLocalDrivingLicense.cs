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
    public partial class frmNewLocalDrivingLicense : Form
    {
        public frmNewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void fillCbLicenseClasses()
        {
            DataTable Items = DVLDBusinessLayer.clsManageApplication.getAllLicenseClasses();
            foreach (DataRow row in Items.Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"].ToString());
            }

        }

        private void fillApplicationFees()
        {
           
            string name= "New Local Driving License Service";
            DataTable Item = DVLDBusinessLayer.clsManageApplication.getApplicationType(name);

            lbFees.Text= Item.Rows[0]["ApplicationFees"].ToString();

        }

        private void fillApplicationInfo()
        {

            lbDate.Text= DateTime.Now.ToString("dd/MM/yyyy");
            fillApplicationFees();
            fillCbLicenseClasses();
            lbCreatedBy.Text= GlobalSettings.CurrentUser.Rows[0]["UserName"].ToString();

        }

        private void frmNewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            fillApplicationInfo();
        }

        private void tpLoginInfo_Click(object sender, EventArgs e)
        {

        }

        private void cbLicenseClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbLicenseClasses.SelectedIndex!=-1){

                DataTable LicenseClass = DVLDBusinessLayer.clsManageApplication.getLicenseClassByName(cbLicenseClasses.SelectedItem.ToString());
              
            int minimumAllowedAge = Convert.ToInt32(LicenseClass.Rows[0]["MinimumAllowedAge"]);

            DateTime PersonBirthDate = Convert.ToDateTime(fpi.getDateOfBirth());

            int personAge = DVLDBusinessLayer.clsManagePeople.getPersonAge(PersonBirthDate);

            if (minimumAllowedAge > personAge){
                MessageBox.Show("the person is under age for this license!");

                cbLicenseClasses.SelectedIndex = -1;
                return;
            }
 }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!fpi.isFilled())
            {
                MessageBox.Show("Please fill in the required fields before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
                tabControl1.SelectTab(1);
        }

        private void fpi_Load(object sender, EventArgs e)
        {

        }

        private void btnNext_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void refreshLocalDrivingLicenseID()
        {
            MessageBox.Show("Application submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int localDrivingLicenseID = DVLDBusinessLayer.clsManageApplication.getLastLocalDrivingLicenseApplicationID();


            lbAppID.Text = localDrivingLicenseID.ToString();
        }


        private bool isSelectedLicenseClassNull()
        {
            if (cbLicenseClasses.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a license class.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if(!isSelectedLicenseClassNull())
            {
                return;
            }

            DataTable LicenseClass = DVLDBusinessLayer.clsManageApplication.getLicenseClassByName(cbLicenseClasses.SelectedItem.ToString());

            int licenseClassID = Convert.ToInt32(LicenseClass.Rows[0]["LicenseClassID"]);
            int personID = fpi.getID();

            if(DVLDBusinessLayer.clsDriversAndLicenses.isPersonADriver(personID))
            {
                int DriverID=DVLDBusinessLayer.clsDriversAndLicenses.getDriverID(personID);

               if(DVLDBusinessLayer.clsDriversAndLicenses.isDriverHasTheLicense(licenseClassID, DriverID))
                {
                    MessageBox.Show("This person has this License Already!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


            }


            if (DVLDBusinessLayer.clsManageApplication.isApplicationExist(personID, licenseClassID))
            {
                MessageBox.Show("This person already has an application for this license class.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double fees = Convert.ToDouble(lbFees.Text);
            int currentUserID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["UserID"]);



            if (DVLDBusinessLayer.clsManageApplication.AddNewApplication(personID, fees, currentUserID,1))
            {
               
                int applicationID = DVLDBusinessLayer.clsManageApplication.getLastApplicationID();

                if (DVLDBusinessLayer.clsManageApplication.addNewLocalDrivingLicenseApplication(applicationID, licenseClassID))
                {
                    refreshLocalDrivingLicenseID();
                }
                else
                {
                    MessageBox.Show("Failed to submit the local driving license application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Failed to create a new application 1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(!fpi.isFilled() && tabControl1.SelectedIndex==1)
            {
                MessageBox.Show("Please fill in the required fields before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectTab(0);
                e.Cancel = false;
            }
          
        }
    }
}
