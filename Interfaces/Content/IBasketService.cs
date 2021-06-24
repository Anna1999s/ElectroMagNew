using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Content
{
    public interface IBasketService
    {
        Basket GetById(int id);

        Basket GetByKeyGuid(Guid key);

        Basket GetByUserId(string userId);

        IList<Basket> GetAllByUserId(string userId);

        IList<Basket> GetAll();

        Basket Update(int id, Action<Basket> update);

        Basket Add(Basket entity);

        bool Delete(int id);

        string GetKeyOfUserBasket(string userName);
    }
}
