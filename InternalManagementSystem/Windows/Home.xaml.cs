using InternalManagementSystem.Pages;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace InternalManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();            
        }
        
        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Btn_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void RedirectToProjects(object sender, RoutedEventArgs e)
        {
            Main.Content = new Projects();
        }

        private void RedirectToClients(object sender, RoutedEventArgs e)
        {
            Main.Content = new Clients();
        }

        private void RedirectToDashboard(object sender, RoutedEventArgs e)
        {
            Main.Content = new Dashboard();
        }

        private void RedirectToInvoice(object sender, RoutedEventArgs e)
        {
            Main.Content = new Invoice();
        }

        private void RedirectToMessages(object sender, RoutedEventArgs e)
        {
            Main.Content = new Messages();
        }
    }
}