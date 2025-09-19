using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class LicenseInfo : UserControl
    {
        public DataTable DriverLicenseInfo { get; set; }

        public int LDLAppID {  get; set; }

        public LicenseInfo()
        {
            InitializeComponent();

           

        }

        private void fillingTheControls()
        {

            if (LDLAppID != 0 )
            {
                DriverLicenseInfo = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveDriverLicenseInfo(LDLAppID);
            }

            if (DriverLicenseInfo != null && LDLAppID != -1 && LDLAppID != 0)
            {

                lbClass.Text = Convert.ToString(DriverLicenseInfo.Rows[0]["ClassName"]);
                lbName.Text = Convert.ToString(DriverLicenseInfo.Rows[0]["FullName"]);
                lbLicenseID.Text = Convert.ToString(DriverLicenseInfo.Rows[0]["LicenseID"]);
                lbNationalNo.Text = Convert.ToString(DriverLicenseInfo.Rows[0]["NationalNo"]);
                if (Convert.ToInt32(DriverLicenseInfo.Rows[0]["Gendor"]) == 0)
                    lbGendor.Text = "Male";
                else
                    lbGendor.Text = "Female";

                lbIssueDate.Text = Convert.ToString(Convert.ToDateTime(DriverLicenseInfo.Rows[0]["IssueDate"]).ToShortDateString());

                int Reason = Convert.ToInt32(DriverLicenseInfo.Rows[0]["IssueReason"]);

                switch (Reason)
                {
                    case 1:
                        lbIssueReason.Text = "First Time";
                        break;

                    case 2:
                        lbIssueReason.Text = "Renew";
                        break;

                    case 3:
                        lbIssueReason.Text = "Replacement for Damaged";
                        break;

                    case 4:
                        lbIssueReason.Text = " Replacement for Lost";
                        break;

                }

                if (Convert.ToString(DriverLicenseInfo.Rows[0]["Notes"]) == "")
                    lbNotes.Text = "No Notes";
                else
                    lbNotes.Text = Convert.ToString(DriverLicenseInfo.Rows[0]["Notes"]);

                if (Convert.ToInt32(DriverLicenseInfo.Rows[0]["IsActive"]) == 1)
                    lbIsActive.Text = "Yes";
                else
                    lbIsActive.Text = "No";

                lbBirth.Text = Convert.ToString(Convert.ToDateTime(DriverLicenseInfo.Rows[0]["DateOfBirth"]).ToShortDateString());

                lbDriverID.Text = Convert.ToString(DriverLicenseInfo.Rows[0]["DriverID"]);
                lbExpirationDate.Text = Convert.ToString(Convert.ToDateTime(DriverLicenseInfo.Rows[0]["ExpirationDate"]).ToShortDateString());

                int licenseID = Convert.ToInt32(DriverLicenseInfo.Rows[0]["LicenseID"]);

                if (DVLDBusinessLayer.clsDriversAndLicenses.isLicenseDetained(licenseID))
                    lbIsDetained.Text = "Yes";
                else
                    lbIsDetained.Text = "No";


                string picPath = "D:\\Dvld-profile-Pic\\" + Convert.ToString(DriverLicenseInfo.Rows[0]["ImagePath"]);

                if (File.Exists(picPath))
                {
                    pb.Image = Image.FromFile(picPath);

                }
                else
                    pb.Image = lbGendor.Text == "male" ? Resources.Male_512 : Resources.Female_512;

            }




        }

        private void LicenseInfo_Load(object sender, EventArgs e)
        {
            fillingTheControls();
        }


        public void Search()
        {
            fillingTheControls();
        }

        public string GetClassName()
        {
            return lbClass?.Text ?? string.Empty;
        }

        public string GetFullName()
        {
            return lbName?.Text ?? string.Empty;
        }

        public string GetLicenseID()
        {
            return lbLicenseID?.Text ?? string.Empty;
        }

        public string GetNationalNo()
        {
            return lbNationalNo?.Text ?? string.Empty;
        }

        public string GetGender()
        {
            return lbGendor?.Text ?? string.Empty;
        }

        public string GetIssueDate()
        {
            return lbIssueDate?.Text ?? string.Empty;
        }

        public string GetIssueReason()
        {
            return lbIssueReason?.Text ?? string.Empty;
        }

        public string GetNotes()
        {
            return lbNotes?.Text ?? "No Notes";
        }

        public string GetIsActive()
        {
            return lbIsActive?.Text ?? string.Empty;
        }

        public string GetDateOfBirth()
        {
            return lbBirth?.Text ?? string.Empty;
        }

        public string GetDriverID()
        {
            return lbDriverID?.Text ?? string.Empty;
        }

        public string GetExpirationDate()
        {
            return lbExpirationDate?.Text ?? string.Empty;
        }

        public string GetIsDetained()
        {
            return lbIsDetained?.Text ?? string.Empty;
        }

        public Image GetProfilePicture()
        {
            return pb?.Image ?? (GetGender().ToLower() == "male" ? Resources.Male_512 : Resources.Female_512);
        }


    }

    


}
