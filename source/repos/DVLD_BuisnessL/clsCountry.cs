using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DataAccessL;


namespace DVLD_BuisnessL
{
   public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
        }

        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        static public clsCountry Find(int CountryID)
        {
            string CountryName = "";

            if (clsCountryData.GetCountryInfoByID(CountryID, ref CountryName))
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        static public clsCountry Find(string CountryName)
        {
            int CountryID = -1;

            if (clsCountryData.GetCountryInfoByName(ref CountryID, CountryName))
                return new clsCountry(CountryID, CountryName);
            else
                return null;

        }

        static public DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
    }



}
