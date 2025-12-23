using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace UsersApp
{
    internal class ApplicationContext : DbContext
    {

        public DbSet<User> User { get; set; }


        public ApplicationContext() : base("DefaultConnection") { }
    }
}
