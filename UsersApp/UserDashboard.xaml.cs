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
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Window
    {
        public UserDashboard()
        {
            InitializeComponent();
            Get_All_Users();
        }


        private void Get_All_Users()
        {

            var users = Database.GetUsers(); // get all users from SQLite
            if (users.Count == 0)
            {
               
                return;
            }
           //var sb = new StringBuilder(); // used to build text efficiently
            
            List<User> users1 = users.ToList();
            DisplayUsers.ItemsSource = users1;

        }

    }
}
