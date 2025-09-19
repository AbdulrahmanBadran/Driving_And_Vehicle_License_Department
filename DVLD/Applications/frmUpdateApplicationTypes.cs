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
    public partial class frmUpdateApplicationTypes : Form
    {

        int ID;
        public frmUpdateApplicationTypes(int ID)
        {
            InitializeComponent();

            this.ID = ID;
        }

        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            DataTable applicationType = DVLDBusinessLayer.clsManageApplication.getApplicationType(ID);

            lbID.Text = Convert.ToString(applicationType.Rows[0]["ApplicationTypeID"]);
            tbTitle.Text = Convert.ToString(applicationType.Rows[0]["ApplicationTypeTitle"]);
            tbFees.Text = Convert.ToString(applicationType.Rows[0]["ApplicationFees"]);
        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name=tbTitle.Text;
            double fees=Convert.ToDouble(tbFees.Text);

            if(DVLDBusinessLayer.clsManageApplication.UpdateApplicationType(ID,name,fees))
            {
                
                MessageBox.Show("Application Type updated successfully"); 
                this.Close();
                
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}
