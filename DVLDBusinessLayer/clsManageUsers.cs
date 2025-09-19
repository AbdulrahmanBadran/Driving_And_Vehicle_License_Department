using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsManageUsers
    {

        public static DataTable retrieveUsers()
        {
            return DVLDDataAccessLayer.clsManageUsers.RetrieveUsers();

        }

        public static DataTable retrieveUser(int userID)
        {
            return DVLDDataAccessLayer.clsManageUsers.RetrieveUser(userID);

        }

        public static DataTable retrieveUser(string userName)
        {
            return DVLDDataAccessLayer.clsManageUsers.RetrieveUser(userName);

        }

        public static bool updateUser(int UserID, string UserName, short isActive)
        {

            return DVLDDataAccessLayer.clsManageUsers.updateUser(UserID, UserName, isActive);   
        }

        public static bool updateUserPassword(int UserID, string Password)
        {

            return DVLDDataAccessLayer.clsManageUsers.updateUserPassword(UserID, Password);
        }

        public static bool IsPersonExist(int PersonID)
        {
            return DVLDDataAccessLayer.clsManageUsers.isPersonExist(PersonID);
        }

        public static bool isUserExist(string username,string password)
        {
            return DVLDDataAccessLayer.clsManageUsers.isUserExist(username, password);  
        }

        public static bool isUserNameExist(string UserName) 
            {
            return DVLDDataAccessLayer.clsManageUsers.isUserNameExist(UserName);  
            }

        public static bool deleteUser(int UserID)
        {
            return DVLDDataAccessLayer.clsManageUsers.deletePerson(UserID);
        }
        public static bool AddNewUser(int personID,
            string username, string password, short isActive)
        {
            return DVLDDataAccessLayer.clsManageUsers.AddNewUser(personID, username, password, isActive);
        }
    }
}
