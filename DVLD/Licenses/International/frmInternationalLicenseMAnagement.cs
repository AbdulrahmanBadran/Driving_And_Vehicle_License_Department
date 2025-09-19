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
    public partial class frmInternationalLicenseMAnagement : Form
    {
        DataView dvInternationalLicenses;
        public frmInternationalLicenseMAnagement()
        {
            InitializeComponent();

             dvInternationalLicenses= DVLDBusinessLayer.clsDriversAndLicenses.RetrieveInternationalLicense().DefaultView;
        }

        private void refreshData()
        {
            dgvIntLicenses.DataSource = dvInternationalLicenses;
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmInternationalLicenseMAnagement_Load(object sender, EventArgs e)
        {
            refreshData();
        }

        

         

        private void cmbFilters_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            if (cmbFilters.SelectedItem != null && cmbFilters.SelectedItem.ToString() == "None")
                tbFilter.Visible = false;
            else
                tbFilter.Visible = true;
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = cmbFilters.SelectedItem?.ToString() ?? "";

            switch (filter)
            {
                case "None":
                    dvInternationalLicenses.RowFilter = "";
                    break;


                case "Int.License ID":
                    if (int.TryParse(tbFilter.Text, out int IntLID))
                    {
                        dvInternationalLicenses.RowFilter = $"[Int.License ID] ={IntLID}";
                    }
                    else
                    {
                        dvInternationalLicenses.RowFilter = "";
                    }
                    break;

                case "L.License ID":
                    if (int.TryParse(tbFilter.Text, out int LID))
                    {
                        dvInternationalLicenses.RowFilter = $"[L.License ID] = {LID}";
                    }
                    else
                    {
                        dvInternationalLicenses.RowFilter = "";
                    }
                    break;

                case "Is Active":
                    if (int.TryParse(tbFilter.Text, out int isActive))
                    {
                        dvInternationalLicenses.RowFilter = $"[Is Active] = {isActive}";
                    }
                    else
                    {
                        dvInternationalLicenses.RowFilter = "";
                    }
                    break;

                case "Full Name":
                    dvInternationalLicenses.RowFilter = $"fullName LIKE '%{tbFilter.Text}%'";
                    break;

                case "Status":
                    dvInternationalLicenses.RowFilter = $"Status LIKE '%{tbFilter.Text}%'";
                    break;

                default:
                    dvInternationalLicenses.RowFilter = "";
                    break;


            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new frmInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvIntLicenses.CurrentRow.Cells["L.License ID"].Value;

            DataTable license = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveLicense(LicenseID);

            int DriverID = Convert.ToInt32(license.Rows[0]["DriverID"]);

            int personID=DVLDBusinessLayer.clsManagePeople.retreivePersonID(DriverID);

            Form frm = new showPersonInfo(personID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvIntLicenses.CurrentRow.Cells["L.License ID"].Value;

            DataTable license = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveLicense(LicenseID);

            int DriverID = Convert.ToInt32(license.Rows[0]["DriverID"]);

            Form frm = new frmInternationalLicenseInfo(DriverID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvIntLicenses.CurrentRow.Cells["L.License ID"].Value;

         /*1*/   int LDLAppID=DVLDBusinessLayer.clsDriversAndLicenses.retreiveLDLAppID(LicenseID);

            DataTable license = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveLicense(LicenseID);

            int DriverID = Convert.ToInt32(license.Rows[0]["DriverID"]);

            int personID = DVLDBusinessLayer.clsManagePeople.retreivePersonID(DriverID);

            DataTable Person = DVLDBusinessLayer.clsManagePeople.GetPerson(personID);

         /*2*/   string nationalNo = Convert.ToString(Person.Rows[0]["NationalNo"]);

            Form frm = new frmLicenseHistory(LDLAppID, nationalNo);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
