using AppointmentScheduler.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AppointmentScheduler.Pages.Appointments
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        int aId;
        int uId;
        string uName;
        internal Edit(int apptId, int userId)
        {
            uId = userId;
            aId = apptId;
            InitializeComponent();

            Sql db = new();

            // get user
            User u = db.GetUser(uId);
            uName = u.GetUserName();

            // get appointment
            Appointment appt = db.GetAppointments(userId, apptId);

            // get customer list and fill customer selector
            List<Customer> cList = db.GetCustomers();
            List<string> customerDS = new List<string>();
            string index = "";
            foreach(Customer customer in cList)
            {
                string s = customer.GetCustomerId().ToString() + " " + customer.GetCustomerName();
                customerDS.Add(s);

                if(customer.GetCustomerId() == appt.CustomerId)
                {
                    index = s;
                }
            }

            // Set Customer selector source
            comboBox.ItemsSource = customerDS;

            // convert time from UTC to Local
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            appt.SetStart(TimeZoneInfo.ConvertTimeFromUtc(appt.Start, localZone));
            appt.SetEnd(TimeZoneInfo.ConvertTimeFromUtc(appt.End, localZone));  

            // set page's textboxes
            comboBox.SelectedIndex = customerDS.IndexOf(index);
            TitleBox.Text = appt.Title;
            DescriptionBox.Text = appt.Description;
            LocationBox.Text = appt.Location;
            ContactBox.Text = appt.Contact;
            TypeBox.Text = appt.ApptType;
            URLBox.Text = appt.Url;
            startDate.SelectedDate = appt.Start;
            EndDate.SelectedDate = appt.End;
            string startTime = appt.Start.ToString("h:mm");
            string endTime = appt.End.ToString("h:mm");
            FromTime.Text = startTime;
            ToTime.Text = endTime;
            string sMeridiem = appt.Start.ToString("tt");
            string eMeridiem = appt.End.ToString("tt");

            foreach(ComboBoxItem i in FromMeridiem.Items)
            {
                if (i.Content.ToString() == sMeridiem)
                {
                    FromMeridiem.SelectedItem = i;
                }
            }
            if(sMeridiem == eMeridiem)
            {
                ToMeridiem.SelectedIndex = FromMeridiem.Items.IndexOf(FromMeridiem.SelectedItem);
            }
            else
            {
                foreach (ComboBoxItem i in ToMeridiem.Items)
                {
                    if (i.Content.ToString() == eMeridiem)
                    {
                        ToMeridiem.SelectedItem = i;
                    }
                }
            }
            
        }

        public void cancelClicked(object sender, RoutedEventArgs e)
        {
            // close window
            this.Close();
        }
        private static bool CheckDateMFEST(DateTime sDate, DateTime eDate)
        {
            // Returns True if dates are normal business hours

            // get est info
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            // convert dates from utc to est
            DateTime sTime = TimeZoneInfo.ConvertTimeFromUtc(sDate, est);
            DateTime eTime = TimeZoneInfo.ConvertTimeFromUtc(eDate, est);

            // get days of the week start and end dates are
            DayOfWeek sDay = sTime.DayOfWeek;
            DayOfWeek eDay = eTime.DayOfWeek;

            // set DateTime for Business Hours
            string estOpen = sTime.ToString("yyyy-MM-dd") + " 09:00";
            string estClose = eTime.ToString("yyyy-MM-dd") + " 17:00";
            DateTime open = DateTime.Parse(estOpen);
            DateTime close = DateTime.Parse(estClose);

            // bool to check if times are within business hours
            bool checkDay = sDay >= DayOfWeek.Monday &&
                sDay <= DayOfWeek.Friday &&
                eDay >= DayOfWeek.Monday &&
                eDay <= DayOfWeek.Friday;

            //bool to check if days are within Monday - friday
            bool checkTime = sTime >= open &&
                sTime <= close &&
                eTime >= open &&
                eTime <= close;
            
            return checkDay && checkTime;
        }
        public void saveClicked(object sender, RoutedEventArgs e)
        {
            // validate if time time is entered in properly
            string fromDate = FromTime.Text;
            string toDate = ToTime.Text;
            int fromColon = fromDate.IndexOf(":");
            int toColon = toDate.IndexOf(":");
            string[] from = fromDate.Split(":");
            string[] to = toDate.Split(":");
            bool checkNumOfColons = from.Count() < 3 && to.Count() < 3;

            if (checkNumOfColons)
                // correct number of colons
            {
                // Alert if there is an issue with time entered 
                if (fromColon <= 0 || int.Parse(from[0]) > 12 || int.Parse(from[1]) > 59)
                {
                    MessageBox.Show("Incorrect format for From Time", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (toColon <= 0 || int.Parse(to[0]) > 12 || int.Parse(to[1]) > 59)
                {
                    MessageBox.Show("Incorrect format for To Time", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            // too many colons
            {
                MessageBox.Show("Too many colons check time input");
                return;
            }
            

            Sql db = new();

            Appointment appt = new();

            // set id
            appt.SetApptId(aId);

            // Get start and end dates
            DateTime? sDate = startDate.SelectedDate;
            DateTime? eDate = EndDate.SelectedDate;

            //Get Meridiem
            string m1 = (FromMeridiem.SelectedItem as ComboBoxItem)?.Content as string;
            string m2 = (ToMeridiem.SelectedItem as ComboBoxItem)?.Content as string;

            //Put Dates together
            string sStart = $"{sDate:yyyy-MM-dd} {FromTime.Text} {m1}";
            string sEnd = $"{eDate:yyyy-MM-dd} {ToTime.Text} {m2}";

            // convert to DateTime
            DateTime start = DateTime.ParseExact(sStart, "yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(sEnd, "yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture);

            // Convert Time to UTC
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            start = TimeZoneInfo.ConvertTimeToUtc(start, localZone);
            end = TimeZoneInfo.ConvertTimeToUtc(end, localZone);
            DateTime now = DateTime.Now;
            now = TimeZoneInfo.ConvertTimeToUtc(now, localZone);

            // check if start is after end date
            if (start > end)
            {
                MessageBox.Show("Appointment end date cannot be before the start date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check to make sure there are no conflicting appointments

            // get appointments for the day
            List<Appointment> appts = db.GetAppointmentsForMonth(uId, start.ToString("yyyy-MM-dd"));
            if (appts.Any())
            {
                Appointment check1 = appts.FirstOrDefault(a => a.Start >= start && a.Start <= end || a.End >= start && a.End <= end, null);
                

                bool conflict = check1 != null;
                if (conflict)
                {
                    if (aId == check1.ApptId)
                    {
                        conflict = false;
                    }
                    if (conflict)
                    {
                        MessageBox.Show("There is a conflicting appointment", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                
            }

            // check if appointments are within normal business hours
            if (!CheckDateMFEST(start, end))
            {
                MessageBox.Show("Appointment is outside of normal business hours\n\nNormal Business hours Monday-Friday 9:00AM-5:00PM Eastern Standard Time", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            

            // Set Dates Start End Update
            appt.SetStart(start);
            appt.SetEnd(end);
            appt.SetLastUpdate(now);
            appt.SetLastUpdateBy(uName);

            // set the other fields 
            appt.SetTitle(TitleBox.Text);
            appt.SetDescription(DescriptionBox.Text);
            appt.SetLocation(LocationBox.Text);
            appt.SetContact(ContactBox.Text);
            appt.SetApptType(TypeBox.Text);
            appt.SetURL(URLBox.Text);

            // get selected appointment ids
            string[] s = comboBox.SelectedItem.ToString().Split(" ");
            appt.SetCustomerId(int.Parse(s[0]));

            // update appointment
            db.UpdateAppointment(appt);
            this.Close();
        }

        public void CheckNull()
        {
            // get start and end dates
            DateTime? dStart = startDate.SelectedDate;
            DateTime? dEnd = EndDate.SelectedDate;

            // check if fields are empty
            bool allBoxes = comboBox.SelectedItem != null &&
                !string.IsNullOrEmpty(TitleBox.Text) &&
                !string.IsNullOrEmpty(DescriptionBox.Text) &&
                !string.IsNullOrEmpty(LocationBox.Text) &&
                !string.IsNullOrEmpty(ContactBox.Text) &&
                !string.IsNullOrEmpty(TypeBox.Text) &&
                !string.IsNullOrEmpty(URLBox.Text) &&
                FromMeridiem.SelectedItem != null &&
                ToMeridiem.SelectedItem != null &&
                !string.IsNullOrEmpty(FromTime.Text) &&
                !string.IsNullOrEmpty(ToTime.Text) &&
                dStart.HasValue &&
                dEnd.HasValue;

            // enable save button if fields are not null
            if (allBoxes)
            {
                SaveButton.IsEnabled = true;
            }
            // disable save button if field is null
            else
            {
                SaveButton.IsEnabled = false;
            }
        }
        public void IsBoxNull(object sender, TextChangedEventArgs e)
        {
            // Check for null fields
            CheckNull();
        }
        public void NumOrColonCheck(object sender, TextCompositionEventArgs e)
        {
            // check if inputis numeric or a colon
            if (int.TryParse(e.Text, out _) || e.Text == ":")
            {
                // allow
                e.Handled = false;
            }
            else
            {
                // disallow
                e.Handled = true;
            }
        }

        public void ComboBoxChange(object sender, SelectionChangedEventArgs e)
        {
            // Check for null fields
            CheckNull();
        }
    }
}
