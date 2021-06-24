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
    public class BasketService : IBasketService
    {
        private readonly ApplicationDbContext _dbContext;

        public BasketService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Basket GetById(int id)
        {

            var result = _dbContext.Baskets.Include(_ => _.Items).FirstOrDefault(_ => _.Id == id);
            return result;

        }

        public Basket GetByKeyGuid(Guid key)
        {


            var result = _dbContext.Baskets.Include(_ => _.Items).FirstOrDefault(_ => _.KeyGuid == key);
            return result;

        }

        public Basket GetByUserId(string userId)
        {

            var result = _dbContext.Baskets.Include(_ => _.Items).FirstOrDefault(_ => _.UserId == userId);
            return result;

        }

        public IList<Basket> GetAllByUserId(string userId)
        {

            var result = _dbContext.Baskets.Include(_ => _.Items).Where(_ => _.UserId == userId).AsQueryable();
            return result.ToList();

        }

        public IList<Basket> GetAll()
        {
            var result = _dbContext.Baskets.AsQueryable();
            return result.ToList();
        }

        public Basket Update(int id, Action<Basket> update)
        {

            var old = _dbContext.Baskets.FirstOrDefault(_ => _.Id == id);
            if (old == null)
            {
                return null;
            }
            update(old);
            _dbContext.SaveChanges();
            return old;
        }

        public Basket Add(Basket entity)
        {

            _dbContext.Baskets.Add(entity);
            _dbContext.SaveChanges();
            return entity;

        }

        public bool Delete(int id)
        {

            var entity = _dbContext.Baskets.FirstOrDefault(_ => _.Id == id);
            if (entity == null)
            {
                return false;
            }

            _dbContext.Baskets.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public string GetKeyOfUserBasket(string userName)
        {

            var basket = _dbContext.Users.Join(_dbContext.Baskets,
                                       u => u.Id,
                                       b => b.UserId,
                                       (u, b) => new
                                       {
                                           UserName = u.UserName,
                                           Key = b.KeyGuid
                                       })
                                       .AsEnumerable()
                                       .FirstOrDefault(t => t.UserName == userName);

            if (basket != null)
            {
                return basket.Key.ToString();
            }

            return null;

        }
    }
}
