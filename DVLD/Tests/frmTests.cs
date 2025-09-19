using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmTests : Form
    {
     
        public int DlAppID { get; }

        DataView dv;

        DataTable DtScheduleVisionTest = new DataTable("ScheduleVisionTest");

        int DLAppID;
        int TestType;

        string Dclass;
        string name;
        int trials ;
        double fees ;
        int TestAppID;
        int IsLocked;
        DateTime TestDate;


        public frmTests(int DlAppId,int testType)
        { 
            
            InitializeComponent();

            this.TestType = testType;

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

            this.DlAppID = DlAppId;
          applicationInfo1.DLAppID = DlAppID;

            DtScheduleVisionTest.Rows.Clear();


            DtScheduleVisionTest.Columns.Add("DLAppID", typeof(int));
            DtScheduleVisionTest.Columns.Add("DClass", typeof(string));
            DtScheduleVisionTest.Columns.Add("Name", typeof(string));
            DtScheduleVisionTest.Columns.Add("Trial", typeof(int));
            DtScheduleVisionTest.Columns.Add("Fees", typeof(double));
            DtScheduleVisionTest.Columns.Add("Date", typeof(DateTime));
            DtScheduleVisionTest.Columns.Add("IsLocked", typeof(int));
            DtScheduleVisionTest.Columns.Add("TestAppID", typeof(int));



            DLAppID = this.DlAppID;
            Dclass = applicationInfo1.GetLicense();
            name = applicationInfo1.GetApplicantName();
            trials = DVLDBusinessLayer.clsManageApplication.getTestTrial(TestType, DLAppID);
            fees = DVLDBusinessLayer.clsManageApplication.getTestFees(TestType);
            TestDate = new DateTime(1914, 7, 28);

        }

        private void refreshTests()
        {
                dv = DVLDBusinessLayer.clsManageApplication.getTests(DlAppID, TestType).DefaultView;

            dgvTests.DataSource = dv ;

        }

       

        private void frmVisionTest_Load(object sender, EventArgs e)
        {
            refreshTests();

        }

        private void btnNewTestAppointment_Click(object sender, EventArgs e)
        {

          
            int lastTestAppointmentID = DVLDBusinessLayer.clsManageApplication.GetMaxTestAppointmenIDtForSpecificLDLAppID(DlAppID,TestType);

            DataView testApp = DVLDBusinessLayer.clsManageApplication.getTests(DlAppID,TestType).DefaultView;
            


            if (testApp.Table.Rows.Count > 0)
            {
                testApp.RowFilter = $"[Appointment ID] = {lastTestAppointmentID}";

                DataView test = DVLDBusinessLayer.clsManageApplication.getTest(lastTestAppointmentID).DefaultView;

                if (Convert.ToInt32(testApp[0]["isLocked"]) == 0)
                {
                    MessageBox.Show("you have an unfinished Test Appointment!", "Unfinished Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (test.Table.Columns.Count > 0)
                {
                    test.RowFilter = $"TestAppointmentID={lastTestAppointmentID}";


                    if (test != null && Convert.ToInt32(test[0]["TestResult"]) == 1)
                    {
                        MessageBox.Show("You have already passed this Test!", "finished Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }

            }

            Dclass = applicationInfo1.GetLicense();
            name = applicationInfo1.GetApplicantName();

            DtScheduleVisionTest.Rows.Clear();

            TestDate = new DateTime(1914, 7, 28);

            

            DtScheduleVisionTest.Rows.Add(DLAppID, Dclass, name, trials, fees, TestDate,0);

                Form frm = new frmScheduleTest(DtScheduleVisionTest,TestType);
                frm.ShowDialog();
                refreshTests();

            
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(dgvTests.CurrentRow.Cells["IsLocked"].Value)==1)
            {
                MessageBox.Show("This Is An Already Finished Test!");
                return;
            }
            int DLAppID = this.DlAppID;

            string Dclass = applicationInfo1.GetLicense();
            string name = applicationInfo1.GetApplicantName();
            int trials = DVLDBusinessLayer.clsManageApplication.getTestTrial(TestType, DLAppID);
            DateTime date = Convert.ToDateTime(applicationInfo1.GetAppDate());
            double fees = DVLDBusinessLayer.clsManageApplication.getTestFees(TestType);

            int testAppID=DVLDBusinessLayer.clsManageApplication.GetMaxTestAppointmenIDtForSpecificLDLAppID(DLAppID, TestType);
            


            DataTable dt = new DataTable("TakeVisionTest");

            dt.Columns.Add("DLAppID", typeof(int));
            dt.Columns.Add("DClass", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Trial", typeof(int));
            dt.Columns.Add("Date",typeof(DateTime));
            dt.Columns.Add("Fees", typeof(double));
            dt.Columns.Add("TestAppID",typeof(int));
            dt.Columns.Add("RTestAppID", typeof(int));
            

            dt.Rows.Add(DLAppID, Dclass, name, trials,date, fees, testAppID);


            Form frm = new TakeVisionTest(dt,TestType);
            frm.ShowDialog();

            refreshTests();

        }

        private void applicationInfo1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
                
            int appID = (int)dgvTests.CurrentRow.Cells["Appointment ID"].Value;

           DataTable Appointment =  DVLDBusinessLayer.clsManageApplication.getTestAppointment(appID);

            TestDate = Convert.ToDateTime(Appointment.Rows[0]["AppointmentDate"]);

            IsLocked = Convert.ToInt32(dgvTests.CurrentRow.Cells["IsLocked"].Value);

            TestAppID = (int)dgvTests.CurrentRow.Cells["Appointment ID"].Value;

            Dclass = applicationInfo1.GetLicense();
            name = applicationInfo1.GetApplicantName();

            DtScheduleVisionTest.Rows.Clear();

            DtScheduleVisionTest.Rows.Add(DLAppID, Dclass, name, trials, fees, TestDate,IsLocked,TestAppID);

            Form frm = new frmScheduleTest(DtScheduleVisionTest,TestType);
            frm.ShowDialog();
            refreshTests();


        }

        private void dgvTests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
