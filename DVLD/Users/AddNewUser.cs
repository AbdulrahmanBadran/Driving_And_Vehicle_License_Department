using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD
{
    public partial class AddNewUser : Form
    {
        int UserID;
        public enum eMode { enAdd = 0, enUpdate = 1 }
        private eMode enMode;
        
        public AddNewUser()
        {
            InitializeComponent();
            enMode = eMode.enAdd;

        }

        public AddNewUser(int UserID)
        {
            InitializeComponent();

            this.UserID = UserID;

            lbMode.Text = "Update User";

            enMode = eMode.enUpdate;

            prepareToEdit(UserID);


        }

        private void prepareToEdit(int UserID)
        {
           DataTable user = DVLDBusinessLayer.clsManageUsers.retrieveUser(UserID);
            if (user.Rows[0]["personID"] == DBNull.Value)
                return;


            int PersonID = Convert.ToInt32(user.Rows[0]["personID"]);

            lbUserID.Text = Convert.ToString(UserID); 

            pic.ID = PersonID;
            gbFilter.Enabled = false;

            tbUserName.Text = Convert.ToString( user.Rows[0]["UserName"]);

            tbPassword.Enabled = false;
            tbConfirmPassword.Enabled = false;

            tbPassword.Text = Convert.ToString(user.Rows[0]["Password"]);
            tbConfirmPassword.Text = Convert.ToString(user.Rows[0]["Password"]);

            if(Convert.ToInt32(user.Rows[0]["IsActive"])==1)
                cbIsActive.Checked = true;

        }

        private void AddNewUser_Load(object sender, EventArgs e)
        {

           

        }

        private void tpPersonalInfo_Click(object sender, EventArgs e)
        {

        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.SelectedItem.ToString()== "Person ID")
            {
                mtbFilter.Mask = "99999999";
                mtbFilter.ValidatingType = typeof(int);
            }
            else
            {
                mtbFilter.Mask = null;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(cmbFilter.SelectedItem != null && cmbFilter.SelectedItem.ToString() == "Person ID")
            {
                int ID;
                bool isNumber = int.TryParse(mtbFilter.Text, out ID);

                if (DVLDBusinessLayer.clsManagePeople.isPersonExist(ID))
                {
                    pic.ID = ID;
                    pic_Load(sender, e);
                }
                else{
                    MessageBox.Show($"person with ID: {ID} does not exist");
                    pic.returnToDefault();
                }

            }
            if (cmbFilter.SelectedItem != null && cmbFilter.SelectedItem.ToString() == "National No.")
            {

                if (DVLDBusinessLayer.clsManagePeople.isPersonExist(mtbFilter.Text))
                {
                    pic.nationalNo = mtbFilter.Text;
                    pic_Load(sender, e);
                }
                else
                {
                    MessageBox.Show($"person with Vationa lNo: {mtbFilter.Text} does not exist");
                    pic.returnToDefault();
                }


            }


        }

        private void pic_Load(object sender, EventArgs e)
        {
            pic.personInformationCard_Load(sender, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm= new AddUpdateScreen(-1);
            frm.ShowDialog();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbUserName.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(tbUserName, "user name is must to enter!");
            }
            if(DVLDBusinessLayer.clsManageUsers.isUserNameExist(tbUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbUserName, "this username is already exist,choose another one");
            }
            else
            {

                errorProvider1.SetError(tbUserName, "");

            }
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbPassword.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(tbPassword, "password is must to enter!");
            }
            else
            {

                errorProvider1.SetError(tbPassword, "");

            }
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbConfirmPassword.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(tbConfirmPassword, "please confirm your password!");
            }
            else if(tbConfirmPassword.Text!=tbPassword.Text)
            {
                errorProvider1.SetError(tbConfirmPassword, "this does not match your password!!");

            }
            else
            {

                errorProvider1.SetError(tbConfirmPassword, "");

            }

        }

        private bool add()
        {
            int personID;
            int.TryParse(pic.getID(), out personID);
            string username = tbUserName.Text;
            string password = tbPassword.Text;
            string confirmPassword = tbConfirmPassword.Text;
            short isActive = cbIsActive.Checked ? (short)1 : (short)0;

            if (DVLDBusinessLayer.clsManageUsers.AddNewUser(personID,
                 username, password, isActive))
                {
                MessageBox.Show("user Added Successfully");
                return true;
            }
            else
            {
                MessageBox.Show("there was error adding this user");
                return false;
            }
        }

        private void update()
        {
            int UserID=Convert.ToInt32(lbUserID.Text);
            string username = tbUserName.Text;
            string password = tbPassword.Text;
            short isActive = cbIsActive.Checked ? (short)1 : (short)0;

            if (DVLDBusinessLayer.clsManageUsers.updateUser(
                UserID, username, isActive))
                MessageBox.Show("user Updated Successfully");
        }


        private void save()
        {
            switch (enMode) 
            {
                case eMode.enAdd:
                    if(add())
                    enMode=eMode.enUpdate;
                    break;

                case eMode.enUpdate:
                    update();
                    break;

            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tpLoginInfo)
            {
                MessageBox.Show("please go to login info first");
                return;
            }

            save();
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            DataTable user = DVLDBusinessLayer.clsManageUsers.retrieveUser(UserID);

            if (enMode==eMode.enAdd && DVLDBusinessLayer.clsManageUsers.IsPersonExist(pic.ID))
            {
                MessageBox.Show("this person has a user account already!");
                return;

            }
            if (pic.Tag == "filled")
                tabControl1.SelectTab(1);
            else
                MessageBox.Show("please enter a person");
        }

       
    }
}
