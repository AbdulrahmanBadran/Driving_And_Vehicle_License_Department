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
    public partial class frmLicenseHistory : Form
    {
        int LDLAppID;

        DataTable person;
        public frmLicenseHistory(int LDLAppID,string nationalNo)
        {
            InitializeComponent();
            this.LDLAppID = LDLAppID;
            person = DVLDBusinessLayer.clsManagePeople.GetPerson(nationalNo);

            pic.ID = Convert.ToInt32(person.Rows[0]["PersonID"]);

        }


        private void refreshData()
        {
            int personID = Convert.ToInt32(person.Rows[0]["PersonID"]);

            int DriverID=DVLDBusinessLayer.clsDriversAndLicenses.getDriverID(personID);

            dgvInternationalLicense.DataSource=DVLDBusinessLayer.clsDriversAndLicenses.RetrieveInternationalLicenseHistory(DriverID);

            dgvLoacalLicenses.DataSource = DVLDBusinessLayer.clsDriversAndLicenses.RetrieveLicenseHistory(LDLAppID);
        }


        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            refreshData();
        }

        private void dgvLoacalLicenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
