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

            //ShowUsers();

        }


        private void ShowUsers()
        {
            var users = Database.GetUsers(); // get all users from SQLite

            if (users.Count == 0)
            {

                //DisplayRegisteredUsers.Text = "No users found.";
                return;
            }

            var sb = new StringBuilder(); // used to build text efficiently

            foreach (var user in users)
            {
                sb.AppendLine($"ID: {user.Id}");
                sb.AppendLine($"Login: {user.Login}");
                sb.AppendLine($"Email: {user.Email}");
                sb.AppendLine("------------------------");
            }

            //DisplayRegisteredUsers.Text = sb.ToString(); // display result in TextBlock
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


                User user = new User(Username, Email, Password);
                Database.AddUser(user);

                TextBoxUsername.Text = "";
                TextBoxEmail.Text = "";
                TextBoxPassword.Password = "";
                TextBoxPasswordAgain.Password = "";

                MessageBox.Show("Done, User was created successfully");

            }

        }

        private void Open_Login_Window(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }
    }
}
