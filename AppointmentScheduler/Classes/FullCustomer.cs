using System.Collections.Generic;
using System.Linq;

namespace AppointmentScheduler.Classes
{
    class FullCustomer
    {
        // properties public getter private setter
        public int CustomerId { get; private set; }
        public int AddrId { get; private set; }
        public int CityId { get; private set; }
        public int CountryId { get; private set; }
        public bool Active {  get; private set; }
        public string CustomerName { get; private set; }
        public string Address { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public string Phone { get; private set; }

        // constructors
        public FullCustomer()
        {
            this.CustomerId = 0;
            this.AddrId = 0;
            this.CityId = 0;
            this.CountryId = 0;
            this.CustomerName = "";
            this.Address = "";
            this.Address2 = "";
            this.City = "";
            this.PostalCode = "";
            this.Country = "";
            this.Phone = "";
        }

        public FullCustomer(Customer customer, List<City> citys, List<Country> countrys, List<Address> addresses)
        {
            Address addr = addresses.FirstOrDefault(a => a.GetAddressId() == customer.GetAddressId(), null);
            City c = citys.FirstOrDefault(c => c.GetCityId() == addr.GetCityId(), null);
            Country country = countrys.FirstOrDefault(country => country.GetId() == c.GetCountryId(), null);

            this.CustomerId = customer.GetCustomerId();
            this.AddrId = addr.GetAddressId();
            this.CityId = c.GetCityId();
            this.CountryId = country.GetId();
            this.CustomerName = customer.GetCustomerName();
            this.Address = addr.GetAddress1();
            this.Address2 = addr.GetAddress2();
            this.City = c.GetCityName();
            this.PostalCode = addr.GetPostalCode();
            this.Country = country.GetCountryName();
            this.Phone = addr.GetPhone();
            this.Active = customer.GetActive();
        }
    }
}
