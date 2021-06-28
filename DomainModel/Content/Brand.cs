using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Brand : BaseEntity
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
