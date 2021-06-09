using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models.Content;

namespace WebSite.Models
{
    public class HomeViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new();
        public List<ProductCategoryViewModel> ProductCategory { get; set; } = new();
    }
}
