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
    public partial class frmManageApplicationType : Form
    {
        public frmManageApplicationType()
        {
            InitializeComponent();
        }

        private void frmManageApplicationType_Load(object sender, EventArgs e)
        {
            refreshApplicationTypes();
        }

        private void refreshApplicationTypes()
        {
            dgvApplicationTypes.DataSource = DVLDBusinessLayer.clsManageApplication.getAllApplicationTypes();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editAppliToolStripMenuItem_Click(object sender, EventArgs e)
        {

             int ID = ((int)dgvApplicationTypes.CurrentRow.Cells["ApplicationTypeID"].Value);

            Form frm=new frmUpdateApplicationTypes(ID);

            frm.ShowDialog();
            refreshApplicationTypes();
        }
    }
}
