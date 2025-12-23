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

namespace UsersApp
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Login_Btn(object sender, RoutedEventArgs e)
        {
            string Login = TextBoxUsername.Text.Trim();
            string Password = TextBoxPassword.Password.Trim();


            TextBoxUsername.Background = Brushes.White;
            TextBoxPassword.Background = Brushes.White;

            bool isError = true;

            if (Login == "")
            {

                TextBoxUsername.Background = Brushes.MistyRose;
                isError = false;
            }

            if (Password == "")
            {

                TextBoxPassword.Background = Brushes.MistyRose;
                isError = false;
            }

            if (isError)
            {
                bool count = Database.ValidateUser(Login, Password);
                if (count)
                {
                    UserDashboard userDashboard = new UserDashboard();
                    this.Close();
                    userDashboard.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //Console.WriteLine($"Attempting login with Username: {Login} and Password: {Password} Cound: {count} ");

            }

        }




        private void Open_Registration_Window(object sender, RoutedEventArgs e)
        {
            MainWindow registrationWindow = new MainWindow();
            registrationWindow.Show();
            this.Close();
        }

    }
}
