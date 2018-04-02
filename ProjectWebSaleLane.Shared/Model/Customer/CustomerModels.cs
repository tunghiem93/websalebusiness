﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Shared.Model.Customer
{
    public class CustomerModels
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập tên!")]
        public string Name { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail không hợp lệ")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Làm ơn xác nhận lại mật khẩu!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Làm ơn nhập số điện thoại!")]
        public string Phone { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập địa chỉ!")]
        public string Address { get; set; }
        public bool MaritalStatus { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public string ImageURL { get; set; }
        public List<SelectListItem> ListMarital { get; set; }
        public List<SelectListItem> ListGender { get; set; }
        public CustomerModels()
        {
            IsActive = true;
            BirthDate = new DateTime(1990, 01, 01);
            ListMarital = new List<SelectListItem>()
            {
                new SelectListItem() {  Text = "Độc thân", Value = "False"},
                new SelectListItem() { Text = "Kết hôn", Value = "True"}
            };

            ListGender = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Nam", Value = "False"},
                new SelectListItem() {  Text = "Nữ", Value = "True"},
            };
        }
    }
}
