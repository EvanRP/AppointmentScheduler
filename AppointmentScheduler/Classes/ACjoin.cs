using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AppointmentScheduler.Classes
{
    internal class ACjoin
    {
        public int apptId {  get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }  
        public string Location { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }


        public ACjoin()
        {

        }

        public BindingList<ACjoin> Join(List<Appointment> appointments, List<Customer> customer)
        {
            // combines appointment list and customer list
            // also converts times from UTC to local time zone
            BindingList<ACjoin> joined = new BindingList<ACjoin>();

            foreach(Appointment appt in appointments)
            {
                // loop through appointment list and
                // finds customer with the same customerId
                int cid = appt.CustomerId;
                Customer c = customer.FirstOrDefault(c=>c.GetCustomerId() == cid, null);

                if(c != null)
                {
                    // if customer is found convert time from UTC to local time zone
                    // create new acJion add add to list to return
                    TimeZoneInfo localZone = TimeZoneInfo.Local;

                    ACjoin whatever = new()
                    {
                        apptId = appt.ApptId,
                        UserId = appt.UserId,
                        Title = appt.Title,
                        Description = appt.Description,
                        Customer = c.GetCustomerName(),
                        CustomerId = cid,
                        Start = TimeZoneInfo.ConvertTimeFromUtc(appt.Start, localZone),
                        End = TimeZoneInfo.ConvertTimeFromUtc(appt.End, localZone),
                        Location = appt.Location,
                        Url = appt.Url,
                        Type = appt.ApptType
                    };

                    joined.Add(whatever);

                }
                else
                {
                    TimeZoneInfo localZone = TimeZoneInfo.Local;
                    ACjoin noCustomer = new()
                    {
                        apptId = appt.ApptId,
                        UserId = appt.UserId,
                        Title = appt.Title,
                        Description = appt.Description,
                        Customer = "No customer",
                        CustomerId = 0,
                        Start = TimeZoneInfo.ConvertTimeFromUtc(appt.Start, localZone),
                        End = TimeZoneInfo.ConvertTimeFromUtc(appt.End, localZone),
                        Location = appt.Location,
                        Url = appt.Url,
                        Type = appt.ApptType
                    };

                    joined.Add(noCustomer);

                }
            }
            return joined;
        }
    }
}
