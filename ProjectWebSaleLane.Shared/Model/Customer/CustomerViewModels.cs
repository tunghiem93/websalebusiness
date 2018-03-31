using ProjectWebSaleLand.Shared.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLane.Shared.Model.Customer
{
    public class CustomerViewModels
    {
        public List<CustomerModels> ListCate { get; set; }
        public CustomerViewModels()
        {
            ListCate = new List<CustomerModels>();
        }
    }
}
