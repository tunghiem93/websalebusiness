using ProjectWebSaleLand.Shared.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLane.Shared.Model.Category
{
    public class CategoryViewModels
    {
        public List<CategoryModels> ListCate { get; set; }
        public CategoryViewModels()
        {
            ListCate = new List<CategoryModels>();
        }
    }
}
