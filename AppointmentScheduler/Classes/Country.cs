using System;

namespace AppointmentScheduler.Classes
{
    internal class Country
    {
        // properties
        private int CountryId;
        private string CountryName;
        private DateTime CreateDate;
        private string CreateBy;
        private DateTime LastUpdate;
        private string LastUpdatedBy;

        // getters
        public int GetId()
        {
            // get country id
            return this.CountryId;
        }
        public string GetCountryName()
        {
            // get country name
            return this.CountryName;
        }
        public DateTime GetCreateDate()
        {
            // get create date
            return this.CreateDate;
        }
        public string GetCreateBy()
        {
            // get create by
            return this.CreateBy;
        }
        public DateTime GetLastUpdate()
        {
            // get last update
            return this.LastUpdate;
        }
        public string GetLastUpdatedBy()
        {
            // get last update by
            return this.LastUpdatedBy;
        }

        // setters
        public void SetCountryId(int CountryId)
        {
            // set country id
            this.CountryId = CountryId;
        }
        public void SetCountryName(string CountryName)
        {
            // set country name
            this.CountryName = CountryName;
        }
        public void SetCreateDate(DateTime dt)
        {
            // set create date
            this.CreateDate = dt;
        }
        public void SetCreateBy(string s)
        {
            // set create by
            this.CreateBy = s;
        }
        public void SetLastUpdate(DateTime dt)
        {
            // set last update date
            this.LastUpdate = dt;
        }
        public void SetLastUpdateBy(string s)
        {
            // set last update by
            this.LastUpdatedBy = s;
        }
        
        // constructor
        public Country()
        {
            this.CountryId = 0;
            this.CountryName = "";
            this.CreateBy = "";
            this.LastUpdatedBy = "";
        }
    }
}
