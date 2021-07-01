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
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Product> GetLastLots(int count)
        {
            var productList = _dbContext.Products.OrderByDescending(_ => _.Created).Take(count).ToList();
            return productList;
        }
        public List<Product> GetAll()
        {
            var productList = _dbContext.Products.Include(_=>_.Photos).OrderByDescending(_ => _.Created).ToList();
            return productList;
        }
        public async Task<Product> Add(Product entity)
        {
            await _dbContext.Products.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public Product GetById(int id)
        {
            var result = _dbContext.Products.Include(_=>_.Photos).FirstOrDefault(_ => _.Id == id);
            return result;
        }
        public List<Product> GetByIdFromCategory(int categoryId)
        {
            var category = _dbContext.ProductCategories.FirstOrDefault(_ => _.Id == categoryId);
            var categoryIds = new List<int> { categoryId };
            GetCategoryIds(ref categoryIds, category);

            var productList = _dbContext.Products.Where(_ => categoryIds.Contains(_.CategoryId.Value)).ToList();
            return productList;
        }

        void GetCategoryIds(ref List<int> categoryIds, ProductCategory category)
        {
            categoryIds.AddRange(category.ChaildCategories.Select(_ => _.Id).ToList());
            foreach (var item in category.ChaildCategories)
                GetCategoryIds(ref categoryIds, item);
        }
    }
}
