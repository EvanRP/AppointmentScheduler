using AppointmentScheduler.Classes;
using System;
using System.Collections.Generic;
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

namespace AppointmentScheduler.Pages.Customers
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        
        string uName;
        int customerId;

        internal Edit(int userId, FullCustomer customerInfo)
        {
            // set Customer ID
            customerId = customerInfo.CustomerId;
            
            Sql db = new();

            // get user
            User u = db.GetUser(userId);
            // set username
            uName = u.GetUserName();

            InitializeComponent();

            // create list for comboBox customer status
            List<string> activeList = new()
            {
                "active",
                "inactive"
            };

            // fill page fields
            ActiveBox.ItemsSource = activeList;
            CustomerName.Text = customerInfo.CustomerName;
            Address.Text = customerInfo.Address;
            Address2.Text = customerInfo.Address2;
            City.Text = customerInfo.City;
            postalCode.Text = customerInfo.PostalCode;
            Country.Text = customerInfo.Country;
            Phone.Text = customerInfo.Phone;

            // set customer status comboBox
            if(customerInfo.Active)
            {
                ActiveBox.SelectedIndex = ActiveBox.Items.IndexOf("active");
            }
            else
            {
                ActiveBox.SelectedIndex = ActiveBox.Items.IndexOf("inactive");
            }
        }

        public void cancelClicked(object sender, EventArgs e)
        {
            // close page
            this.Close();
        }
        public void saveClicked(object sender, EventArgs e)
        {
            Customer customer = new();
            Address addr = new();
            City ci = new();
            Country count = new();

            Sql db = new();

            // get next ids for address, city, and country
            int nextAId = db.GetLastIndex("address") + 1;
            int nextCityId = db.GetLastIndex("city") + 1;
            int nextCountryId = db.GetLastIndex("country") + 1;

            // gets customer status comboBox value
            string act = ActiveBox.SelectedItem.ToString();
            bool active = false;

            // converts customer's status to an integer
            if (act == "active")
            {
                active = true;
            }
            else
            {
                active = false;
            }

            // set page's field values to instance of customer, address, city, and country
            customer.SetCustomerId(customerId);
            customer.SetCustomerName(CustomerName.Text);
            customer.SetActive(active);
            customer.SetLastUpdate(DateTime.Now);
            customer.SetLastUpdateBy(uName);

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
                    addr.SetCityId(ci.GetCityId());
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
            db.UpdateCustomer(customer);

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
