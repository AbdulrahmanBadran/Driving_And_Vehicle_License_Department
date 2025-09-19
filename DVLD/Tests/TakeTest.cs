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
    public partial class TakeVisionTest : Form
    {
        DataTable Info;

        public TakeVisionTest(DataTable dt,int testType)
        {
            InitializeComponent();
            // DLAppID, Dclass, name, trials,date, fees,testAppID
            this.Info = dt;

            if (testType == 1)
            {
                pbVision.Visible = true;
               
                pbWritten.Visible = !true;
                
                pbStreet.Visible = !true;
            }

            if (testType == 2)
            {
                pbVision.Visible = !true;

                pbWritten.Visible = true;

                pbStreet.Visible = !true;
            }


            if (testType == 3)
            {
                pbVision.Visible = !true;

                pbWritten.Visible = !true;

                pbStreet.Visible = true;
            }


        }

        private void ControllFilling()
        {
            lbDLAppID.Text = Convert.ToString(Info.Rows[0]["DLAppID"]);
            lbClass.Text= Convert.ToString(Info.Rows[0]["Dclass"]);
            lbName.Text= Convert.ToString(Info.Rows[0]["name"]);
            lbTrial.Text= Convert.ToString(Info.Rows[0]["Trial"]);
            lbDate.Text= Convert.ToString(Info.Rows[0]["date"]);
            lbFees.Text= Convert.ToString(Info.Rows[0]["fees"]);
            lbTestID.Text= "Not Taken Yet";

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void TakeVisionTest_Load(object sender, EventArgs e)
        {
            ControllFilling();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!rbPass.Checked && !rbFail.Checked)
            {
                MessageBox.Show("Please provide the Applicant test result.", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }

            int testAppID = Convert.ToInt32(Info.Rows[0]["TestAppID"]);
            int createdBy = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["UserID"]);
            string notes = tbNotes.Text;
            int testResult = 0;



            if (rbPass.Checked)
            {
                testResult = 1;
            }
            else
            {
                testResult=0;
            }

            if (DVLDBusinessLayer.clsManageApplication.addNewTest(testAppID, testResult,notes, createdBy ))
            {
                MessageBox.Show("Test has finished", "test finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DVLDBusinessLayer.clsManageApplication.lockTheTestAppointment(testAppID);
                int testID = Convert.ToInt32(DVLDBusinessLayer.clsManageApplication.getTest(testAppID).Rows[0]["TestID"]);

                lbTestID.Text=testID.ToString();

            }

            else
            {
                MessageBox.Show("there is an error attending the test!","error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
