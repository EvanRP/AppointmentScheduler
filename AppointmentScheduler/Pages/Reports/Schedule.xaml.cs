using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AppointmentScheduler.Classes;

namespace AppointmentScheduler.Pages.Reports
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Schedule : Window
    {
        public Schedule(string uName)
        {
            InitializeComponent();

            Sql db = new();

            // get user Id 
            int uId = db.GetUID(uName);

            DateTime dt = DateTime.Now;
            string monthString = dt.ToString("yyyy-MM");

            // get user appointments for the month
            List<Appointment> appointments = db.GetAppointmentsForMonth(uId, monthString);

            // create main stack
            StackPanel mainStack = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

             // create list of customers
            List<Customer> customers = db.GetCustomers();

            ACjoin join = new();

            // join appointment and customer lists
            BindingList<ACjoin> joined = join.Join(appointments, customers);

            // orders list by start date 
            List<ACjoin> sorted = joined.OrderBy(a => a.Start).ToList();

            if(joined.Count > 0)
                // check if user has appointments
            {
                string dates = "";

                foreach (ACjoin ac in sorted)
                    // loop through appointments
                {
                    // get start date and convert to string
                    DateTime startDate = ac.Start;
                    string date = startDate.ToString("yyyy-MM-dd");

                    // create label for day
                    TextBlock dateLabel = new TextBlock
                    {
                        Text = date + " " + startDate.DayOfWeek.ToString(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        TextDecorations = TextDecorations.Underline
                    };

                    if (dates == "" || dates != date)
                        // if there is no date label on stack or day label is differnet than appointment
                        // add new label to stack
                    {
                        mainStack.Children.Add(dateLabel);
                    }
                    // set date of appointment to dates so
                    // next appointment date can be check agianst
                    // it to see if they are on the same date
                    dates = date;

                    // create strings for appointment start and end times
                    string startTime = ac.Start.ToString("HH:mm");
                    string endTime = ac.End.ToString("HH:mm");

                    // create label for schedule item
                    Label scheduleItem = new Label
                    {
                        Content = $"{ac.Customer}       {startTime}-{endTime}"
                    };
                    // add to main stack
                    mainStack.Children.Add(scheduleItem);
                }
            }
            else
            // user has no appointments
            {
                // create label to inform that the user has no appointments for the month
                Label blank = new Label
                {
                    Content = "This user has no appointments this month"
                };

                // add label to stack
                mainStack.Children.Add(blank);
            }

            // put main stack onto the page
            scrollView.Content = mainStack;
        }
    }
}
