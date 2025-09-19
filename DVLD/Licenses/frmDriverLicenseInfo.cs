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
    public partial class frmDriverLicenseInfo : Form
    {
        public frmDriverLicenseInfo(int LDLAppID)
        {
            InitializeComponent();
            LI.LDLAppID = LDLAppID;

        }

        private void frmDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            
        }

        private void LI_Load(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
