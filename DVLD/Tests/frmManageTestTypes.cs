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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void refreshTestTypes()
        {
            dgvTestTypes.DataSource = DVLDBusinessLayer.clsManageApplication.getAllTestTypes();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            refreshTestTypes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvTestTypes.CurrentRow.Cells["TestTypeID"].Value;

            Form frm = new frmEditTestType(ID);
            frm.ShowDialog();
            refreshTestTypes();
        }

    }
}
