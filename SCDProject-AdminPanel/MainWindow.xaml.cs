using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using URSBackend.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SCDProject_AdminPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoginButtonHandler(object sender, RoutedEventArgs e)
        {
            //process credentials
            if (usernameTextbox.Text.Length == 0 || passwordBox.Password.Length == 0)
            {
                MessageBox.Show("Error: Empty username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool loginSuccessful = await RequestHandler.Login(new User()
            {
                Username = usernameTextbox.Text,
                Password = passwordBox.Password
            });

            if (loginSuccessful)
            {
                // create new window and hide previous one
                this.Visibility = Visibility.Hidden;
                Window dashboardWindow = new Window();
                if (usernameTextbox.Text == "admin")
                    dashboardWindow = new Dashboard();
                else if (usernameTextbox.Text == "registrar")
                    dashboardWindow = new RegistrarWindow();
                Application.Current.MainWindow = dashboardWindow;
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                dashboardWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Incorrect username or password", "Unauthorized", MessageBoxButton.OK, MessageBoxImage.Error);
                //usernameTextbox.Clear();
                passwordBox.Clear();
            }
        }
    }
}
