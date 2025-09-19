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


    public partial class frmDrivers : Form
    {

        DataView DVDrivers;

        public frmDrivers()
        {
            InitializeComponent();

            DVDrivers = DVLDBusinessLayer.clsDriversAndLicenses.retrieveDrivers().DefaultView;
        }

        private void cmbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilters.SelectedItem == null ||
                cmbFilters.SelectedItem.ToString() == "None")
            {
                tbFilter.Visible = false;
            }
            else
                tbFilter.Visible = true;
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            string filterType=cmbFilters.SelectedItem.ToString();

            switch (filterType)
            {

                case "None":
                    DVDrivers.RowFilter = "";
                        break;

                case "Driver ID":
                    if (int.TryParse(tbFilter.Text, out int DriverID))
                        DVDrivers.RowFilter = $"DriverID = {tbFilter.Text}";
                    else
                        DVDrivers.RowFilter = "";
                        break;

                case "Person ID":
                    if (int.TryParse(tbFilter.Text, out int PersonID))
                        DVDrivers.RowFilter = $"PersonID ={tbFilter.Text}";
                    else
                        DVDrivers.RowFilter = "";
                        break;

                case "National No.":
                    DVDrivers.RowFilter = $"NationalNo like '%{tbFilter.Text}%'";
                        break;

                case "Full Name":
                    DVDrivers.RowFilter = $"FullName like '%{tbFilter.Text}%'";
                        break;

                case "Active":
                    if (int.TryParse(tbFilter.Text, out int IsActive))
                        DVDrivers.RowFilter = $"IsActive = {tbFilter.Text}";
                    else
                        DVDrivers.RowFilter = "";
                        break;
            }

        }

        private void refreshData()
        {

            dgvDrivers.DataSource = DVDrivers;
        }

        private void frmDrivers_Load(object sender, EventArgs e)
        {
            refreshData();
        }
    }
}
