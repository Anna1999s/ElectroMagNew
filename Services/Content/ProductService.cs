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
            var productList = _dbContext.Products.Include(_=>_.Photos).OrderByDescending(_ => _.Created).Take(count).ToList();
            return productList;
        }
        public async Task<Product> Add(Product entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public Product GetById(int id)
        {
            var result = _dbContext.Products.Where(_ => _.Id == id).Include(_ => _.Photos).Include(_=>_.Category).FirstOrDefault();
            return result;
        }
        
    }
}
