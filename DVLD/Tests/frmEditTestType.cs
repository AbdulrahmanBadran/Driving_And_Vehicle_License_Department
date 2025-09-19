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
    public partial class frmEditTestType : Form
    {

        int ID {  get; set; }

        public frmEditTestType(int ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void loadControls()
        {
            DataTable testType = DVLDBusinessLayer.clsManageApplication.getTestType(ID);

            string title = Convert.ToString(testType.Rows[0]["TestTypeTitle"]);

            string description= Convert.ToString(testType.Rows[0]["TestTypeDescription"]);

            double fees= Convert.ToDouble(testType.Rows[0]["TestTypeFees"]);

            lbID.Text = ID.ToString();
            tbTitle.Text = title;
            tbDescription.Text = description;
            tbFees.Text=Convert.ToString(fees);

        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            loadControls();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

         string title = tbTitle.Text;
         string description = tbDescription.Text;
         double fees = Convert.ToDouble(tbFees.Text);

            if (DVLDBusinessLayer.clsManageApplication.UpdateTestType(ID,title,description,fees))
            {
                MessageBox.Show("Test Type updated successfully");
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
