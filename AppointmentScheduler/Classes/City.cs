using System;

namespace AppointmentScheduler.Classes
{
    internal class City
    {
        // properties
        private int CityId;
        private string CityName;
        private int CountryId;
        private DateTime CreateDate;
        private string CreateBy;
        private DateTime LastUpdate;
        private string LastUpdatedBy;

        // getters
        public int GetCityId()
        {
            // get city id
            return this.CityId;
        }
        public string GetCityName()
        {
            // get city name
            return this.CityName;
        }
        public int GetCountryId()
        {
            // get country id
            return this.CountryId;
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
            // get update date
            return this.LastUpdate;
        }
        public string GetLastUpdatedBy()
        {
            // get update by
            return this.LastUpdatedBy;
        }

        // setters
        public void SetCityId(int i)
        {
            // set city id
            this.CityId = i;
        }
        public void SetCityName(string s)
        {
            // set city name
            this.CityName = s;
        }
        public void SetCountryId(int i)
        {
            // set country id
            this.CountryId = i;
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
        public City()
        {
            this.CityId = 0;
            this.CityName = "";
            this.CountryId = 0;
            this.CreateBy = "";
            this.LastUpdatedBy = "";
        }
    }
}
