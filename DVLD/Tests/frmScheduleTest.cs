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
    public partial class frmScheduleTest : Form
    {
        enum Emode { enAdd=0,enUpdate=1}

        Emode enMode;

        int DLAppID {  get; set; }

        int TestType;

        DataTable TestInfo;

        DataTable lastTest;

        DataTable Tests;
        public frmScheduleTest(DataTable dt,int testType)
        {
            InitializeComponent();


            if (testType == 1)
            {
                pbVision.Visible = true;
                lbVision.Visible = true;

                pbWritten.Visible = !true;
                lbWritten.Visible = !true;

                pbStreet.Visible = !true;
                lbStreet.Visible = !true;


            }

            if (testType == 2)
            {
                pbVision.Visible = !true;
                lbVision.Visible = !true;

                pbWritten.Visible = true;
                lbWritten.Visible = true;

                pbStreet.Visible = !true;
                lbStreet.Visible = !true;


            }


            if (testType == 3)
            {
                pbVision.Visible = !true;
                lbVision.Visible = !true;

                pbWritten.Visible = !true;
                lbWritten.Visible = !true;

                pbStreet.Visible = true;
                lbStreet.Visible = true;


            }



            TestInfo =dt;
            this.TestType = testType;

            DateTime ww1Start = new DateTime(1914, 7, 28);

            if (!TestInfo.Rows[0].IsNull("Date") &&
    Convert.ToDateTime(TestInfo.Rows[0]["Date"]).Date == ww1Start.Date) // if its in this date this mean this date is default and the mode is add
            {
                enMode = Emode.enAdd;

                dtpDate.Enabled = true;
                btnSave.Enabled = true;
                lbCantEdit.Visible = false;

            }
            else
            {
                enMode = Emode.enUpdate;

                dtpDate.Value = Convert.ToDateTime(TestInfo.Rows[0]["Date"]);

                if (Convert.ToInt32(TestInfo.Rows[0]["IsLocked"]) == 1)
                {
                    dtpDate.Enabled = false;
                    btnSave.Enabled = false;
                    lbCantEdit.Visible = true;
                }
            }


            DLAppID = Convert.ToInt32(TestInfo.Rows[0]["DLAppID"]);

            int AppointmentID = DVLDBusinessLayer.clsManageApplication.GetMaxTestAppointmenIDtForSpecificLDLAppID(DLAppID, TestType);

            lastTest = DVLDBusinessLayer.clsManageApplication.getTestAppointment(AppointmentID);


            Tests = DVLDBusinessLayer.clsManageApplication.getTests(DLAppID,TestType);

            

        }

        private void fillingControls()
        {
            if ((Tests.Rows.Count>0))
            {

                if (TestType == 1)
                {
                    lbVision.Text = "Schedule Retake Test";               
                }

                if (TestType == 2)
                {
                    lbWritten.Text = "Schedule Retake Test";                    
                }

                if (TestType == 3)
                {
                    lbStreet.Text = "Schedule Retake Test";        
                }

                gbRetakeTest.Enabled = true;

                DataTable applicationType = DVLDBusinessLayer.clsManageApplication.getBasicApplicationType(7);
                int RTestAppFees = Convert.ToInt32(applicationType.Rows[0]["ApplicationFees"]);

                lbRFess.Text = RTestAppFees.ToString();

                if (Convert.ToInt32(TestInfo.Rows[0]["IsLocked"]) == 1)
                {
                    dtpDate.Enabled = false;
                    btnSave.Enabled = false;
                    lbCantEdit.Visible = true;
                }
                else
                {
                    dtpDate.Enabled = true;
                    btnSave.Enabled = true;
                    lbCantEdit.Visible = false;
                }
            }
            else
            {

                gbRetakeTest.Enabled = false;
            }


            lbDLAppID.Text = Convert.ToString(TestInfo.Rows[0]["DLAppID"]);
            lbClass.Text = Convert.ToString(TestInfo.Rows[0]["DClass"]);
            lbName.Text = Convert.ToString(TestInfo.Rows[0]["Name"]);
            lbTrial.Text = Convert.ToString(TestInfo.Rows[0]["Trial"]);
            lbFees.Text = Convert.ToString(TestInfo.Rows[0]["fees"]);
            lbTotalFess.Text = Convert.ToString(Convert.ToInt32(lbFees.Text)+ Convert.ToInt32(lbRFess.Text));

            if(Convert.ToInt32(lbTrial.Text)>0)
            {
                int RTestAppID;
                if (enMode==Emode.enAdd)
                {
                     RTestAppID = DVLDBusinessLayer.clsManageApplication.getMaxRetakeTestApplicationID() + 1;
                }
                else
                {
                    RTestAppID = DVLDBusinessLayer.clsManageApplication.getMaxRetakeTestApplicationID();
                }

                lbRTestAppID.Text= RTestAppID.ToString();

            }
          
            
        }


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
                
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void frmScheduleVisionTest_Load(object sender, EventArgs e)
        {
            fillingControls();
            
        }

        private void AddAppointment()
        {
            int DLAppId = Convert.ToInt32(lbDLAppID.Text);
            DateTime Date = dtpDate.Value.Date;
            double fees = Convert.ToDouble(lbTotalFess.Text);
            int createdByUserID = Convert.ToInt32(GlobalSettings.CurrentUser.Rows[0]["UserID"]);
            int isLocked = 0;
            int RTAppID;
            if (lbRTestAppID.Text.Trim() == "N/A")
                RTAppID = -1;
            else
                RTAppID = Convert.ToInt32(lbRTestAppID.Text);


            if (DVLDBusinessLayer.clsManageApplication.
                AddNewTestAppointment(TestType, DLAppId,
                Date, fees, createdByUserID, isLocked, RTAppID))
            {
                MessageBox.Show("Test has Scheduled Successfully");
            }
            else
                MessageBox.Show("there is error");

        }

        private void UpdateAppointment()
        {
            DateTime Date = dtpDate.Value.Date;

            int testAppID = Convert.ToInt32(TestInfo.Rows[0]["testAppID"]);

            if (DVLDBusinessLayer.clsManageApplication.
                updateTestAppointment(Date, testAppID))
            {
                MessageBox.Show("Test has Updated Successfully");
            }
            else
                MessageBox.Show("there is error");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (enMode == Emode.enAdd)
            {
                AddAppointment();
            }
            else
            {
                UpdateAppointment();
            }

         }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
