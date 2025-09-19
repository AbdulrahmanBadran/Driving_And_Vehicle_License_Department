using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsManagePeople
    {

        public static DataTable RetrievePeople()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"SELECT People.PersonID,
                            People.NationalNo, People.FirstName,
                            People.SecondName, People.ThirdName,
                            People.LastName, People.DateOfBirth, 
                            People.Gendor, People.Address,
                            People.Phone,People.Email,
                            Countries.CountryName,
                            People.ImagePath 
                  
                        FROM     People INNER JOIN
Countries ON People.NationalityCountryID = Countries.CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable People = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    People.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return People;
        }

        public static DataTable RetrievePerson(int ID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from People
                             where PersonID=@ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            DataTable Person = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Person.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return Person;
        }

        public static DataTable RetrievePerson(string nationalNo)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from people
                              where NationalNo=@nationalNo;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalNo", nationalNo);

            DataTable Person = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Person.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return Person;
        }

        public static int retreivePersonID(int DriverID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select PersonID from drivers where DriverID=@DriverID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static string RetrievePersonFullName(int ID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select FirstName +' ' + SecondName+' ' + ThirdName+ ' '+ LastName as FullName
						from People
						where personID=@ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            object personName="";

            try
            {
                connection.Open();

               personName = command.ExecuteScalar();


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return Convert.ToString(personName);

        }
            


        public static bool isPersonExist(int ID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            bool isExist = false;

            string query = @"select 1 from people
                            where PersonID=@ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isExist = reader.HasRows;

            }
            catch (Exception err)
            {

            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }

        public static bool isPersonExist(string nationalNo)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            bool isExist = false;

            string query = @"select 1 from people
                            where NationalNo=@nationalNo;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalNo", nationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isExist = reader.HasRows;

            }
            catch (Exception err)
            {

            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }

        public static string GetPersonImage(int ID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string path = "";

            string query = @"select ImagePath from people
                            where personID=@ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                path = command.ExecuteScalar().ToString();



            }
            catch (Exception err)
            {

            }
            finally
            {
                connection.Close();
            }

            return path;
        }

        public static bool AddPeople(string firstName, string secondName,
            string thirdName, string lastName, string nationalNo,
            string Email, string phone, int countryID, string address,
            string picturePath, int gender, DateTime birthDay)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"INSERT INTO [People]
           ([NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[DateOfBirth]
           ,[Gendor]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityCountryID]
           ,[ImagePath])
     VALUES
           (@nationalNo
           ,@firstName
           ,@secondName
           ,@thirdName
           ,@lastName
           ,@birthDay
           ,@gender
           ,@address
           ,@phone
           ,@Email
           ,@countryID
           ,@picturePath)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalNo", nationalNo);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@secondName", secondName);
            command.Parameters.AddWithValue("@thirdName", thirdName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@birthDay", birthDay);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@countryID", countryID);
            command.Parameters.AddWithValue("@picturePath", picturePath);


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

            return affectedRows > 0; ;

        }

        public static bool UpdatePerson(int ID, string firstName, string secondName,
            string thirdName, string lastName, string nationalNo,
            string Email, string phone, int countryID, string address,
            string picturePath, int gender, DateTime birthDay)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @" UPDATE[People]
    SET[NationalNo] = @nationalNo,
    [FirstName] = @firstName,
    [SecondName] = @secondName,
    [ThirdName] = @thirdName,
    [LastName] = @lastName,
    [DateOfBirth] = @birthDay,
    [Gendor] = @gender,
    [Address] = @address,
    [Phone] = @phone,
    [Email] = @Email,
    [NationalityCountryID] = @countryID,
    [ImagePath] = @picturePath
WHERE PersonID = @ID;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalNo", nationalNo);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@secondName", secondName);
            command.Parameters.AddWithValue("@thirdName", thirdName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@birthDay", birthDay);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@countryID", countryID);
            command.Parameters.AddWithValue("@picturePath", picturePath);
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

        public static DataTable retrieveCountries()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = "select CountryName from Countries";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Countries = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Countries.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return Countries;

        }

        public static int GetCountryID(string CountryName)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            int countryID = -1;

            string query = @"select CountryID from Countries
                        where CountryName=@CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                countryID = Convert.ToInt32(command.ExecuteScalar());

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return countryID;
        }

        public static string GetCountryName(int ID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string countryName = "";

            string query = @"select CountryName from Countries
 where                          CountryId=@ID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                countryName = Convert.ToString(command.ExecuteScalar());

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return countryName;
        }

        public static bool deletePerson(int ID)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"DELETE FROM [People]
                             WHERE PersonID=@ID; ";

            SqlCommand command = new SqlCommand(query, connection);

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

        public static int CalculateAge(DateTime birthDate)
        {
            int age = Convert.ToInt32(DateTime.Now.Year - birthDate.Year);

            if (DateTime.Now.Month > birthDate.Month)
            {
                return age;
            }

            if (DateTime.Now.Month == birthDate.Month)
            {
                if (DateTime.Now.Day >= birthDate.Day)
                {
                    return age;
                }
            }


            return --age;
        }
    }
}
