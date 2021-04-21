using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Warehouse //склад
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
