using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppointmentScheduler.Classes;

namespace AppointmentScheduler.Pages.Customers
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
            Sql db = new();

            // get user
            uId = userId;
            User u = db.GetUser(uId);
            uName = u.GetUserName();

            // list for Customer status comboBox
            List<string> activeList = new()
            {
                "active",
                "inactive"
            };

            // set source for customer status comboBox
            ActiveBox.ItemsSource = activeList;

            // disable save button
            SaveButton.IsEnabled = false;

        }

        public void saveClicked(object sender, RoutedEventArgs e)
        {
            Customer customer = new();
            Address addr = new();
            City ci = new();
            Country count = new();

            Sql db = new();

            // get next ID for Customer, Address, City, and Country
            int nextCId = db.GetLastIndex("customer") + 1;
            int nextAId = db.GetLastIndex("address") + 1;
            int nextCityId = db.GetLastIndex("city") + 1;
            int nextCountryId = db.GetLastIndex("country") + 1;

            // get customer status and convert to int
            string act = ActiveBox.SelectedItem.ToString();
            bool active = false;

            if (act == "active")
            {
                active = true;
            }
            else
            {
                active = false;
            }

            // set input field values to instances of customer, address, city, and country
            customer.SetCustomerId(nextCId);
            customer.SetCustomerName(CustomerName.Text);
            customer.SetActive(active);
            customer.SetCreateDate(DateTime.Now);
            customer.SetLastUpdate(DateTime.Now);
            customer.SetLastUpdateBy(uName);
            customer.SetCreateBy(uName);

            addr.SetAddress1(Address.Text);
            addr.SetAddress2(Address2.Text);
            addr.SetPostalCode(postalCode.Text);
            addr.SetPhone(Phone.Text);

            ci.SetCityName(City.Text);
            count.SetCountryName(Country.Text);

            // checks if country information provide is new or an existing country within the db 
            Country countryExists = db.GetCountry(count.GetCountryName());
            if (countryExists.GetId() > 0)
            // is existing country
            {
                count = countryExists;
            }
            else
            // is a new country
            {
                // set values update db
                count.SetCountryId(nextCountryId);
                count.SetCreateDate(DateTime.Now);
                count.SetLastUpdate(DateTime.Now);
                count.SetLastUpdateBy(uName);
                count.SetCreateBy(uName);
                
                db.AddCountry(count);
            }

            // checks if city information provided is a new or existing city within the db
            City cityExists = db.GetCity(ci.GetCityName());
            if (cityExists.GetCityId() > 0)
            // is existing city
            {
                ci = cityExists;
                
            }
            else
            // is a new city
            {
                // set values update db
                ci.SetCityId(nextCityId);
                ci.SetCountryId(count.GetId());
                ci.SetCreateDate(DateTime.Now);
                ci.SetLastUpdateBy(uName);
                ci.SetCreateBy(uName);
                ci.SetLastUpdate(DateTime.Now);

                db.AddCity(ci);
            }


            addr.SetCityId(ci.GetCityId());

            // checks if address information provided is a new or existing address within the db
            Address addrExists = db.GetAddress(addr.GetAddress1());
            if (addrExists.GetAddressId() > 0)
            // might be an existing address
            {
                bool same = addr.GetAddress2().Equals(addrExists.GetAddress2()) &&
                    addr.GetCityId().Equals(addrExists.GetCityId()) &&
                    addr.GetPhone().Equals(addrExists.GetPhone()) &&
                    addr.GetPostalCode().Equals(addrExists.GetPostalCode());
                if (same)
                // is an existing address
                {
                    addr = addrExists;
                    
                }
                else
                // is a new address
                {
                    // set values update db
                    addr.SetAddressId(nextAId);
                    addr.SetCreateDate(DateTime.Now);
                    addr.SetLastUpdate(DateTime.Now);
                    addr.SetLastUpdatdeBy(uName);
                    addr.SetCreatedBy(uName);
                    
                    db.AddAddress(addr);
                }

            }
            else
            // is a new address
            {
                // set values update db
                addr.SetAddressId(nextAId);
                addr.SetCityId(ci.GetCityId());
                addr.SetCreateDate(DateTime.Now);
                addr.SetLastUpdate(DateTime.Now);
                addr.SetLastUpdatdeBy(uName);
                addr.SetCreatedBy(uName);

                db.AddAddress(addr);
            }

            // set customer address id and update db
            customer.SetAddressId(addr.GetAddressId());
            db.AddCustomer(customer);

            // close this window
            this.Close();
        }
        public void cancelCLicked(object sender, RoutedEventArgs e)
        {
            // close this window
            this.Close();
        }

        public void CheckNull()
        {
            // check for null fields
            bool allBoxes = !string.IsNullOrEmpty(CustomerName.Text) &&
                !string.IsNullOrEmpty(Address.Text) &&
                !string.IsNullOrEmpty(Address2.Text) &&
                !string.IsNullOrEmpty(City.Text) &&
                ActiveBox.SelectedItem != null &&
                !string.IsNullOrEmpty(postalCode.Text) &&
                !string.IsNullOrEmpty(Country.Text) &&
                !string.IsNullOrEmpty(Phone.Text);

            // enable save button is all required fields are filled
            // disable if null
            if (allBoxes)
            {
                SaveButton.IsEnabled = true;
            }
            else
            {
                SaveButton.IsEnabled = false;
            }
        }
        public void IsBoxNull(object sender, TextChangedEventArgs e)
        {
            //event that calles checkNull
            CheckNull();
        }
        public void NumOrDashCheck(object sender, TextCompositionEventArgs e)
        {
            // check if input is numeric or a dash
            // does not allow anything else to be typed in checked field
            if (int.TryParse(e.Text, out _) || e.Text == "-")
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public void ComboBoxChange(object sender, SelectionChangedEventArgs e)
        {
            // event that calls checkNull
            CheckNull();
        }
    }
}
