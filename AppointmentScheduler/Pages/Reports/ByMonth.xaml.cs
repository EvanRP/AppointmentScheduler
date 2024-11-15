using AppointmentScheduler.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppointmentScheduler.Pages.Reports
{
    /// <summary>
    /// Interaction logic for ByMonth.xaml
    /// </summary>
    public partial class ByMonth : Window
    {
        public ByMonth(int userId)
        {
            InitializeComponent();
            List<string> completed = new List<string>();

            Sql db = new Sql();


            StackPanel mainStack = new();

            for (int i = 1; i < 13; i++)
            {
                Label month = new();
                string year = DateTime.Now.Year.ToString();
                string yearAndMonth = "";

                // set variables based on month
                switch (i){
                    case 1:
                        yearAndMonth = year + "-01";
                        month.Content = "January";
                        break;
                    case 2:
                        yearAndMonth = year + "-02";
                        month.Content = "February";
                        break;
                    case 3:
                        yearAndMonth = year + "-03";
                        month.Content = "March";
                        break;
                    case 4:
                        yearAndMonth = year + "-04";
                        month.Content = "April";
                        break;
                    case 5:
                        yearAndMonth = year + "-05";
                        month.Content = "May";
                        break;
                    case 6:
                        yearAndMonth = year + "-06";
                        month.Content = "June";
                        break;
                    case 7:
                        yearAndMonth = year + "-07";
                        month.Content = "July";
                        break;
                    case 8:
                        yearAndMonth = year + "-08";
                        month.Content = "August";
                        break;
                    case 9:
                        yearAndMonth = year + "-09";
                        month.Content = "September";
                        break;
                    case 10:
                        yearAndMonth = year + "-10";
                        month.Content = "October";
                        break;
                    case 11:
                        yearAndMonth = year + "-11";
                        month.Content = "November";
                        break;
                    case 12:
                        yearAndMonth = year + "-12";
                        month.Content = "December";
                        break;
                }
                //gets list of appointments form the user with in the month
                //List<Appointment> appointments = db.GetAppointmentsForMonth(userId, yearAndMonth);

                // get list of appointments within the month
                List<Appointment> appointments = db.GetAppointmentsForMonth(yearAndMonth);

                // group appointments by type and count how many appointments are in the group
                var grouped = appointments
                    .GroupBy(a => a.ApptType)
                    .Select(group => new { ApptType = group.Key, Count = group.Count() });

                // add month label to stack
                // and make label centered
                mainStack.Children.Add(month);
                month.HorizontalAlignment = HorizontalAlignment.Center;

                if (appointments.Count() > 0)
                    // checks if month has appointments
                {
                    // creates 3 stack panels
                    // one to hold the other two stack panels
                    // one to hold appointment type names
                    // and one to hold count of appointment types
                    // also creates a border
                    StackPanel labelCol = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Margin = new Thickness(10,5,10,5)
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
                        Text = "Appointment Type",
                        TextDecorations = TextDecorations.Underline
                    };
                    TextBlock numberTitle = new TextBlock
                    {
                        Text = "Number of appointments",
                        TextDecorations = TextDecorations.Underline
                    };

                    // add titles to the appropriate stack panels
                    labelCol.Children.Add(typeTitle);
                    valueCol.Children.Add(numberTitle);

                    foreach (var group in grouped)
                     // loop through groups creating the 
                     // labels with appointment type and count
                    {
                        Label groupName = new Label
                        {
                            Content = group.ApptType
                        };

                        Label number = new Label
                        {
                            Content = group.Count
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
                    mainStack.Children.Add(monthBorder);
                    
                }
                else
                // if no appointments this month
                {
                    // create label to inform that there are no appointments
                    // and add to main stack
                    Label noAppt = new Label
                    {
                        Content = "No appointments"
                    };
                    mainStack.Children.Add(noAppt);
                }
                // add main stack panel to report window
                scrollView.Content = mainStack;
            }
            
        }
    }
}
