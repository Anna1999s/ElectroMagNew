using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Interfaces.Content
{
    public interface IPageService
    {
        Task<Page> GetByKey(string stringKey);

        Task<Page> GetById(int id);

        Task<IList<Page>> GetAll(bool onlyPage = false);

        Task<IList<TResult>> GetAll<TResult>(Expression<Func<Page, TResult>> selector);

        Task<Page> Update(int id, Action<Page> update);

        Task<Page> Update(string stringKey, Action<Page> update);

        Task<Page> Add(Page entity);

        Task<bool> Delete(int id);
    }
}