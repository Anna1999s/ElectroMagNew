﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Product> Products { get; set; }

        public int? ParentCategoryId { get; set; }
        public virtual ProductCategory ParentCategory { get; set; }

        public virtual List<ProductCategory> ChaildCategories { get; set; }
    }
}
