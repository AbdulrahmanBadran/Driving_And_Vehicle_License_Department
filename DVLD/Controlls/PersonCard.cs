using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using System.IO;
using DVLD.Properties;
using System.Data.SqlClient;
namespace DVLD
{
    public partial class PersonCard : UserControl
    {

       public enum eMode { enAdd=0,enUpdate=1,enRead=2}

       public eMode enMode { get; set; }
       public int ID { get; set; }

        public string NationalNo {  get; set; }
       
        public PersonCard()
        {
            InitializeComponent();
            
        }

        private void UserCard_Load(object sender, EventArgs e)
        {


            refreshCountry();
            dtpBirth.MaxDate = DateTime.Now.AddYears(-18);

            if (ID == -1)
            {
                enMode = eMode.enAdd;
                lbMode.Text = "Add New Person";
            }
            else if(enMode==eMode.enRead)
            {
                controllesFilling();
                DisableAllControls();
                return;
            }
            else
            {      
               enMode = eMode.enUpdate;
                controllesFilling();
                lbMode.Text = "Update Person";
            }

            
                

        }

        private void refreshCountry()
        {
            DataTable countries = DVLDBusinessLayer.clsManagePeople.GetAllCountries();

            foreach (DataRow row in countries.Rows)
            {
                cbCountry.Items.Add(row["countryName"]);
            }

        }

        private bool AddNew()
        {
            string firstName, secondName, thirdName, lastName,
                nationalNo,Email,phone,address,picturePath;

            int gender,countryID=0;
            DateTime birthDay;

            firstName = tbFirstName.Text;
            secondName = tbSecondName.Text;
            thirdName = tbThirdName.Text;
            lastName = tbLastName.Text;

            birthDay = dtpBirth.Value;

            nationalNo= tbNationalNo.Text;

            Email=mtbEmail.Text;
            phone=mtbPhone.Text;

            countryID =DVLDBusinessLayer.clsManagePeople.GetCountryID( cbCountry.Text);
            address=tbAddress.Text;

            if (rbMale.Checked)
                gender = 0;
            else
                gender = 1;

            picturePath = pbPicture.Tag.ToString();

            return DVLDBusinessLayer.clsManagePeople.AddNewPerson(
                firstName, secondName, thirdName, lastName,
                nationalNo, Email, phone, countryID,
                address, picturePath, gender, birthDay);
        }

        private bool update()
        {

            return DVLDBusinessLayer.clsManagePeople.updatePerson(
                ID,
    tbFirstName.Text,
    tbSecondName.Text,
    tbThirdName.Text,
    tbLastName.Text,
    tbNationalNo.Text,
    mtbEmail.Text,
    mtbPhone.Text,
    DVLDBusinessLayer.clsManagePeople.GetCountryID(cbCountry.Text),
    tbAddress.Text,
    pbPicture.Tag.ToString(),
    rbMale.Checked ? 0 : 1,
    dtpBirth.Value);
        }

        private void DisableAllControls()
        {
            tbFirstName.Enabled =  false;
            tbSecondName.Enabled = false;
            tbThirdName.Enabled =  false;
            tbLastName.Enabled =   false;
            dtpBirth.Enabled =     false;
            tbNationalNo.Enabled = false;
            mtbEmail.Enabled =     false;
            mtbPhone.Enabled =     false;
            cbCountry.Enabled =    false;
            tbAddress.Enabled =    false;
            rbMale.Enabled =       false;
            rbFemale.Enabled =     false;
            pbPicture.Enabled =    false;
            btnSave.Enabled =      false;
            linkLabel1.Visible =   false;
        }

