using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Content
{
    public interface IProductService
    {
        List<Product> GetLastLots(int count );
        Task<Product> Add(Product entity);
        Product GetById(int id);
    }
}