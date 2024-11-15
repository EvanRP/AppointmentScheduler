using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AppointmentScheduler.Classes;

namespace AppointmentScheduler.Pages.Reports
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Reports : Page
    {
        int currentUser;
        Frame theFrame;
        public Reports(int userId, Frame mainFrame)
        {
            theFrame = mainFrame;
            InitializeComponent();
            currentUser = userId;

            Sql db = new();

            // get user
            User usr = db.GetUser(userId);

            // get list of user names
            List<string> uNames = db.GetUserNames();

            // set username label
            labelUname.Content = usr.GetUserName();

            // set source for username comboBox and
            // set default value to current username
            userNameBox.ItemsSource = uNames;
            userNameBox.SelectedIndex = uNames.IndexOf(usr.GetUserName());
        }

        public void homeClicked(object sender, RoutedEventArgs e)
        {
            // navigate to user portal page
            theFrame.Navigate(new UserPortal(currentUser, theFrame, false));
        }

        public void logoutClicked(object sender, RoutedEventArgs e)
        {
            // navigate to logon page
            theFrame.Navigate(new Pages.Logon(theFrame));
        }

        public void MonthReportClicked(object sender, RoutedEventArgs e)
        {
            // create new instance of ByMonth and open window
            ByMonth monthReport = new ByMonth(currentUser);
            monthReport.Show();
        }

        public void UserScheduleClicked(object sender, RoutedEventArgs e)
        {
            // get selected username
            string uName = userNameBox.SelectedItem.ToString();

            // create new instance of schedule and open window
            Schedule uSchedule = new Schedule(uName);
            uSchedule.Show();
        }

        public void CustomerReportClicked(object sender, RoutedEventArgs e)
        {
            // create new instance of customer report and open window
            CustomersReport cr = new CustomersReport();
            cr.Show();
        }
    }
}
