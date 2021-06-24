using DomainModel.Content;
using Interfaces.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Content
{
    public class BasketItemService : IBasketItemService
    {
        private readonly ApplicationDbContext _dbContext;

        public BasketItemService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BasketItem GetById(int id)
        {

            var result = _dbContext.BasketItems.FirstOrDefault(_ => _.Id == id);
            return result;

        }

        public IList<BasketItem> GetByBasketId(int id)
        {

            var result = _dbContext.BasketItems.Where(_ => _.BasketId == id);
            return result.ToList();

        }

        public BasketItem GetByProductId(int basketId, int productId)
        {

            var result = _dbContext.BasketItems.FirstOrDefault(_ => _.ProductId == productId && _.BasketId == basketId);
            return result;

        }

        public IList<BasketItem> GetAll()
        {

            var result = _dbContext.BasketItems.AsQueryable();
            return result.ToList();

        }

        public BasketItem Update(int id, Action<BasketItem> update)
        {

            var old = _dbContext.BasketItems.FirstOrDefault(_ => _.Id == id);
            if (old == null)
            {
                return null;
            }
            update(old);
            _dbContext.SaveChanges();
            return old;

        }

        public BasketItem Add(BasketItem entity)
        {

            _dbContext.BasketItems.Add(entity);
            _dbContext.SaveChanges();
            return entity;

        }

        public bool Delete(int id)
        {

            var entity = _dbContext.BasketItems.FirstOrDefault(_ => _.Id == id);
            if (entity == null)
            {
                return false;
            }

            _dbContext.BasketItems.Remove(entity);
            _dbContext.SaveChanges();
            return true;

        }
    }
}
