using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLand.Data.Entities
{
    public class Employee
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public bool MaritalStatus { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }
        public string ZipCode { get; set; }
        public string WebSite { get; set; }
        public string Description { get; set; }
        public bool IsSupperAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public string ImageURL { get; set; }
    }
}
