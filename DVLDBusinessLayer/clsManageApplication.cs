using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;
namespace DVLDBusinessLayer
{
    public class clsManageApplication
    {
        public static DataTable getAllApplicationTypes()
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveApplicationTypes();
        }

        public static DataTable getAllTestTypes()
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveTestTypes();
        }

        public static double getTestFees(int testTypeID)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveTestFees(testTypeID);
        }

        public static DataTable GetTest(int AppID)
        {

            return DVLDDataAccessLayer.clsManageApplication.RetrieveTest(AppID);

        }

        public static DataTable getApplicationType(int ID)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveApplicationType(ID);
        }

        public static double RetrieveApplicationFees(int appTypeID)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveApplicationFees(appTypeID);
        }

        public static DataTable getBasicApplicationType(int ID)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveBasicApplicationType(ID);
        }

        public static DataTable getTestAppointment(int testAppID)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveTestAppointment(testAppID);
        }

        public static DataTable getApplication(int DlAppID)
        {
          return DVLDDataAccessLayer.clsManageApplication.RetreiveApplication(DlAppID);
        }

        public static DataTable getTestType(int ID)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveTestType(ID);
        }

        public static bool UpdateApplicationType(int ID, string name, double fees)
        {
            return DVLDDataAccessLayer.clsManageApplication.UpdateApplicationType(ID, name, fees);
        }

        public static bool UpdateTestType(int ID, string title, string description, double fees)
        {
            return DVLDDataAccessLayer.clsManageApplication.UpdateTestType(ID, title, description, fees);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrievrLocalDrivingLicenseApplications();
        }

        public static DataTable GetLocalDrivingLicenseApplication(int ID)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrievrLocalDrivingLicenseApplication(ID);
        }

        public static DataTable getAllLicenseClasses()
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveLicenseClasses();
        }

        public static DataTable getLicenseClassByName(string name)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveLicenseClass(name);
        }

        public static DataTable getLicenseClassByID(int ID)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveLicenseClass(ID);
        }


        public static DataTable getApplicationType(string name)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveApplicationType(name);
        }

        public static bool AddNewApplication(int personID, double fees,int currentUser, int AppTypeID)
        {
            return DVLDDataAccessLayer.clsManageApplication.addNewApplication(personID,fees,currentUser,AppTypeID);
        }

        public static bool addNewLocalDrivingLicenseApplication(int ApplicationID, int licenseClassID)
        {
            return DVLDDataAccessLayer.clsManageApplication.addNewLocalDrivingLicenseApplication(ApplicationID,licenseClassID);
        }

        public static int getLastApplicationID()
        {
            return DVLDDataAccessLayer.clsManageApplication.getTheLatestApplicationID();
        }

        public static int getLastLocalDrivingLicenseApplicationID()
        {
                       return DVLDDataAccessLayer.clsManageApplication.getLastLocalDrivingLicenseApplicationID();
        }

        public static bool isApplicationExist(int PersonID,int licenseClassID)
        {
            return DVLDDataAccessLayer.clsManageApplication.isThisApplicationExist(PersonID, licenseClassID);
        }

        public static bool CancelApplication(int applicationID)
        {
            return DVLDDataAccessLayer.clsManageApplication.CancelApplication(applicationID);
        }

        public static bool DeleteTests(int LDLAppID)
        {
            return DVLDDataAccessLayer.clsManageApplication.DeleteTests(LDLAppID);
        }

        public static bool DeleteTestAppointments(int LDLAppID)
        {
            return DVLDDataAccessLayer.clsManageApplication.DeleteTestAppointments(LDLAppID);
        }

        public static bool DeleteLocalDrivingLicenseApplication(int LDLAppID)
        {
            return DVLDDataAccessLayer.clsManageApplication.DeleteLocalDrivingLicenseApplication(LDLAppID);
        }

        public static bool DeleteApplication(int applicationID)
        {
            return DVLDDataAccessLayer.clsManageApplication.DeleteApplication(applicationID);
        }

        public static int passedTests(int DLAppID)
        {
            return DVLDDataAccessLayer.clsManageApplication.PassedTests(DLAppID);
        }

        public static DataTable getTests(int DLAppID, int testTypeID)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveTests(DLAppID,testTypeID);
        }

        public static bool AddNewTestAppointment(int testTypeID,
            int DLAppID, DateTime AppDate, double PaidFess,
            int CreatedByUserID, int isLocked, int RetakeTestAppId = -1)
        {
            return DVLDDataAccessLayer.clsManageApplication
                .AddNewTestAppointment(testTypeID,
                DLAppID, AppDate, PaidFess,
                CreatedByUserID, isLocked,
                RetakeTestAppId);
        }

        public static int GetMaxTestAppointmenIDtForSpecificLDLAppID(int DLAppID,int testTypeID)
        {
            return DVLDDataAccessLayer.clsManageApplication.RetrieveMaxTestAppointmenIDForSpecificLDLAppID(DLAppID,testTypeID);
        }

        public static DataTable getTest(int testAppointment)
        {
           return DVLDDataAccessLayer.clsManageApplication.retrieveTests(testAppointment);
        }

        public static int getTestTrial(int testTypeID, int DLID)
        {
         return DVLDDataAccessLayer.clsManageApplication.retrieveTestTrial(testTypeID, DLID);       
        }

        public static bool addNewTest(int testAppID, int testResult,  string notes ,int createdByUserID)
        {

            return DVLDDataAccessLayer.clsManageApplication.addNewTest(testAppID,testResult,notes ,createdByUserID);

        }

        public static bool lockTheTestAppointment(int testAppID)
        {
            return DVLDDataAccessLayer.clsManageApplication.lockTheTestAppointment(testAppID);
        }

        public static int getMaxRetakeTestApplicationID()
        {
            return DVLDDataAccessLayer.clsManageApplication.retreiveMaxRetakeTestApplicationID();
        }

        public static bool updateTestAppointment(DateTime date, int testAppID)
        {

            return DVLDDataAccessLayer.clsManageApplication.updateTestAppointment(date, testAppID);

        }

        public static bool UpdateApplicationStatus(int applicationID, int statusID)
        {
            return DVLDDataAccessLayer.clsManageApplication.updateApplicationStatus(applicationID, statusID);
        }

    }




}
