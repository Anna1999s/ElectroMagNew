using DomainModel.Content;
using DomainModel.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Content
{
    public interface INewService
    {
        Task<New> GetById(int id);
        Task<IList<New>> GetAll();
        Task<New> Add(New entity);
        Task<bool> Delete(int id);
        Task<New> Update(New news);

    }
}
