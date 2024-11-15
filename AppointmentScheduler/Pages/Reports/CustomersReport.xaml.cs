using AppointmentScheduler.Classes;
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

namespace AppointmentScheduler.Pages.Reports
{
    /// <summary>
    /// Interaction logic for CustomerReport.xaml
    /// </summary>
    public partial class CustomersReport : Window
    {
        public CustomersReport()
        {
            InitializeComponent();


            Sql db = new Sql();

            StackPanel mainView = new StackPanel();
            string year = DateTime.Now.Year.ToString();
            string yearMonth = "";
            

            List<Customer> customers = db.GetCustomers();

            for(int i = 1; i<13; i++)
                // loop through months
            {
                Label month = new Label();

                // set variables based on month
                switch (i)
                {
                    case 1:
                        yearMonth = year + "-01";
                        month.Content = "January";
                        break;
                    case 2:
                        yearMonth = year + "-02";
                        month.Content = "Febuary";
                        break;
                    case 3:
                        yearMonth = year + "-03";
                        month.Content = "March";
                        break;
                    case 4:
                        yearMonth = year + "-04";
                        month.Content = "April";
                        break;
                    case 5:
                        yearMonth = year + "-05";
                        month.Content = "May";
                        break;
                    case 6:
                        yearMonth = year + "-06";
                        month.Content = "June";
                        break;
                    case 7:
                        yearMonth = year + "-07";
                        month.Content = "July";
                        break;
                    case 8:
                        yearMonth = year + "-08";
                        month.Content = "August";
                        break;
                    case 9:
                        yearMonth = year + "-09";
                        month.Content = "September";
                        break;
                    case 10:
                        yearMonth = year + "-10";
                        month.Content = "October";
                        break;
                    case 11:
                        yearMonth = year + "-11";
                        month.Content = "November";
                        break;
                    case 12:
                        yearMonth = year + "-12";
                        month.Content = "December";
                        break;
                }

                // get list of appointments for the month
                List<Appointment> appointments = db.GetAppointmentsForMonth(yearMonth);

                // add month label to stack
                // and make label centered
                month.HorizontalAlignment = HorizontalAlignment.Center;
                mainView.Children.Add(month);

                if (appointments.Count > 0)
                // checks if month has appointments
                {
                    ACjoin join = new();
                    BindingList<ACjoin> joined = join.Join(appointments, customers);

                    // group appointments by customer and count how many appointments are in the group
                    var grouped = joined
                        .GroupBy(a => a.Customer)
                        .Select(group => new { Customer = group.Key, Count = group.Count() });

                    // creates 3 stack panels
                    // one to hold the other two stack panels
                    // one to hold customer names
                    // and one to hold count of appointments customer has
                    // also creates a border
                    StackPanel labelCol = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Margin = new Thickness(10, 5, 10, 5)
                    };
                    StackPanel valueCol = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Margin = new Thickness(10, 5, 10, 5)
                    };
                    StackPanel monthReport = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Margin = new Thickness(10, 10, 10, 10)
                    };
                    Border monthBorder = new Border
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1)
                    };

                    // create 2 text blocks to be colomn titles
                    TextBlock typeTitle = new TextBlock
                    {
                        Text = "Client Name",
                        TextDecorations = TextDecorations.Underline
                    };
                    TextBlock numberTitle = new TextBlock
                    {
                        Text = "Number of Appointments",
                        TextDecorations = TextDecorations.Underline
                    };

                    // add titles to the appropriate stack panels
                    labelCol.Children.Add(typeTitle);
                    valueCol.Children.Add(numberTitle);

                    foreach (var ac in grouped)
                    // loop through groups creating the 
                    // labels with customer name and count
                    {
                        Label groupName = new Label
                        {
                            Content = ac.Customer
                        };

                        Label number = new Label
                        {
                            Content = ac.Count
                        };

                        // add labels to stack panels
                        labelCol.Children.Add(groupName);
                        valueCol.Children.Add(number);
                    }
                    // add colomn stack panels to "table" stack panel
                    monthReport.Children.Add(labelCol);
                    monthReport.Children.Add(valueCol);

                    // put border around "table"
                    monthBorder.Child = monthReport;
                    
                    // add table to main stack panel
                    mainView.Children.Add(monthBorder);
                }
                else
                // if no appointments this month
                {
                    // create label to inform that there are no appointments
                    // and add to main stack
                    Label blank = new Label
                    {
                        Content = "Not meeting with any clients this month."
                    };
                    mainView.Children.Add(blank);
                }
                // add main stack panel to report window
                viewScroll.Content = mainView;  
            }
        }
    }
}
