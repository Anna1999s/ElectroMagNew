using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Warehouse //склад
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Количество")]
        public int Count { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
