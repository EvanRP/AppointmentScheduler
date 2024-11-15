using System;

namespace AppointmentScheduler.Classes
{
    internal class Appointment
    {
        public int ApptId { get; private set; }
        public int CustomerId { get; private set; }
        public int UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public string Contact { get; private set; }
        public string ApptType { get; private set; }
        public string Url { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public string LastUpdateBy { get; private set; }

        // setters
        public void SetApptId(int i)
        {
            // set appt id
            this.ApptId = i;
        }
        public void SetCustomerId(int i)
        {
            // set customer id
            this.CustomerId = i;
        }
        public void SetUserId(int i)
        {
            // set user id
            this.UserId = i;
        }
        public void SetTitle(string s)
        {
            // set title
            this.Title = s;
        }
        public void SetDescription(string s)
        {
            // set description
            this.Description = s;
        }
        public void SetLocation(string s)
        {
            // set location
            this.Location = s;
        }
        public void SetContact(string s)
        {
            // set contact
            this.Contact = s;
        }
        public void SetApptType(string s)
        {
            // set appointment type
            this.ApptType = s;
        }
        public void SetURL(string s)
        {
            // set URL
            this.Url = s;
        }
        public void SetStart(DateTime dt)
        {
            // set start date
            this.Start = dt;
        }
        public void SetEnd(DateTime dt)
        {
            // set end date
            this.End = dt;
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
            // set last update
            this.LastUpdate = dt;
        }
        public void SetLastUpdateBy(string s)
        {
            // set last update by
            this.LastUpdateBy = s;
        }

        // constructors
        public Appointment()
        {
            this.ApptId = 0;
            this.CustomerId = 0;
            this.UserId = 0;
            this.Title = "";
            this.Description = "";
            this.Location = "";
            this.Contact = "";
            this.ApptType = "";
            this.Url = "";
            this.CreatedBy = "";
            this.LastUpdateBy = "";
        }

        public Appointment(int apptId, int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            this.ApptId = apptId;
            this.CustomerId = customerId;
            this.UserId = userId;
            this.Title = title;
            this.Description = description;
            this.Location = location;
            this.Contact = contact;
            this.ApptType = type;
            this.Url = url;
            this.Start = start;
            this.End = end;
            this.CreateDate = createDate;
            this.CreatedBy = createdBy;
            this.LastUpdate = lastUpdate;
            this.LastUpdateBy = lastUpdateBy;
        }
    }
}
