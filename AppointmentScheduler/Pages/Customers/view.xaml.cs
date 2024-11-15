using AppointmentScheduler.Classes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AppointmentScheduler.Pages.Customers
{
    /// <summary>
    /// Interaction logic for Customer.View.xaml
    /// </summary>
    public partial class View : Page
    {
        int currentUser;
        Frame thisFrame;

        public View(int userId, Frame mainFrame)
        {
            // frame for window
            thisFrame = mainFrame;
            InitializeComponent();
            currentUser = userId;

            Sql db = new();

            // get current user
            User u = db.GetUser(currentUser);
            // set username label
            userNameLabel.Content = u.GetUserName();

            // fill dataGrid
            GetGridContent();
        }

        public void homeClicked(object sender, RoutedEventArgs e)
        {
            // return to user portal
            thisFrame.Navigate(new UserPortal(currentUser, thisFrame, false));
        }

        public void logoutClicked(object sender, RoutedEventArgs e)
        {
            // return to logon page
            thisFrame.Navigate(new Pages.Logon(thisFrame));
        }

        public void addClicked(object sender, RoutedEventArgs e)
        {
            // create new instance of customer.add
            Customers.Add addCustomer = new Customers.Add(currentUser);
            // open customer.add window
            addCustomer.ShowDialog();
            // update dateGrid
            GetGridContent();
        }
        public void editClicked(object sender, RoutedEventArgs e)
        {
            // selected row
            var selected = dataGrid.SelectedItem;
            if(selected != null)
            {
                // cast selected row to FullCustomer
                FullCustomer select = selected as FullCustomer;
                // create new instance of customer.edit
                Edit editWindow = new Edit(currentUser, select);
                // open customer.edit window
                editWindow.ShowDialog();
                // update dataGrid
                GetGridContent();
            }
            // error if there is no row selected
            else
            {
                MessageBox.Show("Please select a customer to edit first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void deleteClicked(object sender, RoutedEventArgs e)
        {
            // selected row
            var selected = dataGrid.SelectedItem;

            
            if(selected != null)
            {
                // cast selected row to fullCustomer
                FullCustomer select = selected as FullCustomer;

                // Confirm Delete
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete customer {select.CustomerName}?", "Conformation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.No)
                {
                    return;
                }

                Sql db = new Sql();

                // delete selected row and update db
                db.DeleteRecord("customer", select.CustomerId);
                // update dataGrid
                GetGridContent();
            }
            // error if nothing is selected
            else
            {
                MessageBox.Show("No customer selected to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetGridContent()
        {
            // update datagrid source
            Sql db = new();

            // create list of customers
            List<Customer> customers = db.GetCustomers();
            // create list of cities
            List<City> citys = db.GetCitys();
            // create list of countrys
            List<Country> countrys = db.GetCountrys();
            // create list of addresses
            List<Address> addresses = db.GetAddresses();

            // create new instance of fullCustomer that is used to combine the above lists
            List<FullCustomer> full = new List<FullCustomer>();
            
            if (customers != null)
            {
                foreach (Customer customer in customers)
                {
                    // combine lists
                    FullCustomer f = new FullCustomer(customer, citys, countrys, addresses);
                    // added instace of fullCustomer to full
                    full.Add(f);
                }
            }
            // set DateGrid source to list of FullCustomers
            dataGrid.ItemsSource = full;
        }

    }
}
