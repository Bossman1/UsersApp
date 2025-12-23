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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace UsersApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        

        public MainWindow()
        {
            InitializeComponent();

            Database.Initialize();
        }

        private void Registration_Button(object sender, RoutedEventArgs e)
        {
            string Username = TextBoxUsername.Text.Trim();
            string Email = TextBoxEmail.Text.Trim().ToLower();
            string Password = TextBoxPassword.Password.Trim();
            string PasswordAgain = TextBoxPasswordAgain.Password.Trim();


            TextBoxUsername.Background = Brushes.White;
            TextBoxEmail.Background = Brushes.White;
            TextBoxEmail.Background = Brushes.White;
            TextBoxPassword.Background = Brushes.White;
            TextBoxPasswordAgain.Background = Brushes.White;

            bool isError = true;

            if (Username == "")
            {
                TextBoxUsername.Background = Brushes.MistyRose;
                isError = false;
            }

            if (Password == "")
            {
              
                TextBoxPassword.Background = Brushes.MistyRose;
                isError = false;
            }

            if (PasswordAgain == "")
            {
                
                TextBoxPasswordAgain.Background = Brushes.MistyRose;
                isError = false;
            }

            if (Username.Length < 3)
            {
               
                TextBoxPassword.Background = Brushes.MistyRose;
                isError = false;
            }

            if (Email == "" || !Email.Contains("@") || !Email.Contains("."))
            {
                
                TextBoxEmail.Background = Brushes.MistyRose;
                isError = false;
            }

            if (Password != PasswordAgain)
            {
                
                TextBoxPassword.Background = Brushes.MistyRose;
                isError = false;
            }


            if (isError)
            {


                var user = new User(Username, Email, Password);
                Database.AddUser(user); 
              

                MessageBox.Show("Done, User was created");
               
            }

        }
    }
}
