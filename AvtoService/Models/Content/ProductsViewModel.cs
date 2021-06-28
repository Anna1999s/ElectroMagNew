using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models.Content
{
    public class ProductsViewModel
    {
        public ProductsViewModel()
        {
            Items = new List<ProductViewModel>();
            
        }
        public List<ProductViewModel> Items { get; set; }
        public List<BrandViewModel> Brands { get; set; } = new();
    }
}
