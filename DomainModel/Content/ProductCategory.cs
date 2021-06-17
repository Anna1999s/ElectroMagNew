using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        public virtual IList<Product> Products { get; set; }

        [Display(Name = "Категория-родитель")]
        public int? ParentCategoryId { get; set; }
        [Display(Name = "Категория-родитель")]
        public virtual ProductCategory ParentCategory { get; set; }

        public virtual List<ProductCategory> ChaildCategories { get; set; }
    }
}
