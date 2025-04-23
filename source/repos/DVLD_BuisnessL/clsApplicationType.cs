using DVLD_DataAccessL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BuisnessL
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0,Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public string Title { get; set; }
        public Decimal Fees { get; set; }

        public clsApplicationType() 
        {
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }

        public clsApplicationType(int ApplicationType, string ApplicationTypeTitle, Decimal ApplicationFees)
        {
            this.ID = ApplicationType;
            this.Title = ApplicationTypeTitle;
            this.Fees = ApplicationFees;
            Mode = enMode.Update;
        }

        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string Title = "";
            Decimal Fees = 0;

            if(clsApplicationTypeData.GetApplicationTypeInfoByID(ApplicationTypeID,ref Title,ref Fees))
            {
                return new clsApplicationType(ApplicationTypeID, Title, Fees);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewApplicationType()
        {
            this.ID = clsApplicationTypeData.AddNewApplicationType(this.Title, this.Fees);

            return this.ID != -1;
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateApplicationType();
            }
            return false;
        }
    }
}
