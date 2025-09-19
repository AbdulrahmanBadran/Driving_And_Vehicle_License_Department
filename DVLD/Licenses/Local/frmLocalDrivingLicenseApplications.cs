using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        private DataView DvlocalDrivingLicenseApplications;

        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void refreshData()
        {
            DvlocalDrivingLicenseApplications = DVLDBusinessLayer.clsManageApplication
                .GetAllLocalDrivingLicenseApplications()
                .DefaultView;

            dgvLocalDrivingLicenseApplications.DataSource = DvlocalDrivingLicenseApplications;
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            refreshData();
            cmbFilters_SelectedIndexChanged(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new frmNewLocalDrivingLicense();
            frm.ShowDialog();
            refreshData();
        }

        private void cmbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilter.Visible = !(cmbFilters.SelectedItem != null && cmbFilters.SelectedItem.ToString() == "None");
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = cmbFilters.SelectedItem?.ToString() ?? "";

            switch (filter)
            {
                case "None":
                    DvlocalDrivingLicenseApplications.RowFilter = "";
                    break;

                case "National No.":
                    DvlocalDrivingLicenseApplications.RowFilter = $"NationalNo LIKE '%{tbFilter.Text}%'";
                    break;

                case "L.D.LAppID":
                    if (int.TryParse(tbFilter.Text, out int AppID))
                        DvlocalDrivingLicenseApplications.RowFilter = $"LDLAppID = {AppID}";
                    else
                        DvlocalDrivingLicenseApplications.RowFilter = "";
                    break;

                case "Full Name":
                    DvlocalDrivingLicenseApplications.RowFilter = $"fullName LIKE '%{tbFilter.Text}%'";
                    break;

                case "Status":
                    DvlocalDrivingLicenseApplications.RowFilter = $"Status LIKE '%{tbFilter.Text}%'";
                    break;

                default:
                    DvlocalDrivingLicenseApplications.RowFilter = "";
                    break;
            }
        }

        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LDLAppID"].Value);
            DataTable LDL = DVLDBusinessLayer.clsManageApplication.GetLocalDrivingLicenseApplication(LDLID);

            int AppID = Convert.ToInt32(LDL.Rows[0]["ApplicationID"]);

            if (DVLDBusinessLayer.clsManageApplication.CancelApplication(AppID))
            {
                MessageBox.Show("Application cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshData();
            }
            else
            {
                MessageBox.Show("Failed to cancel the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void scheduleVissionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LDLAppID"].Value;
            Form frm = new frmTests(ID, 1);
            frm.ShowDialog();
            refreshData();
        }

        private void cmsWrittenTest_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LDLAppID"].Value;
            Form frm = new frmTests(ID, 2);
            frm.ShowDialog();
            refreshData();
        }

        private void cmsStreetTest_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LDLAppID"].Value;
            Form frm = new frmTests(ID, 3);
            frm.ShowDialog();
            refreshData();
        }

        private void cmsIssueLicense_Click(object sender, EventArgs e)
        {
            int ldappID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LDLAppID"].Value;
            string nationalNo = (string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["NationalNo"].Value;

            Form frm = new IssueLicenseFirstTime(ldappID, nationalNo);
            frm.ShowDialog();
            refreshData();
        }

        private void showDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LDLAppID"].Value;
            Form frm = new frmApplicationInfo(ID);
            frm.ShowDialog();
        }

        private void cmsShowLicense_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LDLAppID"].Value;
            Form frm = new frmDriverLicenseInfo(ID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LDLAppID"].Value;
            string nationalNo = (string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["NationalNo"].Value;

            Form frm = new frmLicenseHistory(ID, nationalNo);
            frm.ShowDialog();
        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            int LDLID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LDLAppID"].Value);
            DataTable LDL = DVLDBusinessLayer.clsManageApplication.GetLocalDrivingLicenseApplication(LDLID);

            int AppID = Convert.ToInt32(LDL.Rows[0]["ApplicationID"]);

            if (MessageBox.Show("Are you sure you want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DVLDBusinessLayer.clsManageApplication.DeleteTests(LDLID);
                DVLDBusinessLayer.clsManageApplication.DeleteTestAppointments(LDLID);
                DVLDBusinessLayer.clsManageApplication.DeleteLocalDrivingLicenseApplication(LDLID);

                if (DVLDBusinessLayer.clsManageApplication.DeleteApplication(AppID))
                {
                    MessageBox.Show("Application deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refreshData();
                }
                else
                {
                    MessageBox.Show("Failed to delete the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null)
            {
                e.Cancel = true;
                return;
            }

            string status = Convert.ToString(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["Status"].Value);
            int passedTest = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["PassedTest"].Value);

            
            cmsEdit.Enabled = true;
            cmsDelete.Enabled = true;
            cmsCancel.Enabled = true;
            cmsIssueLicense.Enabled = true;
            cmsShowLicense.Enabled = false;
            cmsShedule.Enabled = false;
            cmsVissionTest.Enabled = false;
            cmsWrittenTest.Enabled = false;
            cmsStreetTest.Enabled = false;

           
            if (status == "Completed")
            {
                cmsEdit.Enabled = false;
                cmsDelete.Enabled = false;
                cmsCancel.Enabled = false;
                cmsIssueLicense.Enabled = false;
                cmsShowLicense.Enabled = true;
            }
            else
            {
                
                if (passedTest != 3)
                {
                    cmsIssueLicense.Enabled = false;
                    cmsShowLicense.Enabled = false;
                    cmsShedule.Enabled = true;
                }
                else if (passedTest == 3 && status != "Completed")
                {
                    cmsShedule.Enabled = false;
                    cmsIssueLicense.Enabled = true;
                }
                else
                {
                    cmsShedule.Enabled = false;
                    cmsIssueLicense.Enabled = false;
                }

                
                if (passedTest == 0)
                    cmsVissionTest.Enabled = true;
                else if (passedTest == 1)
                    cmsWrittenTest.Enabled = true;
                else if (passedTest == 2)
                    cmsStreetTest.Enabled = true;
            }
        }
    }
}
