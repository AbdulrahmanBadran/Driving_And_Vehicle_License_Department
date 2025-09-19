using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class clsDriversAndLicenses
    {
        static public bool isPersonADriver(int personID)
        {            
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select 1 from drivers 
                            where PersonID=@personID";

            SqlCommand command=new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personID", personID);

            object rows = 0;

            try
            {
                connection.Open();

                if((rows= command.ExecuteScalar())!=null)
                {
                    rows = (int)rows;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return rows != null;

        }

        static public bool isDriverHasTheLicense(int LicenseID,int DriverID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select top 1 1 from Licenses
                where DriverID=@DriverID and LicenseClass=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            object rows = 0;

            try
            {
                connection.Open();

                if ((rows = command.ExecuteScalar()) != null)
                {
                    rows = (int)rows;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return rows != null;


        }

        static public bool isDriverHasTheInternationalLicense(int DriverID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select 1 from InternationalLicenses
                            where DriverID=@DriverID and IsActive=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            object rows = 0;

            try
            {
                connection.Open();

                if ((rows = command.ExecuteScalar()) != null)
                {
                    rows = (int)rows;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return rows != null;
        }

        static public int retreiveDriverID(int PersonID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select DriverID from Drivers
                            where PersonID=@PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            object ID = -1;

            try
            {
                connection.Open();

                if ((ID = command.ExecuteScalar()) != null)
                {
                    ID = (int)ID;
                }
                else
                {
                    ID = -1;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return (int)ID;
        }


        public static int RetrieveInternationalLicenseID(int AppID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select InternationalLicenseID
                            from InternationalLicenses
                        where ApplicationID=@AppID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppID", AppID);

            object ID = -1;

            try
            {
                connection.Open();

                if ((ID = command.ExecuteScalar()) != null)
                {
                    ID = (int)ID;
                }
                else
                {
                    ID = -1;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return (int)ID;

        }

        static public bool addNewDriver(int personID,int createdByUserID)
        {
            int affectedRows = 0;

            SqlConnection connection=new SqlConnection(DataAccessSettings.connectionString());

            string query = @"INSERT INTO [Drivers]
           ([PersonID]
           ,[CreatedByUserID]
           ,[CreatedDate])
     VALUES
           (@personID
           ,@createdByUserID
           ,CAST(GETDATE() AS DATE));";

        SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID",personID);
            command.Parameters.AddWithValue("@createdByUserID", createdByUserID);

            try
            {
                connection.Open();

                affectedRows = command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return affectedRows > 0;

        }

        static public bool DeActiveLicense(int licenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"UPDATE [Licenses]
                            SET [IsActive] = 0
                         WHERE LicenseID=@licenseID;" ;

            SqlCommand command=new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@licenseID", licenseID);

            int AffectedRows = 0;

            try
            {
                connection.Open();

                AffectedRows = command.ExecuteNonQuery();


            }
            catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            finally
            {
                connection.Close();
            }

            return AffectedRows > 0;

        }


        static public bool issueLicense(int applicationID,
     int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate,
     string notes, double paidFees, bool isActive, int IssueReason,
     int createdByUserID)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"INSERT INTO [Licenses]
        ([ApplicationID]
        ,[DriverID]
        ,[LicenseClass]
        ,[IssueDate]
        ,[ExpirationDate]
        ,[Notes]
        ,[PaidFees]
        ,[IsActive]
        ,[IssueReason]
        ,[CreatedByUserID])
    VALUES
        (@applicationID
        ,@driverID
        ,@licenseClass
        ,@issueDate
        ,@expirationDate
        ,@notes
        ,@paidFees
        ,@isActive
        ,@issueReason
        ,@createdByUserID);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", applicationID);
            command.Parameters.AddWithValue("@driverID", DriverID);
            command.Parameters.AddWithValue("@licenseClass", LicenseClass);
            command.Parameters.AddWithValue("@issueDate", IssueDate);
            command.Parameters.AddWithValue("@expirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@notes", notes);
            command.Parameters.AddWithValue("@paidFees", paidFees);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@issueReason", IssueReason);
            command.Parameters.AddWithValue("@createdByUserID", createdByUserID);

            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return affectedRows > 0;
        }

        public static bool renewLicense(int OldLicenseID,int ApplicationID)
        {

            DataTable OldLicense = RetrieveLicense(OldLicenseID);

            

            int DriverID = Convert.ToInt32(OldLicense.Rows[0]["DriverID"]);
            int LicenseClass = Convert.ToInt32(OldLicense.Rows[0]["LicenseClass"]);
            DateTime IssueDate = DateTime.Now;

            int ValidityLength= RetrieveLicenseClassValidityLength(LicenseClass);

            DateTime ExpirationDate = IssueDate.AddYears(ValidityLength);

            string notes= Convert.ToString(OldLicense.Rows[0]["Notes"]);

            double paidFees= RetrieveLicenseClassFees(LicenseClass);

            int CreatedByUserID= Convert.ToInt32(OldLicense.Rows[0]["CreatedByUserID"]);

            if (DVLDDataAccessLayer.clsManageApplication.addNewLocalDrivingLicenseApplication(ApplicationID, LicenseClass))
            {
                if (issueLicense(ApplicationID, DriverID, LicenseClass, IssueDate,
                ExpirationDate, notes, paidFees, true, 2, CreatedByUserID))
                {
                    if (DeActiveLicense(OldLicenseID))
                    {
                        return true;
                    }


                }
            }

            

            return false;

        }


        public static bool ReplaceLicense(int OldLicenseID, int ApplicationID,
            int replacementType)
        {

            DataTable OldLicense = RetrieveLicense(OldLicenseID);



            int DriverID = Convert.ToInt32(OldLicense.Rows[0]["DriverID"]);
            int LicenseClass = Convert.ToInt32(OldLicense.Rows[0]["LicenseClass"]);
            DateTime IssueDate = Convert.ToDateTime(OldLicense.Rows[0]["IssueDate"]);

            DateTime ExpirationDate = Convert.ToDateTime(OldLicense.Rows[0]["ExpirationDate"]);

            string notes = Convert.ToString(OldLicense.Rows[0]["Notes"]);

            double paidFees = RetrieveLicenseClassFees(LicenseClass);

            int CreatedByUserID = Convert.ToInt32(OldLicense.Rows[0]["CreatedByUserID"]);

            int issueReason;
            if (replacementType == 3)// cuz the issue reason for the lost license is 4 in license table but 3 in applicationType table 
                issueReason = 4;
            else
                issueReason= 3;  // the damaged license is 3 in license table and 4 in applicationType table

            if (DVLDDataAccessLayer.clsManageApplication.addNewLocalDrivingLicenseApplication(ApplicationID, LicenseClass))
            {
                if (issueLicense(ApplicationID, DriverID, LicenseClass, IssueDate,
                ExpirationDate, notes, paidFees, true, issueReason, CreatedByUserID))
                {
                    if (DeActiveLicense(OldLicenseID))
                    {
                        return true;
                    }


                }
            }
            return false;
        }

        static public bool issueInterNationalLicense(int applicationID,
     int DriverID, int LDLAppID, DateTime IssueDate, DateTime ExpirationDate,
     bool isActive,int createdByUserID)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"INSERT INTO [InternationalLicenses]
           ([ApplicationID]
           ,[DriverID]
           ,[IssuedUsingLocalLicenseID]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[IsActive]
           ,[CreatedByUserID])
     VALUES
           (@applicationID
           ,@DriverID
           ,@LDLAppID
           ,@IssueDate
           ,@ExpirationDate
           ,@isActive
           ,@createdByUserID);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", applicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@createdByUserID", createdByUserID);

            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return affectedRows > 0;
        }

        public static DataTable RetrieveDriverLicenseInfo(int LDLAppID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"SELECT LicenseClasses.ClassName, People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName AS fullName, Licenses.LicenseID, People.Gendor, Licenses.IssueDate, Licenses.IssueReason, Licenses.Notes, 
                  Licenses.IsActive, People.DateOfBirth, Drivers.DriverID, Licenses.ExpirationDate, People.ImagePath, People.NationalNo
FROM     LicenseClasses INNER JOIN
                  Licenses ON LicenseClasses.LicenseClassID = Licenses.LicenseClass INNER JOIN
                  Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN
                  People ON Drivers.PersonID = People.PersonID
WHERE  (Licenses.LicenseID =
                      (SELECT LicenseID
                       FROM      Licenses AS Licenses_1
                       WHERE   (ApplicationID =
                                             (SELECT ApplicationID
                                              FROM      Applications
                                              WHERE   (ApplicationID =
                                                                    (SELECT ApplicationID
                                                                     FROM      LocalDrivingLicenseApplications
                                                                     WHERE   (LocalDrivingLicenseApplicationID = @LDLAppID)))))));";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            DataTable DriverLicenseInfo = new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    DriverLicenseInfo.Load(reader);
                }




            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return DriverLicenseInfo;





        }

        public static DataTable RetrieveLicense(int LicenseID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from licenses
                            where licenseID=@LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            DataTable license = new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    license.Load(reader);
                }




            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return license;





        }

        public static double RetrieveLicenseClassFees(int LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"select ClassFees from LicenseClasses
                            where LicenseClassID=@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            object Fees = 0;
            try
            {
                connection.Open();

                Fees = (command.ExecuteScalar());

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return Convert.ToDouble(Fees);


        }

        public static int RetrieveLicenseClassValidityLength(int LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"select DefaultValidityLength from LicenseClasses
                            where LicenseClassID=@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            object Fees = 0;
            try
            {
                connection.Open();

                Fees = (command.ExecuteScalar());

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return Convert.ToInt32(Fees);


        }

        public static bool isLicenseDetained(int licenseID)
        {
            object result = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select 1 from DetainedLicenses
                        where licenseID=@licenseID and isReleased=0;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@licenseID", licenseID);

            try
            {
                connection.Open();

                result = command.ExecuteScalar();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result != null;
        }

        public static bool isLicenseActive(int licenseID)
        {

            
                object result = null;
                SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

                string query = @"select 1 from licenses
                            where licenseID=@licenseID and isActive=1;";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@licenseID", licenseID);

                try
                {
                    connection.Open();

                    result = command.ExecuteScalar();


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return result != null;
            
        }

        public static bool isLicenseExpired(int licenseID)
        {
            object result = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select 1 from licenses
       where licenseID=@licenseID and ExpirationDate <= (SELECT CAST(GETDATE() AS DATE));";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@licenseID", licenseID);

            try
            {
                connection.Open();

                result = command.ExecuteScalar();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result != null;
        }

        public static bool isLicenseExist(int licenseID)
        {
            object result = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select 1 from licenses
                            where licenseID=@licenseID ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@licenseID", licenseID);

            try
            {
                connection.Open();

                result = command.ExecuteScalar();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result != null;
        }

        public static DataTable retrieveDrivers()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select D.DriverID
                            ,P.PersonID
                            ,NationalNo,
                FirstName+' '+SecondName+' '+ThirdName+' '+LastName as FullName,
                            CreatedDate as Date,IsActive
                            from Drivers D
                            Inner Join People P
                            On D.PersonID=P.PersonID 
                            Inner join Licenses L
                            On D.DriverID=L.DriverID;
                            ";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Drivers = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Drivers.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return Drivers;


        }

        public static DataTable RetrieveLicenseHistory(int LDLAppID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select LicenseID,ApplicationID,ClassName,IssueDate,ExpirationDate,IsActive
                            from Licenses L
                            Inner join LicenseClasses LC
                             ON L.LicenseClass=LC.LicenseClassID

where driverID=(select DriverID from Drivers
				where personID=(select ApplicantPersonID from Applications
								where applicationID=(
										select ApplicationID from LocalDrivingLicenseApplications
												where LocalDrivingLicenseApplicationID=@LDLAppID
										)))";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            DataTable LicenseHistory = new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    LicenseHistory.Load(reader);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return LicenseHistory;

        }

        public static DataTable RetrieveInternationalLicenseHistory(int DriverID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select InternationalLicenseID AS [Int.License ID]
                            ,ApplicationID As [Application ID]
                            ,IssuedUsingLocalLicenseID as [L.License ID]
                            ,IssueDate as [Issue Date]
                            ,ExpirationDate as [Expiration Date]
                            ,IsActive as [Is Active]
                            	from InternationalLicenses
                            where DriverID=@DriverID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            DataTable LicenseHistory = new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    LicenseHistory.Load(reader);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return LicenseHistory;

        }

        public static DataTable RetrieveInternationalLicense()
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select InternationalLicenseID AS [Int.License ID]
                            ,ApplicationID As [Application ID]
                            ,IssuedUsingLocalLicenseID as [L.License ID]  
                            ,IssueDate as [Issue Date]
                            ,ExpirationDate as [Expiration Date]
                            ,IsActive as [Is Active]
                            from InternationalLicenses";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Licenses = new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Licenses.Load(reader);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return Licenses;

        }

        public static DataTable RetrieveAllImportantInternationalLicenseInfo(int DriverID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"SELECT People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName AS FullName
, Licenses.LicenseID
, People.NationalNo
, People.Gendor
, InternationalLicenses.IsActive
, People.DateOfBirth
,
InternationalLicenses.DriverID
, InternationalLicenses.ExpirationDate
, InternationalLicenses.ApplicationID
, InternationalLicenses.InternationalLicenseID
, InternationalLicenses.IssueDate
, People.ImagePath
FROM     InternationalLicenses INNER JOIN
                  Licenses ON InternationalLicenses.IssuedUsingLocalLicenseID = Licenses.LicenseID INNER JOIN
                  Drivers ON InternationalLicenses.DriverID = Drivers.DriverID AND Licenses.DriverID = Drivers.DriverID INNER JOIN
                  People ON Drivers.PersonID = People.PersonID  
				  where InternationalLicenses.DriverID=@DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            DataTable License = new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    License.Load(reader);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return License;

        }

        public static int RetrieveLicenseID(int LDLAppID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"
                    SELECT LicenseID FROM  Licenses
                            WHERE(ApplicationID =
                                        (SELECT ApplicationID
                                                FROM LocalDrivingLicenseApplications
                                                        WHERE(LocalDrivingLicenseApplicationID = @LDLAppID)));";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            object ID = -1;

            try
            {
                connection.Open();

                if ((ID = command.ExecuteScalar()) != null)
                {
                    ID = (int)ID;
                }
                else
                {
                    ID = -1;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return (int)ID;

        }

        public static int retreiveLDLAppID(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"SELECT LocalDrivingLicenseApplicationID
 FROM  LocalDrivingLicenseApplications
	WHERE ApplicationID = 
		(SELECT ApplicationID
			FROM  Applications
              WHERE ApplicationID =  
			  (SELECT ApplicationID
				   FROM  Licenses
					 WHERE LicenseID =@LicenseID));";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            object ID = -1;

            try
            {
                connection.Open();

                if ((ID = command.ExecuteScalar()) != null)
                {
                    ID = (int)ID;
                }
                else
                {
                    ID = -1;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return (int)ID;


        }



        public static bool detainLicense(int licenseID,DateTime detainDate,
            double FineFees,int createdByUserID)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"INSERT INTO [dbo].[DetainedLicenses]
           ([LicenseID]
           ,[DetainDate]
           ,[FineFees]
           ,[CreatedByUserID]
           ,[IsReleased]
           ,[ReleaseDate]
           ,[ReleasedByUserID]
           ,[ReleaseApplicationID])
     VALUES
           (@licenseID
           ,@detainDate
           ,@FineFees
           ,@createdByUserID
           ,0
           ,null
           ,null
           ,null);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseID", licenseID);
            command.Parameters.AddWithValue("@detainDate", detainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@createdByUserID", createdByUserID);
           

            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return affectedRows > 0;


        }



        public static bool releaseLicense(int licenseID, DateTime ReleaseDate,
           int ApplicationID, int releasedByUserID)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"UPDATE [dbo].[DetainedLicenses]
                            SET [IsReleased] = 1
                           ,[ReleaseDate] = @ReleaseDate
                           ,[ReleasedByUserID] = @releasedByUserID
                           ,[ReleaseApplicationID] = @ApplicationID
                            WHERE LicenseID=@licenseID and IsReleased=0";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseID", licenseID);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@releasedByUserID", releasedByUserID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return affectedRows > 0;


        }

        public static int retrieveDetainID(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from DetainedLicenses
                    where licenseID=@LicenseID And IsReleased=0;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            object ID = -1;

            try
            {
                connection.Open();

                if ((ID = command.ExecuteScalar()) != null)
                {
                    ID = (int)ID;
                }
                else
                {
                    ID = -1;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return (int)ID;

        }

        public static DataTable RetrieveDetainedLicenseInfo(int licenseID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from DetainedLicenses
                        where licenseID=@licenseID And IsReleased=0";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseID", licenseID);

            DataTable Licenses = new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Licenses.Load(reader);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return Licenses;

        }

        public static DataTable RetrieveDetainedLicenses()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from DetainedLicenses_View";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable DetainedLicenses = new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    DetainedLicenses.Load(reader);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return DetainedLicenses;
        }

    }
}
