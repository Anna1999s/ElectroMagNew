using DomainModel.Content;
using Interfaces.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Content
{
    public class PageService : IPageService
    {
        private readonly ApplicationDbContext _dbContext;

        public PageService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Page> GetByKey(string stringKey)
        {
            var result = await _dbContext.Pages.FirstOrDefaultAsync(_ => _.StringKey.Equals(stringKey));
            return result;
        }

        public async Task<Page> GetById(int id)
        {
            var result = await _dbContext.Pages.FirstOrDefaultAsync(_ => _.Id == id);
            return result;
        }

        public async Task<IList<Page>> GetAll(bool onlyPage = false)
        {
            var result = await _dbContext.Pages.ToListAsync();
            if (onlyPage)
            {
                result = result.Where(_ => !_.IsSection).ToList();
            }
            return result;
        }

        public async Task<IList<TResult>> GetAll<TResult>(Expression<Func<Page, TResult>> selector)
        {
            var result = await _dbContext.Pages.Select(selector).ToListAsync();
            return result.ToList();
        }

        public async Task<Page> Update(int id, Action<Page> update)
        {
            var old = await _dbContext.Pages.FirstOrDefaultAsync(_ => _.Id == id);
            if (old == null)
            {
                return null;
            }
            update(old);
            await _dbContext.SaveChangesAsync();
            return old;
        }

        public async Task<Page> Update(string stringKey, Action<Page> update)
        {
            var old = await _dbContext.Pages.FirstOrDefaultAsync(_ => _.StringKey.Equals(stringKey));
            if (old == null)
            {
                return null;
            }
            update(old);
            await _dbContext.SaveChangesAsync();
            return old;
        }

        public async Task<Page> Add(Page entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.Pages.FirstOrDefaultAsync(_ => _.Id == id);
            if (entity == null)
            {
                return false;
            }

            _dbContext.Pages.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
