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
    public partial class UserManagement : Form
    {
        DataView dvUsers;

        public UserManagement()
        {
            InitializeComponent();

           
            

        }

        private void tbFilter_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void UserManagement_Load(object sender, EventArgs e)
        {
            refreshUsers();
        }

        private void cmbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbFilters.SelectedItem.ToString() == "isActive")
            {
                mtbFilter.Visible = false;
                cbIsActive.Visible = true;
            }
        }


        private void refreshUsers()
        {
            dvUsers = DVLDBusinessLayer.clsManageUsers.retrieveUsers().DefaultView;
            dgvUsers.DataSource = dvUsers;

        }
        private void mtbFilter_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = cmbFilters.SelectedItem?.ToString() ?? "";

            switch (filterColumn)
            {

                case "Person ID":
                    mtbFilter.Mask = "99999999";
                    mtbFilter.ValidatingType = typeof(int);

                    if (string.IsNullOrEmpty(mtbFilter.Text))
                    {
                        dvUsers.RowFilter = "";
                    }

                    else if (int.TryParse(mtbFilter.Text, out int personId))
                        dvUsers.RowFilter = $"PersonID = {personId}";
                    else
                        dvUsers.RowFilter = "1=0";
                    break;

                case "UserID":
                    mtbFilter.Mask = "99999999";
                    mtbFilter.ValidatingType = typeof(int);

                    if (string.IsNullOrEmpty(mtbFilter.Text))
                    {
                        dvUsers.RowFilter = "";
                    }

                    else if (int.TryParse(mtbFilter.Text, out int UserID))
                        dvUsers.RowFilter = $"UserID = {UserID}";
                    else
                        dvUsers.RowFilter = "1=0";
                    break;
                case "UserName":

                    if (string.IsNullOrEmpty(mtbFilter.Text))
                    {
                        dvUsers.RowFilter = "";
                    }

                else
                    {
                        dvUsers.RowFilter = $"UserName LIKE '%{mtbFilter.Text}%'";
                    }
                    break;

                case "FullName":
                    if (string.IsNullOrEmpty(mtbFilter.Text))
                    {
                        dvUsers.RowFilter = "";
                    }
                    else
                    {
                        dvUsers.RowFilter = $"FullName LIKE '%{mtbFilter.Text}%'";
                    }
                    break;
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string isActive = cbIsActive.SelectedItem?.ToString() ?? "";

            switch (isActive)
            {
                case "All":
                    dvUsers.RowFilter = $"IsActive = 1 or IsActive=0";

                    break;

                case "Yes":
                    dvUsers.RowFilter = $"IsActive = 1";
                    break;

                case "No":
                    dvUsers.RowFilter = $"IsActive = 0";
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new AddNewUser();
            frm.ShowDialog();
            refreshUsers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = ((int)dgvUsers.CurrentRow.Cells["PersonID"].Value);

            Form form = new showPersonInfo(ID);
            form.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new AddNewUser();
            frm.ShowDialog();
            refreshUsers();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = ((int)dgvUsers.CurrentRow.Cells["UserID"].Value);
            Form frm = new AddNewUser(UserID);
            frm.ShowDialog();
            refreshUsers();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = ((int)dgvUsers.CurrentRow.Cells["UserID"].Value);

            if(MessageBox.Show("are you sure you want to delete this user?","confirmation",MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                DVLDBusinessLayer.clsManageUsers.deleteUser(UserID);
                MessageBox.Show("User deleted Successfully");
                refreshUsers();
            } 

            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = ((int)dgvUsers.CurrentRow.Cells["UserID"].Value);

            Form frm = new frmChangePassword(UserID);
            frm.ShowDialog(); 

        }
    }
}
