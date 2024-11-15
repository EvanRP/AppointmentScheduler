using System;

namespace AppointmentScheduler.Classes
{
    internal class Address
    {
        private int AddressId;
        private string Address1;
        private string Address2;
        private int CityId;
        private string PostalCode;
        private string Phone;
        private DateTime CreateDate;
        private string CreatedBy;
        private DateTime LastUpdate;
        private string LastUpdatedBy;

        // getters
        public int GetAddressId()
        {
            // get address Id
            return this.AddressId;
        }
        public string GetAddress1()
        {
            // get address1
            return this.Address1;
        }
        public string GetAddress2()
        {
            // get address2
            return this.Address2;
        }
        public int GetCityId()
        {
            // get city Id
            return this.CityId;
        }
        public string GetPostalCode()
        {
            // get postal code
            return this.PostalCode;
        }
        public string GetPhone()
        {
            // get phone
            return this.Phone;
        }
        public DateTime GetCreateDate()
        {
            // get create date
            return this.CreateDate;
        }
        public string GetCreateBy()
        {
            // get create by
            return this.CreatedBy;
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
        public void SetAddressId(int i)
        {
            // set address Id
            this.AddressId = i;
        }
        public void SetAddress1(string s)
        {
            // set address1
            this.Address1 = s;
        }
        public void SetAddress2(string s)
        {
            // set address2
            this.Address2 = s;
        }
        public void SetCityId(int i)
        {
            // set city Id
            this.CityId = i;
        }
        public void SetPostalCode(string s)
        {
            // set postal code
            this.PostalCode = s;
        }
        public void SetPhone(string s)
        {
            // set phone
            this.Phone = s;
        }
        public void SetCreateDate(DateTime dt)
        {
            // set create date
            this.CreateDate = dt;
        }
        public void SetCreatedBy(string s)
        {
            // set create by
            this.CreatedBy = s;
        }
        public void SetLastUpdate(DateTime dt)
        {
            // set last update date
            this.LastUpdate = dt;
        }
        public void SetLastUpdatdeBy(string s)
        {
            // set last update by
            this.LastUpdatedBy = s;
        }

        //constructor
        public Address()
        {
            this.AddressId = 0;
            this.Address1 = "";
            this.Address2 = "";
            this.CityId = 0;
            this.PostalCode = "";
            this.Phone = "";
            this.CreatedBy = "";
            this.LastUpdatedBy = "";
        }
    }
}
