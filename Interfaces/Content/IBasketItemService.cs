using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Content
{
    public interface IBasketItemService
    {
        BasketItem GetById(int id);
        IList<BasketItem> GetByBasketId(int id);
        BasketItem GetByProductId(int basketId, int productId);

        IList<BasketItem> GetAll();

        BasketItem Update(int id, Action<BasketItem> update);

        BasketItem Add(BasketItem entity);

        bool Delete(int id);
    }
}
