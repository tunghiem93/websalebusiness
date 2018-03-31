using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLand.Shared.Models
{
    public class UserModels
    {
        public string StoreId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }

        public List<string> ListStoreID { get; set; }

        public string StoreName { get; set; }

        public List<string> ListOrganizationId { get; set; }

        public List<string> ListIndustry { get; set; }
        public string ImageURL { get; set; }
        public bool IsSuperAdmin { get; set; }
        public List<OrganizationDTO> ListOrganizations { get; set; }
        public string OrganizationName { get; set; }

        public string CurrencySymbol { get; set; }
        public UserModels()
        {
            ListStoreID = new List<string>();
            ListOrganizations = new List<OrganizationDTO>();
        }
    }

    public class OrganizationDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
