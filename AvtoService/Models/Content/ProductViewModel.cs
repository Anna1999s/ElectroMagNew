using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models.Content
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public string Guarantee { get; set; }
        public int Count { get; set; }

        //категория
        public int? CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }


        //склад
        public int? WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
