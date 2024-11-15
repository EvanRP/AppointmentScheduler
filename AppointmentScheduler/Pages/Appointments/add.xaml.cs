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
    /// Interaction logic for add.xaml
    /// </summary>
    public partial class Add : Window
    {
        int uId;
        string uName;
        public Add(int userId)
        {
            InitializeComponent();
            // get Customers to fill customer picker
            Sql db = new();

            //get list of customers
            List<Customer> customers = db.GetCustomers();
            List<string> customerDS = new();
            foreach(Customer customer in customers)
            {
                // create list of customer ids and customer names for customer selector
                string s = customer.GetCustomerId().ToString() + " " + customer.GetCustomerName();

                customerDS.Add(s);
            }
            // set customer selector comboBox source
            comboBox.ItemsSource = customerDS;

            // get User
            User u = db.GetUser(userId);
            uId = userId;
            uName = u.GetUserName();

            // disable save button
            SaveButton.IsEnabled = false;
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
            // validate if time is entered in properly 
            string fromDate = FromTime.Text;
            string toDate = ToTime.Text;
            int fromColon = fromDate.IndexOf(":");
            int toColon = toDate.IndexOf(":");
            string[] from = fromDate.Split(":");
            string[] to = toDate.Split(":");
            bool noAlpha = int.TryParse(from[0], out _) &&
                int.TryParse(from[1], out _) &&
                int.TryParse(to[0], out _) &&
                int.TryParse(from[1], out _) &&
                from.Count() < 3 &&
                to.Count() < 3;

            if (noAlpha)
                //input is numeric
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
            // input is not numeric
            {
                MessageBox.Show("Time boxes can only contain nuberic values and a colon", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Get Next Id
            Sql db = new();
            int nextId = db.GetLastIndex("appointment") + 1;

            // Convert Time

            //Get local time zone
            TimeZoneInfo localZone = TimeZoneInfo.Local;

            // Get entered dates
            DateTime? sDate = startDate.SelectedDate;
            DateTime? eDate = EndDate.SelectedDate;

            // Get Meridiem
            string m1 = (FromMeridiem.SelectedItem as ComboBoxItem)?.Content as string;
            string m2 = (ToMeridiem.SelectedItem as ComboBoxItem)?.Content as string;

            // Put it together
            string sStart = $"{sDate:yyyy-MM-dd} {FromTime.Text} {m1}";
            string sEnd = $"{eDate:yyyy-MM-dd} {ToTime.Text} {m2}";

            // Convert to DateTime
            DateTime start = DateTime.ParseExact(sStart, "yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(sEnd, "yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture);
            
            // Convert to UTC
            start = TimeZoneInfo.ConvertTimeToUtc(start, localZone);
            end = TimeZoneInfo.ConvertTimeToUtc(end, localZone);

            // get current time for DateCreated and LastUpdated fields
            DateTime now = DateTime.Now;
            now = TimeZoneInfo.ConvertTimeToUtc(now, localZone);

            // check if start is after end date
            if(start > end)
            {
                MessageBox.Show("Appointment end date cannot be before the start date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check to make sure there are no conflicting appointments

            // get appointments for the day
            List<Appointment> appts = db.GetAppointmentsForMonth(uId, start.ToString("yyyy-MM-dd"));
            if (appts.Any())
            {
                // check if there are any appointments in time range
                Appointment check1 = appts.FirstOrDefault(a=>a.Start >= start && a.Start <= end, null);
                Appointment check2 = appts.FirstOrDefault(a => a.End >= start && a.End <= end, null);

                bool conflict = check1 != null || check2 != null;

                // if there are conflicts error and return
                if (conflict)
                {
                    MessageBox.Show("There is a conflicting appointment", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // check if appointments are within normal business hours
            if (!CheckDateMFEST(start, end))
            {
                MessageBox.Show("Appointment is outside of normal business hours\n\nNormal Business hours Monday-Friday 9:00AM-5:00PM Eastern Standard Time", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // get customerId
            string[] s = comboBox.SelectedItem.ToString().Split(" ");
            int customerId = int.Parse(s[0]);

            // create new Appointment
            Appointment appt = new Appointment(nextId, customerId, uId, TitleBox.Text, DescriptionBox.Text, LocationBox.Text, ContactBox.Text, TypeBox.Text, URLBox.Text, start, end, now, uName, now, uName);
            db.AddAppointment(appt);
            this.Close();
        }
        public void cancelClicked(object sender, RoutedEventArgs e)
        {
            // close window
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
            // check if input is numeric or a colon
            if(int.TryParse(e.Text, out _) || e.Text == ":")
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
            // check for null fields
            CheckNull();
        }
    }
}
