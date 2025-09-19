using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDriversAndLicenses
    {
        static public bool isPersonADriver(int personID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.isPersonADriver(personID);
        }

        static public bool isDriverHasTheLicense(int LicenseID, int DriverID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.isDriverHasTheLicense(LicenseID, DriverID);
        }

        static public bool isDriverHasTheInternationalLicense(int DriverID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.isDriverHasTheInternationalLicense(DriverID);
        }

        static public int getDriverID(int PersonID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.retreiveDriverID(PersonID);
        }

        static public bool addNewDriver(int personID, int createdByUserID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.addNewDriver(personID, createdByUserID);
        }

        static public bool IssueLicense(int applicationID,
    int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate,
    string notes, double paidFees, bool isActive, int IssueReason,
    int createdByUserID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.
         issueLicense
         (
         applicationID,
         DriverID,
         LicenseClass,
         IssueDate,
         ExpirationDate,
         notes,
         paidFees,
         isActive,
         IssueReason,
         createdByUserID
        );
        }

        static public bool issueInterNationalLicense(int applicationID,
     int DriverID, int LDLAppID, DateTime IssueDate, DateTime ExpirationDate,
     bool isActive, int createdByUserID)
        {

            return DVLDDataAccessLayer.clsDriversAndLicenses.issueInterNationalLicense(applicationID,
                DriverID,
                LDLAppID,
                IssueDate,
                ExpirationDate,
                isActive,
                createdByUserID);
        }

       public static bool renewLicense(int OldLicenseID, int ApplicationID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.renewLicense(OldLicenseID, ApplicationID);
        }

        public static bool ReplaceLicense(int OldLicenseID, int ApplicationID,
           int replacementType)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.ReplaceLicense(OldLicenseID,
                ApplicationID, replacementType);
        }

        public static DataTable RetrieveDriverLicenseInfo(int LDLAppID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveDriverLicenseInfo(LDLAppID);
        }

        public static bool isLicenseDetained(int licenseID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.isLicenseDetained(licenseID);
        }

        public static DataTable retrieveDrivers()
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.retrieveDrivers(); 
        }

        public static DataTable RetrieveLicenseHistory(int LDLAppID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveLicenseHistory(LDLAppID);
        }

        public static DataTable RetrieveInternationalLicenseHistory(int DriverID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveInternationalLicenseHistory(DriverID);
        }

        public static DataTable RetrieveInternationalLicense()
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveInternationalLicense();

        }

        public static DataTable RetrieveLicense(int LicenseID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveLicense(LicenseID);
        }

        public static DataTable RetrieveAllImportantInternationalLicenseInfo(int DriverID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveAllImportantInternationalLicenseInfo(DriverID);
        }

        public static double RetrieveLicenseClassFees(int LicenseClassID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveLicenseClassFees((int) LicenseClassID);
        }

        public static int RetrieveLicenseClassValidityLength(int LicenseClassID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveLicenseClassValidityLength((int)LicenseClassID);

        }

        public static int RetrieveLicenseID(int LDLAppID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveLicenseID(LDLAppID);
        }

        public static int RetrieveInternationalLicenseID(int AppID)
        {
           return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveInternationalLicenseID( AppID);
        }

        public static int retreiveLDLAppID(int LicenseID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.retreiveLDLAppID( LicenseID);
        }

        public static bool isLicenseActive(int licenseID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.isLicenseActive(licenseID);
        }

        public static bool isLicenseExpired(int licenseID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.isLicenseExpired(licenseID);
        }

        public static bool isLicenseExist(int licenseID)
        {

            return DVLDDataAccessLayer.clsDriversAndLicenses.isLicenseExist(licenseID);
        }

        public static bool detainLicense(int licenseID, DateTime detainDate,
            double FineFees, int createdByUserID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.detainLicense(licenseID,
                detainDate, FineFees, createdByUserID);
        }

        public static bool releaseLicense(int licenseID, DateTime ReleaseDate,
          int ApplicationID, int releasedByUserID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.releaseLicense(licenseID,
                ReleaseDate, ApplicationID, releasedByUserID);
        }

        public static int retrieveDetainID(int LicenseID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.retrieveDetainID(LicenseID);
        }

        public static DataTable RetrieveDetainedLicenseInfo(int licenseID)
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveDetainedLicenseInfo(licenseID);

        }

        public static DataTable RetrieveDetainedLicenses()
        {
            return DVLDDataAccessLayer.clsDriversAndLicenses.RetrieveDetainedLicenses();
        }

    }
}
