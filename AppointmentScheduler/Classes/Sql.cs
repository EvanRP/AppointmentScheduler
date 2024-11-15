using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AppointmentScheduler.Classes
{
    internal class Sql
    {
        private static string DbIp = "127.0.0.1";
        private static string DbPort = "3306";
        private static string Db = "client_schedule";
        private static string DbUser = "sqlUser";
        private static string DbPass = "Passw0rd!";

        //private string DbConnectString = $"Server={DbIp};Port={DbPort};Database={Db};User Id ={DbUser};Password={DbPass};sslmode=none;";
        private string DbConnectString = $"Server={DbIp};Port={DbPort};Database={Db};User Id ={DbUser};Password={DbPass};";
        public Sql()
        {

        }

        public int GetLogon(string uName, string pass)
        {
            // return uid if login is correct
            // return -2 if login is invalid
            // return -3 if connection error

            // create sql Connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT `userId` FROM `user` WHERE `userName` = '{uName}' AND `password` = '{pass}';";

                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // read results
                            while (reader.Read())
                            {
                                return int.Parse(reader[0].ToString());
                            }
                            return -2;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -3;
                }
            }
        }

        public User GetUser(int userId)
        {
            // return user where user id = userId
            // retunr null if no user found

            // sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open connection
                    connect.Open();

                    // sql Query
                    string q = $"SELECT * FROM `user` WHERE `userId` = {userId}";
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // read results
                            while (reader.Read())
                            {
                                User u = new User(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[3].ToString()), DateTime.Parse(reader[4].ToString()), reader[5].ToString(), DateTime.Parse(reader[6].ToString()), reader[7].ToString());
                                return u;
                            }
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public int GetUID(string uName)
        {
            // returns userId for username
            // returns 0 if username not found

            // sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT `userId` FROM `user` WHERE `userName` = '{uName}'";
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read query results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return int.Parse(reader[0].ToString());
                            }
                            return 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
        public List<string> GetUserNames()
        {
            // returns list of usernames
            
            // sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    //sql query
                    string q = $"SELECT `userName` FROM `user`;";
                    List<string> uNames = new List<string>();

                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // read results
                            while (reader.Read())
                            {
                                uNames.Add(reader[0].ToString());
                            }
                            return uNames;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public List<Appointment> GetAppointments(int userId)
        {
            // returns list of appointments for user

            // sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql conntection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `appointment` WHERE `userId` = {userId}";
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            List<Appointment> appt = new List<Appointment>();

                            // read results
                            while (reader.Read())
                            {
                                Appointment a = new Appointment(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), DateTime.Parse(reader[9].ToString()), DateTime.Parse(reader[10].ToString()), DateTime.Parse(reader[11].ToString()), reader[12].ToString(), DateTime.Parse(reader[13].ToString()), reader[14].ToString());
                                appt.Add(a);
                            }
                            return appt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public Appointment GetAppointments(int userId, int apptId)
        {
            // returns appointment where user Id = userId and appointment id = apptId

            // sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `appointment` WHERE `userId` = {userId} AND `appointmentId` = {apptId}";
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Appointment appt = new();
                            while (reader.Read())
                            {
                                appt = new Appointment(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), DateTime.Parse(reader[9].ToString()), DateTime.Parse(reader[10].ToString()), DateTime.Parse(reader[11].ToString()), reader[12].ToString(), DateTime.Parse(reader[13].ToString()), reader[14].ToString());

                            }
                            return appt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public List<Appointment> GetAppointmentsForMonth(int userId, string dt)
        {
            // Returns list of appointments for user where start contains dt


            // create sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();
                    //string month = dt.ToString("yyyy-MM");
                    string month = dt;

                    // sql query
                    string q = $"SELECT * FROM `appointment` WHERE `userId` = {userId} AND `start` LIKE '{month}%';";
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            List<Appointment> list = new List<Appointment>();
                            Appointment appt = new();
                            while (reader.Read())
                            {
                                appt = new Appointment(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), DateTime.Parse(reader[9].ToString()), DateTime.Parse(reader[10].ToString()), DateTime.Parse(reader[11].ToString()), reader[12].ToString(), DateTime.Parse(reader[13].ToString()), reader[14].ToString());
                                list.Add(appt);
                            }
                            return list;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public List<Appointment> GetAppointmentsForMonth(string dt)
        {
            //returns list of appointments where start contains dt

            // sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();
                    //string month = dt.ToString("yyyy-MM");
                    string month = dt;

                    // sql query
                    string q = $"SELECT * FROM `appointment` WHERE `start` LIKE '{month}%';";

                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            List<Appointment> list = new List<Appointment>();
                            Appointment appt = new();
                            while (reader.Read())
                            {
                                appt = new Appointment(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), DateTime.Parse(reader[9].ToString()), DateTime.Parse(reader[10].ToString()), DateTime.Parse(reader[11].ToString()), reader[12].ToString(), DateTime.Parse(reader[13].ToString()), reader[14].ToString());
                                list.Add(appt);
                            }
                            return list;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public List<Customer> GetCustomers()
        {
            // returns list of customers

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `customer`";
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            List<Customer> customer = new List<Customer>();
                            while (reader.Read())
                            {
                                Customer c = new Customer(int.Parse(reader[0].ToString()), reader[1].ToString(), int.Parse(reader[2].ToString()), reader.GetByte(3) != 0, DateTime.Parse(reader[4].ToString()), reader[5].ToString(), DateTime.Parse(reader[6].ToString()), reader[7].ToString());
                                customer.Add(c);
                            }
                            return customer;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public int GetLastIndex(string tableName)
        {
            // returns next id from table
            string index = "";

            // uses table name to set what colomn to return
            switch (tableName)
            {
                case "user":
                    index = "userId";
                    break;
                case "appointment":
                    index = "appointmentId";
                    break;
                case "customer":
                    index = "customerId";
                    break;
                case "address":
                    index = "addressId";
                    break;
                case "city":
                    index = "cityId";
                    break;
                case "country":
                    index = "countryId";
                    break;
            }

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // opens sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT Max({index}) FROM `{tableName}`;";

                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return int.Parse(reader[0].ToString());
                            }
                            return 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public void AddAppointment(Appointment appt)
        {
            // adds appointment to db

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // sql connection
                    connect.Open();

                    // turn dateTime to string
                    DateTime start = appt.Start;
                    DateTime end = appt.End;
                    DateTime create = appt.CreateDate;
                    DateTime update = appt.LastUpdate;
                    string sStart = start.ToString("yyyy-MM-dd HH:mm");
                    string sEnd = end.ToString("yyyy-MM-dd HH:mm");
                    string sCreate = create.ToString("yyyy-MM-dd HH:mm");
                    string sUpdate = update.ToString("yyyy-MM-dd HH:mm");

                    //sql query
                    string q = $"INSERT INTO appointment(appointmentId, customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES ('{appt.ApptId}', '{appt.CustomerId}', '{appt.UserId}', '{appt.Title}', '{appt.Description}', '{appt.Location}', '{appt.Contact}', '{appt.ApptType}', '{appt.Url}', '{sStart}', '{sEnd}', '{sCreate}', '{appt.CreatedBy}', '{sUpdate}', '{appt.LastUpdateBy}');";

                    // execute query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }

        public void UpdateAppointment(Appointment appt)
        {
            // update appointment

            // create sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // convert DateTime to string
                    string sStart = appt.Start.ToString("yyyy-MM-dd HH:mm");
                    string sEnd = appt.End.ToString("yyyy-MM-dd HH:mm");
                    string sUpdate = appt.LastUpdate.ToString("yyyy-MM-dd HH:mm");

                    // sql query
                    string q = $"UPDATE appointment " +
                        $"SET customerId = '{appt.CustomerId}', title = '{appt.Title}', description ='{appt.Description}', location = '{appt.Location}', contact = '{appt.Contact}', type = '{appt.ApptType}', url = '{appt.Url}', start = '{sStart}', " +
                        $"end = '{sEnd}', lastUpdate = '{sUpdate}', lastUpdateBy = '{appt.LastUpdateBy}' " +
                        $"Where appointmentId = {appt.ApptId};";

                    // execute query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }

        public void DeleteRecord(string tableName, int recordId)
        {
            // deletes record where record id matches
            string index = "";

            // gets id colomn name
            switch (tableName)
            {
                case "user":
                    index = "userId";
                    break;
                case "appointment":
                    index = "appointmentId";
                    break;
                case "customer":
                    index = "customerId";
                    break;
                case "address":
                    index = "addressId";
                    break;
                case "city":
                    index = "cityId";
                    break;
                case "country":
                    index = "countryId";
                    break;
            }

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open connection
                    connect.Open();

                    // sql query
                    string q = $"DELETE FROM {tableName} WHERE {index} = '{recordId}';";

                    // execute query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }

        public Address GetAddress(int addressId)
        {
            // returns address 

            // create sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `address` WHERE `addressId` = '{addressId}';";

                    // execute command
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Address addr = new Address();

                            while (reader.Read())
                            {
                                addr.SetAddressId(int.Parse(reader[0].ToString()));
                                addr.SetAddress1(reader[1].ToString());
                                addr.SetAddress2(reader[2].ToString());
                                addr.SetCityId(int.Parse(reader[3].ToString()));
                                addr.SetPostalCode(reader[4].ToString());
                                addr.SetPhone(reader[5].ToString());
                                addr.SetCreateDate(DateTime.Parse(reader[6].ToString()));
                                addr.SetCreatedBy(reader[7].ToString());
                                addr.SetLastUpdate(DateTime.Parse(reader[8].ToString()));
                                addr.SetLastUpdatdeBy(reader[9].ToString());
                            }
                            return addr;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public City GetCity(int cityId)
        {
            // returns city

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // opens sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `` WHERE `cityId` = '{cityId}';";

                    // execute query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            City c = new City();

                            while (reader.Read())
                            {
                                c.SetCityId(int.Parse(reader[0].ToString()));
                                c.SetCityName(reader[1].ToString());
                                c.SetCountryId(int.Parse(reader[2].ToString()));
                                c.SetCreateDate(DateTime.Parse(reader[3].ToString()));
                                c.SetCreateBy(reader[4].ToString());
                                c.SetLastUpdate(DateTime.Parse(reader[5].ToString()));
                                c.SetLastUpdateBy(reader[6].ToString());
                            }

                            return c;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public Country GetCountry(int countryId)
        {
            // returns country
            
            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // opens sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `country` WHERE `countryId` = '{countryId}';";

                    // execute sql query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Country c = new();

                            while (reader.Read())
                            {
                                c.SetCountryId(int.Parse(reader[0].ToString()));
                                c.SetCountryName(reader[1].ToString());
                                c.SetCreateDate(DateTime.Parse(reader[2].ToString()));
                                c.SetCreateBy(reader[3].ToString());
                                c.SetLastUpdate(DateTime.Parse(reader[4].ToString()));
                                c.SetLastUpdateBy(reader[5].ToString());
                            }

                            return c;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public List<Address> GetAddresses()
        {
            // returns list of all address

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // opens sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `address`;";

                    // execuctes sql query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            List<Address> addr = new();

                            while (reader.Read())
                            {
                                Address a = new Address();

                                a.SetAddressId(int.Parse(reader[0].ToString()));
                                a.SetAddress1(reader[1].ToString());
                                a.SetAddress2(reader[2].ToString());
                                a.SetCityId(int.Parse(reader[3].ToString()));
                                a.SetPostalCode(reader[4].ToString());
                                a.SetPhone(reader[5].ToString());
                                a.SetCreateDate(DateTime.Parse(reader[6].ToString()));
                                a.SetCreatedBy(reader[7].ToString());
                                a.SetLastUpdate(DateTime.Parse(reader[8].ToString()));
                                a.SetLastUpdatdeBy(reader[9].ToString());

                                addr.Add(a);
                            }
                            return addr;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public List<City> GetCitys()
        {
            // returns list of all cities

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `city`;";

                    // execute sql query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            List<City> citys = new();

                            while (reader.Read())
                            {
                                City c = new();
                                c.SetCityId(int.Parse(reader[0].ToString()));
                                c.SetCityName(reader[1].ToString());
                                c.SetCountryId(int.Parse(reader[2].ToString()));
                                c.SetCreateDate(DateTime.Parse(reader[3].ToString()));
                                c.SetCreateBy(reader[4].ToString());
                                c.SetLastUpdate(DateTime.Parse(reader[5].ToString()));
                                c.SetLastUpdateBy(reader[6].ToString());

                                citys.Add(c);
                            }

                            return citys;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public List<Country> GetCountrys()
        {
            // returns list of all countries

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `country`";

                    // execute sql query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            List<Country> countrys = new();

                            while (reader.Read())
                            {
                                Country c = new();
                                c.SetCountryId(int.Parse(reader[0].ToString()));
                                c.SetCountryName(reader[1].ToString());
                                c.SetCreateDate(DateTime.Parse(reader[2].ToString()));
                                c.SetCreateBy(reader[3].ToString());
                                c.SetLastUpdate(DateTime.Parse(reader[4].ToString()));
                                c.SetLastUpdateBy(reader[5].ToString());
                                countrys.Add(c);
                            }
                            return countrys;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public Address GetAddress(string addr)
        {
            // returns address where address1 == addr

            // create sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `address` WHERE `address` = '{addr}';";

                    // execute sql query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Address a = new Address();

                            while (reader.Read())
                            {
                                a.SetAddressId(int.Parse(reader[0].ToString()));
                                a.SetAddress1(reader[1].ToString());
                                a.SetAddress2(reader[2].ToString());
                                a.SetCityId(int.Parse(reader[3].ToString()));
                                a.SetPostalCode(reader[4].ToString());
                                a.SetPhone(reader[5].ToString());
                                a.SetCreateDate(DateTime.Parse(reader[6].ToString()));
                                a.SetCreatedBy(reader[7].ToString());
                                a.SetLastUpdate(DateTime.Parse(reader[8].ToString()));
                                a.SetLastUpdatdeBy(reader[9].ToString());
                            }
                            return a;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public City GetCity(string cName) 
        {
            // returns city where city == cName

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `city` WHERE `city` = '{cName}';";

                    //executes sql query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read result
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            City c = new();

                            while (reader.Read())
                            {
                                
                                c.SetCityId(int.Parse(reader[0].ToString()));
                                c.SetCityName(reader[1].ToString());
                                c.SetCountryId(int.Parse(reader[2].ToString()));
                                c.SetCreateDate(DateTime.Parse(reader[3].ToString()));
                                c.SetCreateBy(reader[4].ToString());
                                c.SetLastUpdate(DateTime.Parse(reader[5].ToString()));
                                c.SetLastUpdateBy(reader[6].ToString());
                            }
                            return c;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public Country GetCountry(string cName)
        {
            // returns country where country == cName

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `country` WHERE `country` = '{cName}';";

                    //execute sql query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Country c = new();

                            while (reader.Read())
                            {

                                c.SetCountryId(int.Parse(reader[0].ToString()));
                                c.SetCountryName(reader[1].ToString());
                                c.SetCreateDate(DateTime.Parse(reader[2].ToString()));
                                c.SetCreateBy(reader[3].ToString());
                                c.SetLastUpdate(DateTime.Parse(reader[4].ToString()));
                                c.SetLastUpdateBy(reader[5].ToString());
                            }
                            return c;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public Customer GetCustomer(int cId)
        {
            // returns customer where customerId == cId

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // sql query
                    string q = $"SELECT * FROM `customer` WHERE `customerId` = '{cId}';";

                    // execute sql query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        // read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Customer c = new();

                            while (reader.Read())
                            {
                                c.SetCustomerId(int.Parse(reader[0].ToString()));
                                c.SetCustomerName(reader[1].ToString());
                                c.SetAddressId(int.Parse(reader[2].ToString()));
                                c.SetActive(reader.GetByte(3) != 0);
                                c.SetCreateDate(DateTime.Parse(reader[4].ToString()));
                                c.SetCreateBy(reader[5].ToString());
                                c.SetLastUpdate(DateTime.Parse(reader[6].ToString()));
                                c.SetLastUpdateBy(reader[7].ToString());   
                            }
                            return c;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public void AddCustomer(Customer customer)
        {
            // add customer to db

            // creates sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // convert DateTime to string
                    string sCreate = customer.GetCreateDate().ToString("yyyy-MM-dd HH:mm");
                    string sUpdate = customer.GetLastUpdate().ToString("yyyy-MM-dd HH:mm");
                    int active = 0;

                    if (customer.GetActive())
                    {
                        active = 1;
                    }

                    // sql query
                    string q = $"INSERT INTO customer(customerId, customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES('{customer.GetCustomerId()}', '{customer.GetCustomerName()}', '{customer.GetAddressId()}', '{active}', '{sCreate}', '{customer.GetCreatedBy()}', '{sUpdate}', '{customer.GetLastUpdateBy()}');";

                    // execute sql query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }
        public void AddAddress (Address addr)
        {
            // add address to db

            // create sql connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // convert DateTime to string
                    string sCreate = addr.GetCreateDate().ToString("yyyy-MM-dd HH:mm");
                    string sUpdate = addr.GetLastUpdate().ToString("yyyy-MM-dd HH:mm");

                    // sql query
                    string q = $"INSERT INTO address(addressId, address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES('{addr.GetAddressId()}', '{addr.GetAddress1()}', '{addr.GetAddress2()}', '{addr.GetCityId()}', '{addr.GetPostalCode()}', '{addr.GetPhone()}', '{sCreate}', '{addr.GetCreateBy()}', '{sUpdate}', '{addr.GetLastUpdatedBy()}');";

                    // execute query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }
        public void AddCity(City city)
        {
            // add city to db

            // create db connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // convert DateTime to string
                    string sCreate = city.GetCreateDate().ToString("yyyy-MM-dd HH:mm");
                    string sUpdate = city.GetLastUpdate().ToString("yyyy-MM-dd HH:mm");

                    // sql query
                    string q = $"INSERT INTO city(cityId, city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES('{city.GetCityId()}', '{city.GetCityName()}', '{city.GetCountryId()}', '{sCreate}', '{city.GetCreateBy()}','{sUpdate}', '{city.GetLastUpdatedBy()}');";

                    // execute query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }
        public void AddCountry(Country country)
        {
            // add country to db

            // create spl connection
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // convert DateTime to string
                    string sCreate = country.GetCreateDate().ToString("yyyy-MM-dd HH:mm");
                    string sUpdate = country.GetLastUpdate().ToString("yyyy-MM-dd HH:mm");

                    // sql query
                    string q = $"INSERT INTO country(countryId, country, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES('{country.GetId()}', '{country.GetCountryName()}', '{sCreate}', '{country.GetCreateBy()}', '{sUpdate}', '{country.GetLastUpdatedBy()}');";

                    // execute query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }
        public void UpdateCustomer(Customer c)
        {
            // update customer

            // create sql connetion
            using (MySqlConnection connect = new MySqlConnection(DbConnectString))
            {
                try
                {
                    // open sql connection
                    connect.Open();

                    // convert DateTime to string
                    string sUpdate = c.GetLastUpdate().ToString("yyyy-MM-dd HH:mm");
                    int active = 0;
                    if (c.GetActive())
                    {
                        active = 1;
                    }

                    // sql query
                    string q = $"UPDATE `customer`" +
                        $"SET `customerName` = '{c.GetCustomerName()}', `addressId` = '{c.GetAddressId()}', `active` = '{active}', `lastUpdateBy` = '{c.GetLastUpdateBy()}', `lastUpdate` = '{sUpdate}'" +
                        $"Where `customerId` = '{c.GetCustomerId()}';";

                    // execute sql query
                    using (MySqlCommand command = new MySqlCommand(q, connect))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }
    }
}
