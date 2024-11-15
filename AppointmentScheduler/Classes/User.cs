using System;

namespace AppointmentScheduler.Classes
{
    internal class User
    {
        // properties
        private int UserId;
        private string UserName;
        private string Password;
        private int Active;
        private DateTime CreateDate;
        private string CreatedBy;
        private DateTime LastUpdate;
        private string LastUpdatedBy;

        // getters
        public int GetUserId()
        {
            // get user id
            return this.UserId;
        }
        public string GetUserName()
        {
            // get user name
            return this.UserName;
        }
        public string GetPassword()
        {
            // get user password
            return this.Password;
        }
        public int GetActive()
        {
            // get active status
            return this.Active;
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
            // get last update date
            return this.LastUpdate;
        }
        public string GetLastUpdatedBy()
        {
            // get last update by
            return this.LastUpdatedBy;
        }

        // setters
        public void SetUserId(int i)
        {
            // set user ID
            this.UserId = i;
        }
        public void SetUserName(string s)
        {
            // set username
            this.UserName = s;
        }
        public void SetPassword(string s)
        {
            // set password
            this.Password = s;
        }
        public void SetCreateDate(DateTime dt)
        {
            // set create date
            this.CreateDate = dt;
        }
        public void SetCreatedBy(string s)
        {
            // set created by
            this.CreatedBy = s;
        }
        public void SetLastUpdate(DateTime dt)
        {
            // set last update date
            this.LastUpdate = dt;
        }
        public void SetLastUpdatedBy(string s)
        {
            // set last updated by
            this.LastUpdatedBy = s;
        }

        // constructors
        public User()
        {
            this.UserId = 0;
            this.UserName = "";
            this.Password = "";
            this.Active = 0;
            this.CreatedBy = "";
            this.LastUpdatedBy = "";
        }

        public User(int uId, string uName, string pass, int active,  DateTime createDate, string createBy, DateTime updated, string updatedBy)
        {
            this.UserId = uId;
            this.UserName = uName;
            this.Password = pass;
            this.Active = active;
            this.CreateDate = createDate;
            this.CreatedBy = createBy;
            this.LastUpdatedBy= updatedBy;
            this.LastUpdate= updated;
        }
    }
}
