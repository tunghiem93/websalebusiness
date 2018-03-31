using ProjectWebSaleLand.Shared.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLane.Shared.Model.Employee
{
    public class EmployeeViewModels
    {
        public List<EmployeeModels> ListCate { get; set; }
        public EmployeeViewModels()
        {
            ListCate = new List<EmployeeModels>();
        }
    }
}
