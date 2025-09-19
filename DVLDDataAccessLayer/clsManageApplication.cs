using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class clsManageApplication
    {
        public static DataTable RetrieveApplicationTypes()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from ApplicationTypes;";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable ApplicationTypes = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    ApplicationTypes.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return ApplicationTypes;
        }

        public static DataTable RetrieveTestTypes()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from TestTypes;";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable TestTypes = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    TestTypes.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return TestTypes;
        }

        public static double RetrieveTestFees(int testTypeID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select TestTypeFees from TestTypes
                            where testTypeID=@testTypeID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@testTypeID", testTypeID);

            object fees = 0;

            try
            {
                connection.Open();

               fees= command.ExecuteScalar();


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return Convert.ToDouble(fees);

        }

        public static double RetrieveApplicationFees(int appTypeID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"select ApplicationFees from ApplicationTypes
                where ApplicationTypeID=@appTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@appTypeID", appTypeID);

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

        public static DataTable RetrieveApplicationType(int ID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"	select * from ApplicationTypes
where ApplicationTypeID=(
select ApplicationTypeID from Applications
							where ApplicationID=@ID);";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            DataTable ApplicationType = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    ApplicationType.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return ApplicationType;
        }

        public static DataTable RetrieveBasicApplicationType(int ID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"	select * from ApplicationTypes
							where ApplicationTypeID=@ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            DataTable ApplicationType = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    ApplicationType.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return ApplicationType;
        }

        public static DataTable RetrieveTest(int AppID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @" select * from tests
				  where TestAppointmentID=@AppID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppID", AppID);

            DataTable Test = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Test.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return Test;
        }

        public static DataTable RetrieveApplicationType(string name)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from ApplicationTypes
                            where ApplicationTypeTitle=@name;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            DataTable ApplicationTypes = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    ApplicationTypes.Load(reader);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return ApplicationTypes;
        }

        public static DataTable RetrieveTestAppointment(int testAppID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from TestAppointments
				             where TestAppointmentID=@testAppID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testAppID", testAppID);

            DataTable TestApp = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    TestApp.Load(reader);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return TestApp;
        }

        public static DataTable RetreiveApplication(int DLAppID)
        {
            SqlConnection connection=new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from Applications
                    where applicationID=(
                        select ApplicationID from
                        LocalDrivingLicenseApplications
                        where LocalDrivingLicenseApplicationID=@LDLAppID
                        )";

            SqlCommand command=new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLAppID", DLAppID);
            DataTable Application = new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    Application.Load(reader);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return Application;
        }

        

        public static DataTable RetrieveTestType(int ID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from TestTypes
                             where TestTypeID=@ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            DataTable TestType = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    TestType.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return TestType;
        }

        public static bool UpdateApplicationType(int ID, string name, double fees)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @" UPDATE [ApplicationTypes]
             SET [ApplicationTypeTitle] = @name
            ,[ApplicationFees] = @fees
                 WHERE ApplicationTypeID=@ID;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@ID", ID);


            try
            {
                connection.Open();


                affectedRows = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return affectedRows > 0;


        }

        public static bool UpdateTestType(int ID, string title, string description, double fees)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"UPDATE [TestTypes]
                   SET [TestTypeTitle] = @title
                   ,[TestTypeDescription] = @description
                   ,[TestTypeFees] =@fees
                 WHERE TestTypeID=@ID;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@ID", ID);


            try
            {
                connection.Open();


                affectedRows = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return affectedRows > 0;


        }

        public static DataTable RetrievrLocalDrivingLicenseApplications()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"SELECT * FROM LDLAppInfoView;"; 

            SqlCommand command = new SqlCommand(query, connection);

            DataTable LocalDrivingLicenses = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    LocalDrivingLicenses.Load(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return LocalDrivingLicenses;
        }

        public static DataTable RetrievrLocalDrivingLicenseApplication(int ID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"select * from LocalDrivingLicenseApplications
                            where LocalDrivingLicenseApplicationID=@ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            DataTable LocalDrivingLicenses = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    LocalDrivingLicenses.Load(reader);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return LocalDrivingLicenses;
        }


        public static DataTable RetrieveLicenseClasses()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"select ClassName from LicenseClasses";
            SqlCommand command = new SqlCommand(query, connection);
            DataTable LicenseClasses = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    LicenseClasses.Load(reader);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return LicenseClasses;
        }

        public static DataTable RetrieveLicenseClass(string name)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"select * from LicenseClasses
                             where ClassName=@name;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);
            DataTable LicenseClasses = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    LicenseClasses.Load(reader);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return LicenseClasses;
        }

        public static DataTable RetrieveLicenseClass(int ID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"select * from LicenseClasses
                             where LicenseClassID=@ID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            DataTable LicenseClasses = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    LicenseClasses.Load(reader);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return LicenseClasses;
        }

         public static int PassedTests(int DLAppID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"select ISNULL(SUM(CAST(TestResult AS INT)),0) from Tests
                            where TestAppointmentID in(
                                select TestAppointmentID 
                                from TestAppointments
                            where LocalDrivingLicenseApplicationID=@LDLAppID
                            )";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", DLAppID);

            object passed = 0;
            try
            {
                connection.Open();

                passed= (command.ExecuteScalar());

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return Convert.ToInt32(passed);


        }


        public static bool addNewApplication(int personID, double paidFees,
        int currentUserID,int ApplicationTypeID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"INSERT INTO [Applications]
                            ([ApplicantPersonID]
                            ,[ApplicationDate]
                            ,[ApplicationTypeID]
                            ,[ApplicationStatus]
                            ,[LastStatusDate]
                            ,[PaidFees]
                            ,[CreatedByUserID])
                        VALUES
                            (@personID
                            ,CAST(GETDATE() AS DATE)
                            ,@ApplicationTypeID 
                            ,1
                            ,CAST(GETDATE() AS DATE)
                            ,@paidFees 
                            ,@currentUserID);";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", personID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@paidFees", paidFees);
            command.Parameters.AddWithValue("@currentUserID", currentUserID);


            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return affectedRows > 0;
        }

        public static bool addNewLocalDrivingLicenseApplication(int applicationID, int licenseClassID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"INSERT INTO [LocalDrivingLicenseApplications]
                            ([ApplicationID]
                            ,[LicenseClassID])
                      VALUES
                            (@applicationID
                            ,@licenseClassID);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationID", applicationID);
            command.Parameters.AddWithValue("@licenseClassID", licenseClassID);

            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return affectedRows > 0;
        }

        public static int getTheLatestApplicationID()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"SELECT TOP 1 ApplicationID FROM Applications ORDER BY ApplicationID DESC;";
            SqlCommand command = new SqlCommand(query, connection);
            int applicationID = -1;
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    applicationID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return applicationID;
        }

        public static int getLastLocalDrivingLicenseApplicationID()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"select top 1 LocalDrivingLicenseApplicationID from LocalDrivingLicenseApplications order by LocalDrivingLicenseApplicationID desc;";

            SqlCommand command = new SqlCommand(query, connection);
            int applicationID = -1;
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    applicationID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return applicationID;
        }

        public static bool isThisApplicationExist(int personID, int licenseID)
        {
            object result = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select 1 from LocalDrivingLicenseApplications
                            where LicenseClassID=@licenseID and ApplicationID in
                            (
                            select ApplicationID from Applications
                            where ApplicantPersonID=@personID and ApplicationStatus=1 ) ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", personID);
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

        public static bool CancelApplication(int applicationID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"UPDATE [Applications]
                            SET [ApplicationStatus] = 2
                            WHERE ApplicationID=@applicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@applicationID", applicationID);
            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return affectedRows > 0;
        }

        public static bool DeleteTests(int LDLAppID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"
            delete from tests                               
                where TestAppointmentID in(
				        select TestAppointmentID from TestAppointments
				                 where LocalDrivingLicenseApplicationID=@LDLAppID);";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return affectedRows > 0;

        }

        public static bool DeleteTestAppointments(int LDLAppID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"delete from TestAppointments                    
                            where LocalDrivingLicenseApplicationID=@LDLAppID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return affectedRows > 0;

        }

        public static bool DeleteLocalDrivingLicenseApplication(int LDLAppID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"DELETE FROM LocalDrivingLicenseApplications     
                                WHERE LocalDrivingLicenseApplicationID=@LDLAppID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return affectedRows > 0;

        }

        public static bool DeleteApplication(int applicationID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"DELETE FROM [Applications]
                            WHERE ApplicationID=@applicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@applicationID", applicationID);
            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return affectedRows > 0;

        }

        public static DataTable RetrieveTests(int DLAppID,int testTypeID)
        {

            SqlConnection connection=new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select
                            TestAppointmentID as [Appointment ID],
                            AppointmentDate as [Appointment Date],
                            PaidFees as [Paid Fees],
                            IsLocked from TestAppointments
          where LocalDrivingLicenseApplicationID=@DLAppID and TestTypeID=@testTypeID;";

            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DLAppID", DLAppID);
            command.Parameters.AddWithValue("@testTypeID", testTypeID);


            DataTable VisionTests =new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                
                    VisionTests.Load(reader);
                




            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return VisionTests;
        }

        public static int RetrieveMaxTestAppointmenIDForSpecificLDLAppID(int DLAppID,int testTypeID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select max(TestAppointmentID)
                            from TestAppointments
                  where LocalDrivingLicenseApplicationID=@DLAppID and TestTypeID=@testTypeID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DLAppID", DLAppID);
            command.Parameters.AddWithValue("@testTypeID", testTypeID);


            object maxID = -1;
            try
            {
                connection.Open();

                maxID = Convert.ToInt32(command.ExecuteScalar());

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return Convert.ToInt32(maxID);
        }


        public static bool AddNewTestAppointment(int testTypeID,
            int DLAppID,DateTime AppDate,double PaidFess,
            int CreatedByUserID,int isLocked,int RetakeTestAppId=-1)
        {

            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"Insert Into [TestAppointments]
           ([TestTypeID]
           ,[LocalDrivingLicenseApplicationID]
           ,[AppointmentDate]
           ,[PaidFees]
           ,[CreatedByUserID]
           ,[IsLocked]
           ,[RetakeTestApplicationID])
     VALUES
           (@testTypeID
           ,@DLAppID
           ,@AppDate
           ,@PaidFess
           ,@CreatedByUserID
           ,@isLocked
           ,@RetakeTestAppId);";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeID", testTypeID);
            command.Parameters.AddWithValue("@DLAppID", DLAppID);
            command.Parameters.AddWithValue("@AppDate", AppDate);
            command.Parameters.AddWithValue("@PaidFess", PaidFess);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@isLocked", isLocked);
            if(RetakeTestAppId==-1)
            command.Parameters.AddWithValue("@RetakeTestAppId", DBNull.Value);
            else
              {
                DataTable application = RetreiveApplication(DLAppID);

                int personID = Convert.ToInt32(application.Rows[0]["ApplicantPersonID"]);

                addNewApplication(personID, PaidFess, CreatedByUserID, 7);
                
                command.Parameters.AddWithValue("@RetakeTestAppId", RetakeTestAppId);
            
            }

            

            try
            {
                    connection.Open();
                    affectedRows = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);

                }
                finally
                {
                    connection.Close();
                }
            return affectedRows > 0;

        }

        public static bool addNewTest(int testAppID,int testResult ,string notes,int createdByUserID)
        {

            int affectedRows = 0;
            int testID = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"INSERT INTO [Tests]
                        ([TestAppointmentID]
                        ,[TestResult]
                        ,[Notes]
                        ,[CreatedByUserID])
                    VALUES
                        (@testAppID
                        ,@testResult
                        ,@notes
                        ,@createdByUserID); ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@testAppID", testAppID);
            command.Parameters.AddWithValue("@testResult", testResult);
            command.Parameters.AddWithValue("@notes", notes);
            command.Parameters.AddWithValue("@createdByUserID", createdByUserID);



            try
            {
                connection.Open();
               affectedRows= command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return  affectedRows>0;

        }

        public static DataTable retrieveTests(int testAppointmentID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from tests
                        where TestAppointmentID=@testAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);

            DataTable Tests = new DataTable();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Tests.Load(reader);
                }




            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return Tests;

        }

        public static int retrieveTestTrial(int testTypeID,int DLID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select Count(*) from tests
                             where TestAppointmentID in (
                        select TestAppointmentID from TestAppointments
            where LocalDrivingLicenseApplicationID=@DLID and TestTypeID=@testTypeID
                );";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DLID", DLID);
            command.Parameters.AddWithValue("@testTypeID", testTypeID);


            object trials = 0;
            try
            {
                connection.Open();

                trials=Convert.ToInt32(command.ExecuteScalar());

            }
            catch (Exception ex)
            {    
                Console.WriteLine(ex.ToString());
               
            }
            finally
            {
                connection.Close();
            }

            return Convert.ToInt32(trials);
        }

        public static bool lockTheTestAppointment(int testAppID)
        {

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"UPDATE [TestAppointments]
                     SET 
                         [IsLocked] = 1
                        WHERE TestAppointmentID=@testAppID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@testAppID", testAppID);



            int affectedRows = 0;
            try
            {
                connection.Open();

                affectedRows=command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return affectedRows > 0;

        }

        public static int retreiveMaxRetakeTestApplicationID()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select max(ApplicationID) from Applications
WHERE                       ApplicationTypeID = 7;";

            SqlCommand command = new SqlCommand(query, connection);

            object maxID = -1;
            try
            {
                connection.Open();

                maxID = Convert.ToInt32(command.ExecuteScalar());

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return Convert.ToInt32(maxID);


        }

        public static bool updateTestAppointment(DateTime date,int testAppID)
        {

            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"UPDATE [dbo].[TestAppointments]
                            SET  [AppointmentDate] = @date
                        WHERE TestAppointmentID=@testAppID;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@testAppID", testAppID);


            try
            {
                connection.Open();


                affectedRows = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return affectedRows > 0;


        }

        public static bool updateApplicationStatus(int applicationID, int statusID)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());
            string query = @"UPDATE [Applications]
                            SET [ApplicationStatus] = @statusID
                            WHERE ApplicationID=@applicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@statusID", statusID);
            command.Parameters.AddWithValue("@applicationID", applicationID);

            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return affectedRows > 0;

        }

        


    }



}
