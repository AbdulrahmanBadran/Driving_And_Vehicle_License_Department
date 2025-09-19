using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Policy;

namespace DVLDDataAccessLayer
{
    public class clsManageUsers
    {
        public static DataTable RetrieveUsers()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select users.UserID,users.PersonID 
,(people.FirstName + ' '+ people.SecondName + ' ' +people.thirdName + ' ' +people.lastName) as FullName,
users.UserName,users.Password, users.IsActive
from users
Inner join people On users.PersonID=People.PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable Users = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Users.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return Users;
        }

        public static DataTable RetrieveUser(int UserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from users
                            where UserID=@UserID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserId",UserID);

            DataTable User = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    User.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return User;
        }

        public static DataTable RetrieveUser(string UserName)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"select * from users
                where    UserName=@UserName;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            DataTable User = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    User.Load(reader);
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return User;
        }

        public static bool updateUser(int UserID,string UserName,short isActive)

        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"UPDATE [Users]
                SET 
                [UserName] = @UserName
               ,[IsActive] = @isActive
                WHERE UserID=@UserID";


            SqlCommand command = new SqlCommand(query, connection);

          
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@UserID", UserID);


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

        public static bool updateUserPassword(int UserID,string password)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"UPDATE [Users]
                            SET [Password] = @password
                            WHERE userID=@UserID";


            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@UserID", UserID);


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

        public static bool deletePerson(int UserID)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            string query = @"DELETE FROM [Users]
                             WHERE userID=@UserID; ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);


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
        public static bool AddNewUser(int personID,
            string username,string password,short isActive)
        {
            int affectedRows = 0;
            SqlConnection connection=new SqlConnection(DataAccessSettings.connectionString());

            string query = @"INSERT INTO [Users]
                            ([PersonID]
                            ,[UserName]
                            ,[Password]
                            ,[IsActive])
                        VALUES
                             (@personID,
                              @username,
                              @password,
                              @isActive)";

            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", personID);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@isActive", isActive);


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
            return affectedRows>0;
        }

        public static bool isPersonExist(int personID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            bool isExist = false;

            string query = @"select 1 from users
                           where personId=@personID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personID", personID);

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

        public static bool isUserExist(string username,string password)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            bool isExist = false;

            string query = @"select 1 from users
        where UserName=@username and Password=@password;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

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

        public static bool isUserNameExist(string username)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString());

            bool isExist = false;

            string query = @"select 1 from users
                            where UserName=@username;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);

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



    }
}
