using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsManagePeople
    {
        public static DataTable listPeople()
        {
            return DVLDDataAccessLayer.clsManagePeople.RetrievePeople();

        }

        public static DataTable GetPerson(int ID)
        {
            return DVLDDataAccessLayer.clsManagePeople.RetrievePerson(ID);

        }

        public static DataTable GetPerson(string nationalNo)
        {
            return DVLDDataAccessLayer.clsManagePeople.RetrievePerson(nationalNo);

        }

        public static int retreivePersonID(int DriverID)
        {
            return DVLDDataAccessLayer.clsManagePeople.retreivePersonID(DriverID);
        }

       public static string GetPersonFullName(int ID)
        {
            return DVLDDataAccessLayer.clsManagePeople.RetrievePersonFullName(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return DVLDDataAccessLayer.clsManagePeople.isPersonExist(ID);
        }

        public static bool isPersonExist(string NationalNo)
        {
            return DVLDDataAccessLayer.clsManagePeople.isPersonExist(NationalNo);
        }

        public static string GetPersonPicture(int ID)
        {
            return DVLDDataAccessLayer.clsManagePeople.GetPersonImage(ID);
        }

        public static bool AddNewPerson(string firstName, string secondName,
            string thirdName, string lastName, string nationalNo,
            string Email, string phone, int countryID, string address,
            string picturePath, int gender, DateTime birthDay)
        {

            return DVLDDataAccessLayer.clsManagePeople.AddPeople(
                firstName,secondName,thirdName, lastName,
                nationalNo, Email, phone, countryID,
                address,picturePath, gender, birthDay);
        }


        public static bool updatePerson(int ID,string firstName, string secondName,
            string thirdName, string lastName, string nationalNo,
            string Email, string phone, int countryID, string address,
            string picturePath, int gender, DateTime birthDay)
        {
            return DVLDDataAccessLayer.clsManagePeople.UpdatePerson(
                ID,firstName, secondName, thirdName, lastName,
                nationalNo, Email, phone, countryID,
                address, picturePath, gender, birthDay);
        }

        public static DataTable GetAllCountries()
        {

            return DVLDDataAccessLayer.clsManagePeople.retrieveCountries();
        }

        public static int GetCountryID(string CountryName)
        {

            return DVLDDataAccessLayer.clsManagePeople.GetCountryID(CountryName);
        }

        public static string GetCountryName(int ID)
        {

            return DVLDDataAccessLayer.clsManagePeople.GetCountryName(ID);
        }

        public static bool deletePerson(int ID)
        {
            return DVLDDataAccessLayer.clsManagePeople.deletePerson(ID);
        }

        public static int getPersonAge(DateTime birtdate)
        {
            return DVLDDataAccessLayer.clsManagePeople.CalculateAge(birtdate);  

        }


    }
}
