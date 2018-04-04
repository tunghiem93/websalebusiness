using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLand.Data.Entities
{
    public class Product
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string LocationID { get; set; }
        public int Type { get; set; }
        public int Segment { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Acreage { get; set; }
        public double Price { get; set; }
        public string Right { get; set; }
        public int BedRoom { get; set; }
        public int LivingRoom { get; set; }
        public int BathRoom { get; set; }
        public int Floor { get; set; }
        public string Description { get; set; }
        public string Phone1 { get; set; }
        public string Address1 { get; set; }
        public string Phone2 { get; set; }
        public string Address2 { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
    }
}
