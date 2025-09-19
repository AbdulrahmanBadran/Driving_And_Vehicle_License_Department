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
    public partial class InternationalLicenseInfo : UserControl
    {

        public int LDLAppID {  get; set; }

        public int DriverID {  get; set; }

       
        public InternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void InternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            fillingTheControl();
        }

        private void fillingTheControl()
        {



            if (DriverID != 0)
            {
                DataTable licenseInfo=DVLDBusinessLayer.clsDriversAndLicenses.RetrieveAllImportantInternationalLicenseInfo(DriverID);

                lbName.Text = Convert.ToString(licenseInfo.Rows[0]["FullName"]);
                lbLicenseID.Text = Convert.ToString(licenseInfo.Rows[0]["LicenseID"]);
                lbNationalNo.Text = Convert.ToString(licenseInfo.Rows[0]["NationalNo"]);

                int gendor= Convert.ToInt32(licenseInfo.Rows[0]["Gendor"]);

                if (gendor == 1)
                    lbGendor.Text = "Male";
                else
                    lbGendor.Text = "Female";

                lbIssueDate.Text = Convert.ToString(licenseInfo.Rows[0]["IssueDate"]);

                int isActive = Convert.ToInt32(licenseInfo.Rows[0]["IsActive"]);

                if (isActive == 1)
                    lbIsActive.Text = "Yes";
                else
                    lbIsActive.Text = "No";
                

                lbBirth.Text = Convert.ToString(licenseInfo.Rows[0]["DateOfBirth"]);
                lbDriverID.Text = Convert.ToString(licenseInfo.Rows[0]["DriverID"]);
                lbExpirationDate.Text = Convert.ToString(licenseInfo.Rows[0]["ExpirationDate"]);

                lbAppID.Text = Convert.ToString(licenseInfo.Rows[0]["ApplicationID"]);

                lbIntLicenseID.Text = Convert.ToString(licenseInfo.Rows[0]["InternationalLicenseID"]);

                string path= Convert.ToString(licenseInfo.Rows[0]["ImagePath"]);

                pb.Image = Image.FromFile(path);

            }





        }

    }
}
