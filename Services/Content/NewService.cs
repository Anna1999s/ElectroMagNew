using DomainModel.Content;
using DomainModel.Localization;
using Interfaces.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Content
{
    public class NewService : INewService
    {
        private readonly ApplicationDbContext _dbContext;

        public NewService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<New> Add(New entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.News.FirstOrDefaultAsync(_ => _.Id == id);
            if (entity == null) return false;

            _dbContext.News.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IList<New>> GetAll()
        {
            var result = await _dbContext.News.Include(_ => _.Photo).ToListAsync();
            return result;
        }

        public async Task<New> GetById(int id)
        {
            var result = await _dbContext.News.Include(_ => _.Photo).FirstOrDefaultAsync(_ => _.Id == id);
            return result;
        }


        public async Task<New> Update(New news)
        {
            _dbContext.News.Update(news);
            await _dbContext.SaveChangesAsync();
            return news;
        }
    }
}
