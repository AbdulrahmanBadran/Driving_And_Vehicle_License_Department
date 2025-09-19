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
    public partial class frmChangePassword : Form
    {
        public int ID {  get; set; }

        DataTable User;

        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            User = DVLDBusinessLayer.clsManageUsers.retrieveUser(UserID);
            fillingControls();
            
        }

        private void fillingControls()
        {
           
            pic.ID = Convert.ToInt32(User.Rows[0]["PersonID"]);
 
            lbUserID.Text = User.Rows[0]["UserID"].ToString();
            lbUserName.Text = User.Rows[0]["userName"].ToString();
            if(Convert.ToInt32(User.Rows[0]["IsActive"])==1)
                lbIsActive.Text = "Yes";
            else
                lbIsActive.Text = "No";

        }



        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            pic.personInformationCard_Load(sender, e);
        }

        private void tbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if(String.IsNullOrWhiteSpace(tbCurrentPassword.Text) || tbCurrentPassword.Text != User.Rows[0]["Password"].ToString())
            {
                e.Cancel = true;
                errorProvider1.SetError(tbCurrentPassword, "empty or invalid password!");
            }
            else
            {

                errorProvider1.SetError(tbCurrentPassword, "");

            }
        }

        private void tbNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbNewPassword.Text) )
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNewPassword, "please enter a new password!");
            }
            else
            {

                errorProvider1.SetError(tbNewPassword, "");

            }
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbConfirmPassword.Text) || tbConfirmPassword.Text!=tbNewPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmPassword, "empty or wrong!");
            }
            else
            {

                errorProvider1.SetError(tbConfirmPassword, "");

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(User.Rows[0]["UserID"]);
            string passWord= tbNewPassword.Text;
            DVLDBusinessLayer.clsManageUsers.updateUserPassword(userID, passWord);
            MessageBox.Show("Password changed successfully");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

