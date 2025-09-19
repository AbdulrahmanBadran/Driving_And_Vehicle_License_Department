using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class personInformationCard : UserControl
    {

        public int ID { get; set; }

       

        public string nationalNo {  get; set; }
        public personInformationCard()
        {
            InitializeComponent();
            returnToDefault();
        }

       

        public void personInformationCard_Load(object sender, EventArgs e)
        {
            
            controllesFilling();
            
        }

        public void returnToDefault()
        {
            lbID.Text = "[????]";

            lbName.Text = "[????]";


            lbDateOfBirth.Text = "[????]";

            lbNationalNo.Text = "[????]";


            lbEmail.Text = "[????]";
            lbPhone.Text = "[????]";


            lbCountry.Text = "[????]";

            lbAddress.Text = "[????]";


            lbGendor.Text = "[????]";

            pbPicture.Image = Resources.Male_512;

            Tag = "empty";

        }

        private void controllesFilling()
        {
            DataTable person;
            if (nationalNo != null)
                person = DVLDBusinessLayer.clsManagePeople.GetPerson(nationalNo);
            else
                person = DVLDBusinessLayer.clsManagePeople.GetPerson(ID);

            if (person == null || person.Rows.Count == 0)
            {
                return;
            }


           
                DataRow row = person.Rows[0];

                string name = row["FirstName"].ToString() + " " +
                    row["SecondName"].ToString() + " " +
                    row["ThirdName"].ToString() + " " +
                    row["LastName"].ToString();

                lbID.Text =row["personID"].ToString();

                lbName.Text = name;


            lbDateOfBirth.Text = Convert.ToDateTime(row["DateOfBirth"]).ToShortDateString();

            lbNationalNo.Text = row["NationalNo"].ToString();


                lbEmail.Text = row["Email"].ToString();
                lbPhone.Text = row["Phone"].ToString();


                lbCountry.Text = DVLDBusinessLayer.clsManagePeople.GetCountryName(Convert.ToInt32(row["NationalityCountryID"]));

                lbAddress.Text = row["Address"].ToString();

                int gender = Convert.ToInt32(row["Gendor"]);

                if (gender == 0)
                    lbGendor.Text = "male";
                else
                    lbGendor.Text = "female";

                string path = "D:\\Dvld-profile-Pic\\"+ row["ImagePath"].ToString();
                pbPicture.Tag = path;

                if (File.Exists(path))
                {
                    pbPicture.Image = Image.FromFile(path);
                    pbPicture.Tag = path;
                }
                else
                    pbPicture.Image = lbGendor.Text == "male" ? Resources.Male_512 : Resources.Female_512;

            Tag = "filled";
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ID == 0 && nationalNo==null)
            {
                MessageBox.Show("Please select a person to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Form frm;
            if (nationalNo != null)
               frm  = new AddUpdateScreen(nationalNo);
            else
             frm = new AddUpdateScreen(ID);

            frm.ShowDialog();
        }

        public string getID()
        {
            return lbID.Text == "[????]" ? null : lbID.Text;
        }

        public string getName()
        {
            return lbName.Text == "[????]" ? null : lbName.Text;
        }

        public string getDateOfBirth()
        {
            return lbDateOfBirth.Text == "[????]" ? null : lbDateOfBirth.Text;
        }

        public string getNationalNo()
        {
            return lbNationalNo.Text == "[????]" ? null : lbNationalNo.Text;
        }

        public string getEmail()
        {
            return lbEmail.Text == "[????]" ? null : lbEmail.Text;
        }

        public string getPhone()
        {
            return lbPhone.Text == "[????]" ? null : lbPhone.Text;
        }

        public string getCountry()
        {
            return lbCountry.Text == "[????]" ? null : lbCountry.Text;
        }

        public string getAddress()
        {
            return lbAddress.Text == "[????]" ? null : lbAddress.Text;
        }

        public string getGender()
        {
            return lbGendor.Text == "[????]" ? null : lbGendor.Text;
        }

        public Image getPicture()
        {
            return pbPicture.Image == Resources.Male_512 ? null : pbPicture.Image;
        }
    }
}
