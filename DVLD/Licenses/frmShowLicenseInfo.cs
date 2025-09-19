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
    public partial class frmShowLicenseInfo : Form
    {

        public frmShowLicenseInfo(int LDLAppID)
        {
            InitializeComponent();
            LI.LDLAppID = LDLAppID;
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            //LI.Search();
        }
    }
}
