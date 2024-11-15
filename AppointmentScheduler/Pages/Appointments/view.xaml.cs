using AppointmentScheduler.Classes;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace AppointmentScheduler.Pages.Appointments
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class View : Page
    {
        int currentUser;
        Frame theFrame;
        public View(int userId, Frame mainFrame)
        {
            theFrame = mainFrame;
            InitializeComponent();
            currentUser = userId;
            Sql db = new();
            User u = db.GetUser(currentUser);

            // Set userName on Page
            string uName = u.GetUserName();
            userNameLabel.Content = uName;

            // disable to and from date boxes
            fromDatePicker.IsEnabled = false;
            toDatePicker.IsEnabled = false;

            // create list of ranges for ComboBox
            List<string> rangePicker = new List<string>();
            string v1 = "Next 30 Days";
            string v2 = "Next 7 Days";
            string v3 = "Custom";
            string v4 = "All";
             
            rangePicker.Add(v4);
            rangePicker.Add(v1);
            rangePicker.Add(v2);
            rangePicker.Add(v3);

            // give list to ComboBox and set All to Default
            viewsBox.ItemsSource = rangePicker;
            viewsBox.SelectedIndex = rangePicker.IndexOf("All");
            GetPageContent();

        }

        public void homeClicked(object sender, RoutedEventArgs e)
        {
            // return to userPortal
            theFrame.Navigate(new UserPortal(currentUser, theFrame, false));
        }

        public void logoutClicked(object sender, RoutedEventArgs e)
        {
            //MainWindow.UserId = 0;

            // Go to logon page
            theFrame.Navigate(new Pages.Logon(theFrame));
        }

        public void apptAddClicked(object sender, RoutedEventArgs e)
        {
            // create new instance of appointment.add
            Appointments.Add addAppt = new Appointments.Add(currentUser);

            // open window
            addAppt.ShowDialog();

            // update Table
            datePicked();

        }

        public void editClicked(object sender, RoutedEventArgs e)
        {
            // get selected appointment
            var selected = dataGrid.SelectedItem;
            if(selected != null)
            {
                // cast selected item to ACjoin
                ACjoin select = selected as ACjoin;

                // Get appointment id
                int aId = select.apptId;

                // create new instance of appoints.edit
                Edit editWindow = new Edit(aId, currentUser);

                // Open window
                editWindow.ShowDialog();

                // update Table
                datePicked();
            }
            else
            {
                // error if nothing selected
                MessageBox.Show("Please select an appointment to edit first.","Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
        public void deleteClicked(object sender, RoutedEventArgs e)
        {
            Sql db = new Sql();

            // get selected appointment
            var selected = dataGrid.SelectedItem;
            if (selected != null)
            {
                // cast selected item to ACjoin
                ACjoin select = selected as ACjoin;

                // confirm delete
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {select.Title}?", "Conformation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }

                // get appointment id
                int aId = select.apptId;
                // delete appontment from db
                db.DeleteRecord("appointment", aId);

                // update table
                datePicked();
            }
            else
            {
                // Error is nothing is selected
                MessageBox.Show("There is no appointment selected to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private void GetPageContent()
        {
            Sql db = new();
            ACjoin joined = new();

            // get appointments for the user
            List<Appointment> appts = db.GetAppointments(currentUser);

            // get list of customers
            List<Customer> customers = db.GetCustomers();

            if (appts != null)
            {
                // create and fill list of combined appointments and customers
                BindingList<ACjoin> allJoined = joined.Join(appts, customers);
                // set list to dataGrid source
                dataGrid.ItemsSource = allJoined;
            }
            
        }

        private void GetPageContent(DateTime start, DateTime end)
        {
            Sql db = new();
            ACjoin joined = new();

            // get list of appointments for user
            List<Appointment> appts = db.GetAppointments(currentUser);

            // get list of customers
            List<Customer> customers = db.GetCustomers();

            if (appts != null)
            {
                // combine appointments and customer lists with ACjoin
                BindingList<ACjoin> allJoined = joined.Join(appts, customers);
                BindingList<ACjoin> inRange = new BindingList<ACjoin>();

                // selected range
                string startString = start.ToString("yyyy-MM-dd") + " 00:00:01";
                string endString = end.ToString("yyyy-MM-dd") + " 23:59:59";
                start = DateTime.Parse(startString);
                end = DateTime.Parse(endString);

                foreach (ACjoin ap in allJoined)
                {
                    if (ap.Start >= start && ap.Start <= end)
                    {
                        // add appointments that are with range
                        inRange.Add(ap);
                    }
                }
                // set dategrid source to list inRange 
                dataGrid.ItemsSource = inRange;
            }
            
        }

        private void GetPageContent(DateTime dt, char c)
        {

            Sql db = new();
            ACjoin joined = new();

            // get list of appointments for user
            List<Appointment> appts = db.GetAppointments(currentUser);

            // get list of customers
            List<Customer> customers = db.GetCustomers();

            if (appts != null)
            {
                // combine appointments and customer lists with ACjoin
                BindingList<ACjoin> allJoined = joined.Join(appts, customers);
                BindingList<ACjoin> inRange = new();

                foreach (ACjoin ap in allJoined)
                {
                    // if from date is filled and to date empty
                    if (c == 's')
                    {
                        // date range
                        string startString = dt.ToString("yyyy-MM-dd") + " 00:00:01";
                        dt = DateTime.Parse(startString);

                        if (ap.Start >= dt)
                        {
                            // add to list if in range
                            inRange.Add(ap);
                        }
                    }
                    // if to date is filled and from date empty
                    else if (c == 'e')
                    {
                        // date range
                        string endString = dt.ToString("yyyy-MM-dd") + " 23:59:59";
                        dt = DateTime.Parse(endString);

                        if (ap.Start <= dt)
                        {
                            // add to list if in range
                            inRange.Add(ap);
                        }
                    }
                }
                // set dategrid source to list inRange
                dataGrid.ItemsSource = inRange;
            }
            
        }

        public void viewPicked(object sender, SelectionChangedEventArgs e)
        {
            string s = viewsBox.SelectedItem as string;
            DateTime now = DateTime.Now.Date;

            switch (s)
            {
                case "Custom":
                    // enable datePickers
                    fromDatePicker.IsEnabled = true;
                    toDatePicker.IsEnabled = true;
                    break;

                case "Next 30 Days":
                    // disable datePickers
                    fromDatePicker.IsEnabled = false;
                    toDatePicker.IsEnabled = false;
                    // set to date range
                    toDatePicker.SelectedDate = now.AddDays(30);
                    // set from date range
                    fromDatePicker.SelectedDate = now;
                    break;

                case "Next 7 Days":
                    // disable datePicker
                    fromDatePicker.IsEnabled = false;
                    toDatePicker.IsEnabled = false;
                    // set from datePicker
                    fromDatePicker.SelectedDate = now;
                    // set to datePicker
                    toDatePicker.SelectedDate = now.AddDays(7);
                    break;

                case "All":
                    // disable DatePickers
                    fromDatePicker.IsEnabled = false;
                    toDatePicker.IsEnabled = false;
                    // set DatePickers to null 
                    fromDatePicker.SelectedDate = null;
                    toDatePicker.SelectedDate = null;
                    break;
            }
        }

        public void datePicked()
        {
            // get dates from DatePickers
            DateTime? dStart = fromDatePicker.SelectedDate;
            DateTime? dEnd = toDatePicker.SelectedDate;

            // check if datePickers have value and call the necessary GetPageContent
            if (!dStart.HasValue  && dEnd.HasValue)
            {
                DateTime dt = toDatePicker.SelectedDate.Value;
                GetPageContent(dt, 'e');
            }
            else if (dStart.HasValue && !dEnd.HasValue)
            {
                DateTime dt = fromDatePicker.SelectedDate.Value;
                GetPageContent( dt, 's');
            }
            else if(dStart.HasValue && dEnd.HasValue)
            {
                DateTime start = fromDatePicker.SelectedDate.Value;
                DateTime end = toDatePicker.SelectedDate.Value;
                GetPageContent(start, end);
            }
            else
            {
                GetPageContent();
            }
        }

        public void DateChanged(object sender, EventArgs e)
        {
            // event that updates dateGrid when dates change
            datePicked();
        }

    }
}
