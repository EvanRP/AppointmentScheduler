using AppointmentScheduler.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace AppointmentScheduler.Pages
{
    /// <summary>
    /// Interaction logic for UserPortal.xaml
    /// </summary>
    public partial class UserPortal : Page
    {
        int currentUser;
        Frame theFrame;
        public UserPortal(int userId, Frame mainFrame, bool fromLogon)
        {
            Sql db = new();
            User user = new();

            // get user
            user = db.GetUser(userId);

            theFrame = mainFrame;
            currentUser = userId;
            InitializeComponent();

            // set username label
            labelUname.Content = user.GetUserName();

            if (fromLogon)
            {
                // if comming from logon page check if there is an appointment in the next 15 mins
                CheckForAppointment();
            }
        }

        
        public void logoutClicked(object sender, RoutedEventArgs e)
        {
            //Go to logon page
            theFrame.Navigate(new Logon(theFrame));
        }

        public void apptViewClicked(object sender, RoutedEventArgs e)
        {
            // Go to appointment view 
            theFrame.Navigate(new Appointments.View(currentUser, theFrame));
        }
        public void apptAddClicked(object sender, RoutedEventArgs e)
        {
            // create new instance of appointment.add 
            Appointments.Add addAppt = new Appointments.Add(currentUser);
            // open appointment.add window
            addAppt.Show();
        }

        public void customerViewClicked(object sender, RoutedEventArgs e)
        {
            // go to customer view page
            theFrame.Navigate(new Customers.View(currentUser, theFrame));
        }

        public void reportViewClicked(object sender, RoutedEventArgs e)
        {
            // go to reports page
            theFrame.Navigate(new Reports.Reports(currentUser, theFrame));
        }
        public void addCustomerClicked(object sender, RoutedEventArgs e)
        {
            // create new instance of customer.add
            Customers.Add addCustomer = new Customers.Add(currentUser);
            // open customer.add window
            addCustomer.Show();
        }

        public void CheckForAppointment()
        {
            Sql db = new Sql();

            // get timezone info
            TimeZoneInfo localZone = TimeZoneInfo.Local;

            // get current time 
            // convert it to UTC
            // and Add 15 mins
            DateTime now = DateTime.Now;
            DateTime utcNow = TimeZoneInfo.ConvertTimeToUtc(now, localZone);
            DateTime Utc15Mins = utcNow.AddMinutes(15);

            // get list of appointments for the day
            List<Appointment> appts = db.GetAppointmentsForMonth(currentUser, utcNow.ToString("yyyy-MM-dd"));

            // if there are no appointments return
            if (!appts.Any())
            {
                return;
            }

            // create list of appointments within the next 15 mins
            var nextAppt = appts
                .Where(a => a.Start >= utcNow && a.Start <= Utc15Mins)
                .ToList();

            // if list is empty return
            if(!nextAppt.Any()) 
            {
                return;
            }
            // alert that there is an appointment in 15 mins
            else
            {
                MessageBox.Show($"You have appointment {nextAppt[0].Title} within the next 15 minutes.", "Appointment", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
