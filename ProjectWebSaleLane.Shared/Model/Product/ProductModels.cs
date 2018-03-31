﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Shared.Model.Product
{
    public class ProductModels
    {
        public string ID { get; set; }
        public string CategoryID { get; set; }
        public string LocationID { get; set; }
        public string Category { get; set; }
     
        public string Address { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Acreage { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public string Right { get; set; }
        public int BedRoom { get; set; }
        public int LivingRoom { get; set; }
        public int BathRoom { get; set; }
        public int Floor { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public string Phone1 { get; set; }
        public string Address1 { get; set; }
        public string Phone2 { get; set; }
        public string Address2 { get; set; }
        public string ImageURL1 { get; set; }
        public string ImageURL2 { get; set; }
        public string ImageURL3 { get; set; }
        public string ImageURL4 { get; set; }
        public string ImageURL5 { get; set; }
        public string ImageURL6 { get; set; }
        public string ImageURL7 { get; set; }
        public string ImageURL8 { get; set; }
        
        public ProductModels()
        {            
        }
    }
}
