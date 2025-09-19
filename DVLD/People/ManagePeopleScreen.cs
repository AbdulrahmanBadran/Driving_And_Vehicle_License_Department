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
using DVLDBusinessLayer;
namespace DVLD
{
    public partial class ManagePeopleScreen : Form
    {

        DataView DVpeople;

        public ManagePeopleScreen()
        {
            InitializeComponent();

           cmbFilters.SelectedIndex = 0;

           
        }

        private void refreshPeople()
        {
            DVpeople = DVLDBusinessLayer.clsManagePeople.listPeople().DefaultView;

            dgvPeople.DataSource = DVpeople;
        }

        private void dgvPeople_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form form = new AddUpdateScreen(-1);
            form.ShowDialog();
            
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int ID = ((int)dgvPeople.CurrentRow.Cells["PersonID"].Value);

            Form form = new AddUpdateScreen(ID);
            form.ShowDialog();
        }

        private void showDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = ((int)dgvPeople.CurrentRow.Cells["PersonID"].Value);

            Form form = new showPersonInfo(ID);
            form.ShowDialog();
        }


        private void ManagePeopleScreen_Load(object sender, EventArgs e) 
        {
            refreshPeople();

            if (cmbFilters.SelectedItem != null && cmbFilters.SelectedItem.ToString() == "None")
                tbFilter.Visible = false;

           

        }

        private void cmbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilters.SelectedItem != null && cmbFilters.SelectedItem.ToString() == "None")
                tbFilter.Visible = false;
            else
                tbFilter.Visible = true;

            
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = cmbFilters.SelectedItem?.ToString() ?? "";

            switch (filterColumn)
            {
                case "Person ID":

                    if (string.IsNullOrEmpty(tbFilter.Text))
                    {
                        DVpeople.RowFilter = ""; 
                    }

                   else if (int.TryParse(tbFilter.Text, out int personId))
                        DVpeople.RowFilter = $"PersonID = {personId}";
                    else
                        DVpeople.RowFilter = "1=0";
                    break;

                case "National No.":
                    DVpeople.RowFilter = $"NationalNo LIKE '%{tbFilter.Text}%'";
                    break;

                case "First Name":
                    DVpeople.RowFilter = $"FirstName LIKE '%{tbFilter.Text}%'";
                    break;

                case "Second Name":
                    DVpeople.RowFilter = $"SecondName LIKE '%{tbFilter.Text}%'";
                    break;

                case "Third Name":
                    DVpeople.RowFilter = $"ThirdName LIKE '%{tbFilter.Text}%'";
                    break;

                case "Last Name":
                    DVpeople.RowFilter = $"LastName LIKE '%{tbFilter.Text}%'";
                    break;

                case "Nationality":
                    DVpeople.RowFilter = $"Nationality LIKE %{tbFilter.Text}%";
                    break;

                case "Gendor":
                    
                    if (tbFilter.Text.ToLower() == "male")
                        DVpeople.RowFilter = "Gendor = 0";
                    else if (tbFilter.Text.ToLower() == "female")
                        DVpeople.RowFilter = "Gendor = 1";
                    else
                        DVpeople.RowFilter = "1=0"; 
                    break;

                case "Phone":
                    DVpeople.RowFilter = $"Phone LIKE '%{tbFilter.Text}%'";
                    break;

                case "Email":
                    DVpeople.RowFilter = $"Email LIKE '%{tbFilter.Text}%'";
                    break;

                default:
                    DVpeople.RowFilter = "";
                    break;
            }

        }

        

       

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new AddUpdateScreen(-1);
            form.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to delete this person?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                int ID = ((int)dgvPeople.CurrentRow.Cells["PersonID"].Value);

                string path = DVLDBusinessLayer.clsManagePeople.GetPersonPicture(ID);
                DVLDBusinessLayer.clsManagePeople.deletePerson(ID);
                File.Delete("D:\\Dvld-profile-Pic\\" + path);
                refreshPeople();
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbFilter_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
