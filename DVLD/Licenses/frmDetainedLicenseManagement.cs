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
    public partial class frmDetainedLicenseManagement : Form
    {
       DataView DVDetainedLicenses;

        public frmDetainedLicenseManagement()
        {
            InitializeComponent();
            DVDetainedLicenses = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveDetainedLicenses().DefaultView;
        }

        private void refreshData()
        {
            dgvDetainedLicenses.DataSource = DVDetainedLicenses;

        }

        private void frmDetainedLicenseManagement_Load(object sender, EventArgs e)
        {
            refreshData();

        }

        private void cmbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilters.SelectedItem != null && cmbFilters.SelectedItem.ToString() == "None")
                tbFilter.Visible = false;
            else
                tbFilter.Visible = true;
        }

        private void tbFilter_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
           
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void showPersonDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nationalNo = (string)dgvDetainedLicenses.CurrentRow.Cells["NationalNo"].Value;

            DataTable person=DVLDBusinessLayer.clsManagePeople.GetPerson(nationalNo);

            int PersonID= Convert.ToInt32(person.Rows[0]["PersonID"]);

            Form frm = new showPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int LicenseID= (int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value;

            int LDLAppID = DVLDBusinessLayer.clsDriversAndLicenses.retreiveLDLAppID(LicenseID);

            Form frm = new frmShowLicenseInfo(LDLAppID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value;

            int LDLAppID = DVLDBusinessLayer.clsDriversAndLicenses.retreiveLDLAppID(LicenseID);

            string nationalNo = (string)dgvDetainedLicenses.CurrentRow.Cells["NationalNo"].Value;


            Form frm = new frmLicenseHistory(LDLAppID, nationalNo);
            frm.ShowDialog();
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = cmbFilters.SelectedItem?.ToString() ?? "";

            switch (filter)
            {
                case "None":
                    DVDetainedLicenses.RowFilter = "";
                    break;

                case "License ID":
                    if (int.TryParse(tbFilter.Text, out int ID))
                    {
                        DVDetainedLicenses.RowFilter = $"[License ID] = {ID}";
                    }
                    else
                    {
                        DVDetainedLicenses.RowFilter = "";
                    }
                    break;

                case "Is Released":
                    if (int.TryParse(tbFilter.Text, out int isReleased))
                    {
                        DVDetainedLicenses.RowFilter = $"[IsReleased] = {isReleased}";
                    }
                    else
                    {
                        DVDetainedLicenses.RowFilter = "";
                    }
                    break;

                case "Full Name":
                    DVDetainedLicenses.RowFilter = $"fullName LIKE '%{tbFilter.Text}%'";
                    break;

                case "National No.":
                    DVDetainedLicenses.RowFilter = $"NationalNo LIKE '%{tbFilter.Text}%'";
                    break;

                default:
                    DVDetainedLicenses.RowFilter = "";
                    break;
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value;

            Form frm = new frmReleaseDetainedLicense(LicenseID);
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells["IsReleased"].Value) == 1)
            {
                cmsRelease.Enabled = false;
            }
            else
                cmsRelease.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