        private void controllesFilling()
        {
            DataTable person;

            if (NationalNo != null)
            {
                person= DVLDBusinessLayer.clsManagePeople.GetPerson(NationalNo);
            }
            else
            person = DVLDBusinessLayer.clsManagePeople.GetPerson(ID);

            if (person == null)
            {
                return;
            }

            DataRow row = person.Rows[0];
            
            tbFirstName.Text = row["FirstName"].ToString();
            tbSecondName.Text = row["SecondName"].ToString();
            tbThirdName.Text = row["ThirdName"].ToString();
            tbLastName.Text = row["LastName"].ToString();
         
            dtpBirth.Value = Convert.ToDateTime(row["DateOfBirth"]);
          
            tbNationalNo.Text = row["NationalNo"].ToString();

            
            mtbEmail.Text = row["Email"].ToString();
            mtbPhone.Text = row["Phone"].ToString();


            cbCountry.Text = DVLDBusinessLayer.clsManagePeople.GetCountryName(Convert.ToInt32(row["NationalityCountryID"]));

            tbAddress.Text = row["Address"].ToString();

            int gender = Convert.ToInt32(row["Gendor"]);
            if (gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            string path =  row["ImagePath"].ToString();
            pbPicture.Tag = path;

            if (File.Exists(path))
            {
                pbPicture.Image = Image.FromFile(path);
                pbPicture.Tag= path;
            }
            else
                pbPicture.Image = rbMale.Checked ? Resources.Male_512 : Resources.Female_512;


        }

        private void save()
        {
            switch (enMode)
            {

                case eMode.enAdd:
                    {
                        if(AddNew())
                        enMode = eMode.enUpdate;
                    }
                    break;

                case eMode.enUpdate:
                    {
                        update();
                        lbMode.Text = "Update Person";
                    }
                    break;

            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        

        

        public string firstName()
        {

            return tbFirstName.Text;
        }

        public string secondName()
        {

            return tbSecondName.Text;
        }

        public string thirdName()
        {

            return tbThirdName.Text;
        }

        public string lastName()
        {

            return tbLastName.Text;
        }


        public string nationalNo()
        {

            return tbNationalNo.Text;
        }

        public DateTime dateOfBirth()
        {
            return dtpBirth.Value;
        }

        public byte gendor()
        {
            if (rbMale.Checked)
            {
                return 0;
            }
            return 1;

        }

        public string phone()
        {
            return mtbPhone.Text;
        }

        public string email()
        {
            return mtbEmail.Text;
        }

        public string country()
        {
            return cbCountry.Text;
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbFirstName.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(tbFirstName, "first name is must to enter!");
            }
            else
            {

                errorProvider1.SetError(tbFirstName, "");

            }
        }

        private void tbSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbSecondName.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(tbSecondName, "second name is must to enter!");
            }
            else
            {

                errorProvider1.SetError(tbSecondName, "");

            }
        }

        private void tbThirdName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbThirdName.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(tbThirdName, "third name is must to enter!");
            }
            else
            {

                errorProvider1.SetError(tbThirdName, "");

            }
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbLastName.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(tbLastName, "last name is must to enter!");
            }
            else
            {

                errorProvider1.SetError(tbLastName, "");

            }
        }

        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbNationalNo.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(tbNationalNo, "National Number is must to enter!");
            }
            if (DVLDBusinessLayer.clsManagePeople.isPersonExist(tbNationalNo.Text))
            {
                e.Cancel= true;
                errorProvider1.SetError(tbNationalNo, "this National Number is already exist in the system");
            }
            else
            {

                errorProvider1.SetError(tbNationalNo, "");

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Validating(object sender, CancelEventArgs e)
        {
            if (!rbFemale.Checked && !rbMale.Checked)
            {

                e.Cancel = true;
                errorProvider1.SetError(panel1, "provide your gender!");
            }
            else
            {

                errorProvider1.SetError(panel1, "");

            }
        }

        private void mtbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(mtbEmail.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(mtbEmail, "Email is must to enter!");
            }
            else
            {

                errorProvider1.SetError(mtbEmail, "");

            }
        }

        private void cbCountry_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cbCountry.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(cbCountry, "Country is must to enter!");
            }
            else
            {

                errorProvider1.SetError(cbCountry, "");

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CancelEventArgs E=new CancelEventArgs();
            panel1_Validating(sender, E);

            save();
            //this.ParentForm.Close();
        }

        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string DistinationDir = "D:\\Dvld-profile-Pic";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string currentPath = openFileDialog1.FileName;

                if (ID != -1)
                {

                    if (pbPicture.Image != null)
                    {
                        string oldPath =  DVLDBusinessLayer.clsManagePeople.GetPersonPicture(ID);

                        pbPicture.Image.Dispose();
                        pbPicture.Image = null;

                        if (File.Exists(oldPath))
                            File.Delete(oldPath);
                    }

                    

                }

                Guid newName = Guid.NewGuid();
                string ext=Path.GetExtension(currentPath);
                string fileName = newName.ToString()+ext;
               


                 string destinationPath = Path.Combine(DistinationDir,fileName);

                File.Copy(currentPath, destinationPath);

                pbPicture.Image = Image.FromFile(destinationPath);
                pbPicture.Tag = fileName;
                llRemove.Visible = true;
            }
        

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPicture.Image == null)
                pbPicture.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if(pbPicture.Image==null)
            pbPicture.Image = Resources.Female_512;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           this.ParentForm.Close();
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPicture.Image.Dispose();
            pbPicture.Image = null;
            File.Delete("D:\\Dvld-profile-Pic\\" + pbPicture.Tag.ToString());
        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }





        
    }


