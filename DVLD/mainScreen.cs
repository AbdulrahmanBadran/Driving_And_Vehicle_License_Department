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
    public partial class mainScreen : Form
    {
        
        public bool signOut {  get; set; }
        public mainScreen()
        {
            InitializeComponent();

            while (!showLoginScreen()) 
            {

            }




            signOut = false;
        }

        private bool showLoginScreen()
        {
            frmLogin form = new frmLogin();

            form.sendUser += getTheUser;

         

            return form.ShowDialog()==DialogResult.OK;
            
               
        }

        public void getTheUser(object sender, DataTable user)
            {

            GlobalSettings.CurrentUser = user;

        }


        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new ManagePeopleScreen();

            form.ShowDialog();


        }

        private void mainScreen_Load(object sender, EventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new UserManagement();
            frm.ShowDialog();
        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["userID"]);

            Form frm = new frmShowUserInformation(userID);
            frm.ShowDialog();   
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["userID"]);

            Form frm = new frmChangePassword(userID);
            frm.ShowDialog();
        }

        private void signedOut()
        {
            signOut = true;
        }

        private void sighnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings.CurrentUser = null;
            signedOut();
            this.Close();

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageApplicationType();
            frm.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmNewLocalDrivingLicense();
                frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmLocalDrivingLicenseApplications();
            form.ShowDialog();
        }

        private void internationalLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmInternationalLicenseMAnagement();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmDrivers();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void reewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmRenewLicenseApplication();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReplacementForDamagedOrLostLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmDetainedLicenseManagement();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmLocalDrivingLicenseApplications();
            form.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }
    }
}
