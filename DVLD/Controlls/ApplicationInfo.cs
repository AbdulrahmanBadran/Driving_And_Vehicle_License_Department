using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class ApplicationInfo : UserControl
    {

        public int DLAppID { get; set; }


        public ApplicationInfo()
        {
            InitializeComponent();


        }

        private void fillControlls()
        {
            DataTable Info = DVLDBusinessLayer.clsManageApplication.GetLocalDrivingLicenseApplication(DLAppID);

            if (Info.Rows.Count > 0)
            {


                int LicenseClassID = Convert.ToInt32(Info.Rows[0]["LicenseClassID"]);

                int appID = Convert.ToInt32(Info.Rows[0]["ApplicationID"]);

                DataTable Applications = DVLDBusinessLayer.clsManageApplication.getApplication(DLAppID);

                int status = Convert.ToInt32(Applications.Rows[0]["ApplicationStatus"]);

                double fees = Convert.ToDouble(Applications.Rows[0]["PaidFees"]);

                DataTable AppTypes = DVLDBusinessLayer.clsManageApplication.getApplicationType(appID);

                string AppType = Convert.ToString(AppTypes.Rows[0]["ApplicationTypeTitle"]);

                int personID = Convert.ToInt32(Applications.Rows[0]["ApplicantPersonID"]);

                string applicantName = DVLDBusinessLayer.clsManagePeople.GetPersonFullName(personID);

                string AppDate = Convert.ToDateTime(Applications.Rows[0]["ApplicationDate"]).ToShortDateString();

                int userID = Convert.ToInt32(Applications.Rows[0]["CreatedByUserID"]);

                string userName = DVLDBusinessLayer.clsManageUsers.retrieveUser(userID).Rows[0]["UserName"].ToString();


                lbDLAppID.Text = Convert.ToString(DLAppID);

                lbLicense.Text = DVLDBusinessLayer.clsManageApplication.getLicenseClassByID(LicenseClassID).Rows[0]["ClassName"].ToString();

                fillPassedTest();

                lbAppID.Text = Convert.ToString(appID);

                fillApplicatosStatus(status);

                lbFees.Text = Convert.ToString(fees);

                lbType.Text = AppType;

                lbApplicant.Text = applicantName;

                lbDate.Text = AppDate;

                lbStatusDate.Text = AppDate;

                lbCreatedBy.Text = userName;

            }
        }

        private void fillApplicatosStatus(int status)
        {
            if (status == 1)
                lbStatus.Text = "New";
            if (status == 2)
                lbStatus.Text = "Cancelled";
            if (status == 3)
                lbStatus.Text = "Completed";

        }

        private void fillPassedTest()
        {

            int passedTest = DVLDBusinessLayer.clsManageApplication.passedTests(DLAppID);

            if (passedTest == 0)
            {
                lbPassedTest.Text = "0/3";
            }
            else if (passedTest == 1)
            {
                lbPassedTest.Text = "1/3";
            }
            else if (passedTest == 2)
            {
                lbPassedTest.Text = "2/3";
            }
            else if (passedTest == 3)
            {
                lbPassedTest.Text = "3/3";
            }


        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ApplicationInfo_Load(object sender, EventArgs e)
        {

            fillControlls();

        }

        private void lbPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            DataTable Applications = DVLDBusinessLayer.clsManageApplication.getApplication(DLAppID);
            int personID = Convert.ToInt32(Applications.Rows[0]["ApplicantPersonID"]);


            Form frm = new showPersonInfo(personID);
            frm.ShowDialog();
        }

        
        public string GetDLAppID()
        {
            return lbDLAppID?.Text ?? string.Empty;
        }

       
        public string GetLicense()
        {
            return lbLicense?.Text ?? string.Empty;
        }

        
        public string GetAppID()
        {
            return lbAppID?.Text ?? string.Empty;
        }

       
        public string GetFees()
        {
            return lbFees?.Text ?? string.Empty;
        }

        
        public string GetAppType()
        {
            return lbType?.Text ?? string.Empty;
        }

        
        public string GetApplicantName()
        {
            return lbApplicant?.Text ?? string.Empty;
        }

        
        public string GetAppDate()
        {
            return lbDate?.Text ?? string.Empty;
        }

       
        public string GetStatusDate()
        {
            return lbStatusDate?.Text ?? string.Empty;
        }

      
        public string GetCreatedBy()
        {
            return lbCreatedBy?.Text ?? string.Empty;
        }
        
        public void Search()
        {
            fillControlls();
        }
    }



}
