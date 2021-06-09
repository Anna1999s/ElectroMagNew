using DomainModel.Content;
using Interfaces.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Content
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductCategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<ProductCategory> GetAll()
        {
            var productCategories = _dbContext.ProductCategories.Include(_=>_.ChaildCategories).ToList();
            return productCategories;
        }
        public ProductCategory GetById(int id)
        {
            var result = _dbContext.ProductCategories.Where(_ => _.Id == id).FirstOrDefault();
            return result;
        }
    }
}
