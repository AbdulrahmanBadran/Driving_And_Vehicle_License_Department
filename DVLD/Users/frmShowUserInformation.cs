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
    public partial class frmShowUserInformation : Form
    {
        public int ID { get; set; }

        DataTable User;
        public frmShowUserInformation(int ID)
        {
            InitializeComponent();
            User = DVLDBusinessLayer.clsManageUsers.retrieveUser(ID);
            fillingControls();

        }

        private void fillingControls()
        {

            pic.ID = Convert.ToInt32(User.Rows[0]["PersonID"]);

            lbUserID.Text = User.Rows[0]["UserID"].ToString();
            lbUserName.Text = User.Rows[0]["userName"].ToString();
            if (Convert.ToInt32(User.Rows[0]["IsActive"]) == 1)
                lbIsActive.Text = "Yes";
            else
                lbIsActive.Text = "No";

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void frmShowUserInformation_Load(object sender, EventArgs e)
        {

        }
    }
}
