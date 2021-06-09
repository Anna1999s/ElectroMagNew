using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models.Content
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Product> Products { get; set; }

        public int? ParentCategoryId { get; set; }
        public virtual ProductCategory ParentCategory { get; set; }

        public virtual List<ProductCategory> ChaildCategories { get; set; }
    }
}
