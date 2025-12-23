using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersApp
{
    internal class User
    {

        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public User() { }

        public User(string login, string email, string password)
        {
            this.Login = login;
            this.Email = email;
            this.Password = password;
        }

    }
}
