using DomainModel.Content;
using Interfaces.Content;
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
        public List<Product> GetLastLots(int count = 5)
        {
            return _dbContext.Products.OrderByDescending(_ => _.Created).Take(count).ToList();
        }
    }
}
