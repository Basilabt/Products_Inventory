using FactWebsiteApp.Models.DataAccess;

using FactWebsiteApp.Models.Utility;
using System.Runtime.CompilerServices;

namespace FactWebsiteApp.Models.Business
{
    public class clsUser
    {

        public enum enMode
        {
            AddNew = 1, Update = 2, Delete = 3
        }

        public int userID { get; set; }
        public int personID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; }

        public string accountStatus
        {
            get
            {
                return (this.isActive) ? "Active" : "Not-Active";
            }
        }
        public enMode mode { get; set; }

        public clsPerson person { get; set; }

        public clsUser()
        {
            this.userID = -1;
            this.personID = -1;
            this.username = "";
            this.password = "";
            this.isActive = false;
            this.mode = enMode.AddNew;
        }

        private clsUser(int userID, int personID, string username, string password, bool isActive)
        {
            this.userID = userID;
            this.personID = personID;
            this.username = username;
            this.password = password;
            this.isActive = isActive;
            this.mode = enMode.Update;
            this.person = clsPerson.getPersonByPersonID(personID);
        }


        public bool save()
        {
            switch (this.mode)
            {
                case enMode.AddNew:
                    {
                        this.userID = addNewUser(this.personID,this.username,clsEncryptor.ComputeHash(this.password), this.isActive);
                        return this.userID != -1;
                    }

                case enMode.Update:
                    {
                        return updateUser(this.userID,this.personID,this.username,clsEncryptor.ComputeHash(this.password),this.isActive);
                    }

                case enMode.Delete:
                    {   
                        return deleteUserByUserID(this.userID);
                    }

            }

            return false;
        }

        public static bool login(string username, string password)
        {
            int userID = -1, personID = -1;
            bool isActive = false;

            string hashedPassword = clsEncryptor.ComputeHash(password);
           

            if (clsUserDataAccess.login(ref userID, ref personID, username, hashedPassword, ref isActive))
            {
                clsGlobal.loggedUser = new clsUser(userID, personID, username, hashedPassword, isActive);

                return true;
            }

            return false;
        }

        public static bool doesUserExist(string username)
        {
            return clsUserDataAccess.doesUserExist(username);
        }

        public static bool isUserActive(string username)
        {
            return clsUserDataAccess.isUserActive(username);
        }

        public static int addNewUser(int personID , string username , string password , bool isActive)
        {
            return clsUserDataAccess.addNewUser(personID , username , password , isActive);
        }

        public static bool deleteUserByUserID(int userID)
        {
              return clsUserDataAccess.deleteUserByUserID(userID);
        }

        public static bool deleteUserByUsername(string username)
        {
            return clsUserDataAccess.deleteUserByUsername(username);
        }

        public static bool updateUser(int userID , int personID , string username , string password , bool isActive)
        {
            return clsUserDataAccess.updateUserByUserID(userID,personID,username,password,isActive);
        }

    }
}
