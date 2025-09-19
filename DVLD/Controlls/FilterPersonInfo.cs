using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DVLD
{
    public partial class FilterPersonInfo : UserControl
    {
        public FilterPersonInfo()
        {
            InitializeComponent();
        }

        static bool filled = false;



        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.SelectedItem.ToString() == "Person ID")
            {
                mtbFilter.Mask = "99999999";
                mtbFilter.ValidatingType = typeof(int);
            }
            else
            {
                mtbFilter.Mask = null;
            }
        }

        public bool isFilled()
        {
            return filled;
        }

        private void pic_Load(object sender, EventArgs e)
        {
            pic.personInformationCard_Load(sender, e);

        }

        private void FilterPersonInfo_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new AddUpdateScreen(-1);
            frm.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbFilter.SelectedItem != null && cmbFilter.SelectedItem.ToString() == "Person ID")
            {
                int ID;
                bool isNumber = int.TryParse(mtbFilter.Text, out ID);

                if (DVLDBusinessLayer.clsManagePeople.isPersonExist(ID))
                {
                    pic.ID = ID;
                    filled = true;
                    pic_Load(sender, e);
                }
                else
                {
                    MessageBox.Show($"person with ID: {ID} does not exist");
                    pic.returnToDefault();
                }

            }
            if (cmbFilter.SelectedItem != null && cmbFilter.SelectedItem.ToString() == "National No.")
            {

                if (DVLDBusinessLayer.clsManagePeople.isPersonExist(mtbFilter.Text))
                {
                    pic.nationalNo = mtbFilter.Text;
                    filled = true;
                    pic_Load(sender, e);


                }
                else
                {
                    MessageBox.Show($"person with Vationa lNo: {mtbFilter.Text} does not exist");
                    pic.returnToDefault();
                }


            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pic_Load_1(object sender, EventArgs e)
        {

        }

        public int getID()
        {
            return Convert.ToInt32(pic.getID());
        }

        public string getName()
        {
            return pic.getName();
        }

        public string getDateOfBirth()
        {
            return pic.getDateOfBirth();
        }

        public string getNationalNo()
        {
            return pic.getNationalNo();
        }

        public string getEmail()
        {
            return pic.getEmail();
        }

        public string getPhone()
        {
            return pic.getPhone();
        }

        public string getCountry()
        {
            return pic.getCountry();
        }

        public string getAddress()
        {
            return pic.getAddress();
        }

        public string getGender()
        {
            return pic.getGender();
        }

        public Image getPicture()
        {
            return pic.getPicture();
        }
    }

}
