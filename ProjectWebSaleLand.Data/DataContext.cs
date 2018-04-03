using ProjectWebSaleLand.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLand.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name = CompanyConnectionString")
        {

           // Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());
        }

        public DbSet<Customer> dbCustomer
        {
            get;
            set;
        }
        public DbSet<Employee> dbEmployee
        {
            get;
            set;
        }
              
        public DbSet<Location> dbLocation
        {
            get;
            set;
        }
        
        public DbSet<Product> dbProduct
        {
            get;
            set;
        }
        public DbSet<Image> dbImage
        {
            get;
            set;
        }
    }
}
