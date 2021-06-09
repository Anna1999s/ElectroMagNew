using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Content
{
    public interface IProductCategoryService
    {
        List<ProductCategory> GetAll();

        ProductCategory GetById(int id);
    }
}
