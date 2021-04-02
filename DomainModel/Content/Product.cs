using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; } 
        public string Guarantee { get; set; }

        //категория
        public int? CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }

        //склад
        public int? WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
