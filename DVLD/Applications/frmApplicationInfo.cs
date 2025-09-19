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
    public partial class frmApplicationInfo : Form
    {


        public frmApplicationInfo(int DLAppID)
        {
            InitializeComponent();

            ai.DLAppID = DLAppID;
        }

        private void applicationInfo1_Load(object sender, EventArgs e)
        {

        }

        private void frmApplicationInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
