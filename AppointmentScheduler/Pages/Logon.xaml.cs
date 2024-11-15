using AppointmentScheduler.Classes;
using System;
using System.IO;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;


namespace AppointmentScheduler.Pages
{
    /// <summary>
    /// Interaction logic for Logon.xaml
    /// </summary>
    public partial class Logon : Page
    {
        Frame theFrame;
        public Logon(Frame mainFrame)
        {
            theFrame = mainFrame;
            InitializeComponent();
            loginFailed.Visibility = Visibility.Hidden;
            ConnectError.Visibility = Visibility.Hidden;

            // get language and timezone
            string language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            TimeZoneInfo time = TimeZoneInfo.Local;

            switch (language)
            {
                case "en":
                    // content if language is english
                    userLabel.Content = "Username:";
                    passLabel.Content = "Password:";
                    Login.Content = "Login";
                    loginFailed.Content = "Invalid Username or Password";
                    ConnectError.Content = "Error connecting to database try again later.";
                    something.Content = $"Timezone is {time.StandardName}";
                    break;
                case "es":
                    // content if language is spanish
                    userLabel.Content = "Nombre de usuario:";
                    passLabel.Content = "Contraseña:";
                    Login.Content = "Acceso";
                    loginFailed.Content = "Usuario o contraseña invalido";
                    ConnectError.Content = "Error al conectarse a la base de datos, inténtelo nuevamente más tarde.";
                    something.Content = $"la zona horaria es {time.StandardName}";
                    break;
            }
        }

        public void loginClicked(object sender, RoutedEventArgs e)
        {
            // hide failed logon and error messages
            loginFailed.Visibility = Visibility.Hidden;
            ConnectError.Visibility = Visibility.Hidden;
            Sql db = new();

            // get timezone info
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            // get current time
            DateTime now = DateTime.Now;
            // convert current time to UTC
            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(now, localZone);

            // try logon
            int result = db.GetLogon(UserNameTextBox.Text, PasswordTextBox.Text);

            // logon faile or error based on int result
            switch (result)
            {
                case -2:
                    // log Failed logon attempt
                    log($"{utcTime.ToString("yyyy-MM-dd HH:mm")}z {UserNameTextBox.Text} had a unsuccessful logged in attempt.\n");
                    // unhide failed logon text
                    loginFailed.Visibility = Visibility.Visible;
                    break;
                case -3:
                    // unhide connection error text
                    ConnectError.Visibility = Visibility.Visible;
                    break;
                default:
                    // log sucessful logon attempt
                    log($"{utcTime.ToString("yyyy-MM-dd HH:mm")}z {UserNameTextBox.Text} Successfully logged in.\n");
                    // navigate to user portal page
                    theFrame.Navigate(new UserPortal(result, theFrame, true));
                    break;
            }
        }

        public void log(string toWrite)
        {
            // write to logon log file Login_History.txt

            string filePath = "..\\..\\..\\Login_History.txt";
            try
            {
               File.AppendAllText(filePath, toWrite);
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
