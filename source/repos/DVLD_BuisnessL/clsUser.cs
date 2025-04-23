using DVLD_DataAccessL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DVLD_BuisnessL
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUser()
        {

            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;

            Mode = enMode.AddNew;
        }
        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
           
            string HashingPassword = ComputeHash(this.Password);

            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, HashingPassword,
                this.IsActive);

            return this.UserID != -1;
        }

        private bool _UpdateUser()
        {
          
            string HashingPassword = ComputeHash(this.Password);

            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, HashingPassword, this.IsActive);
        }

        public static clsUser FindUserByUserID(int UserID)
        {
            int PersonID = -1;
            bool IsActive = false;
            string UserName = "", Password = "";

            if (clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindUserByPersonID(int PersonID)
        {
            int UserID = -1;
            bool IsActive = false;
            string UserName = "", Password = "";

            if (clsUserData.GetUserInfoByUserID(UserID,ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindUserByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;
            bool IsActive = false;

           
            string HashingPassword = ComputeHash(Password);

            if (clsUserData.GetUserInfoByUserNameAndPassword(ref UserID, ref PersonID, UserName, HashingPassword, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, HashingPassword, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static string ComputeHash(string inpute)
        {
            using(SHA256 sha256= SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inpute));

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if (_AddNewUser())
                {
                    Mode = enMode.Update;
                    return true;
                }
            }
            else
            {
                return _UpdateUser();
            }

            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUserByUserID(UserID);
        }

        public static bool isUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool isUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }


    }
}
