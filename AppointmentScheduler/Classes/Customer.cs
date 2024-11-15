using System;

namespace AppointmentScheduler.Classes
{
    internal class Customer
    {
        // properties
        private int CustomerId;
        private string CustomerName;
        private int AddressId;
        private bool Active;
        private DateTime CreateDate;
        private string CreatedBy;
        private DateTime LastUpdate;
        private string LastUpdatedBy;

        // getters
        public int GetCustomerId()
        {
            // get customer id
            return this.CustomerId;
        }
        public string GetCustomerName()
        {
            // get customer name
            return this.CustomerName;
        }
        public int GetAddressId()
        {
            // get address id
            return this.AddressId;
        }
        public bool GetActive()
        {
            // get active status
            return this.Active;
        }
        public DateTime GetCreateDate()
        {
            // get create date
            return this.CreateDate;
        }
        public string GetCreatedBy()
        {
            // get create by
            return this.CreatedBy;
        }
        public DateTime GetLastUpdate()
        {
            // get last update date
            return this.LastUpdate;
        }
        public string GetLastUpdateBy()
        {
            // get last update by
            return this.LastUpdatedBy;
        }

        // setters
        public void SetCustomerId(int i)
        {
            // set customer id
            this.CustomerId = i;
        }
        public void SetCustomerName(string s)
        {
            // set customer name
            this.CustomerName = s;
        }
        public void SetAddressId(int i)
        {
            // set address id
            this.AddressId = i;
        }
        public void SetActive(bool i)
        {
            // set active status
            this.Active = i;
        }
        public void SetCreateDate(DateTime dt)
        {
            // set create date
            this.CreateDate = dt;
        }
        public void SetCreateBy(string s)
        {
            // set create by
            this.CreatedBy = s;
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

        // constructors
        public Customer()
        {
            this.CustomerId = 0;
            this.CustomerName = "";
            this.AddressId = 0;
            this.Active = false;
            this.CreatedBy = "";
            this.LastUpdatedBy = "";

        }
        public Customer(int customerId, string customerName, int addressId, bool active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdatedBy)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            AddressId = addressId;
            Active = active;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdatedBy = lastUpdatedBy;
        }
    }
}
