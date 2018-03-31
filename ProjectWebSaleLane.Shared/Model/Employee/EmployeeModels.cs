﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Shared.Model.Employee
{
    public class EmployeeModels
    {
        public string ID { get; set; }
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
        public string Description { get; set; }
        public bool IsSupperAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public string ImageURL { get; set; }
        public List<SelectListItem> ListMarital { get; set; }
        public List<SelectListItem> ListGender { get; set; }
        public EmployeeModels()
        {
            ListMarital = new List<SelectListItem>()
            {
                new SelectListItem() {  Text = "Độc thân", Value = "True"},
                new SelectListItem() { Text = "Kết hôn", Value = "False"}
            };

            ListGender = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Nam", Value = "False"},
                new SelectListItem() {  Text = "Nữ", Value = "True"},
            };
        }
    }
}
