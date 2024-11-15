using System.Windows;
using System.Windows.Controls;


namespace AppointmentScheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Frame MainFrame { get; set; }
        public int UserId { get; set; }
        public MainWindow()
        {
            // create frame
            MainFrame = new Frame();
            InitializeComponent();
            
            // use frame for logon page
            MainFrame.Navigate(new Pages.Logon(MainFrame));
            // disable navigation elements
            MainFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            // add frame to grid
            mainGrid.Children.Add(MainFrame);
        }
    }
}
