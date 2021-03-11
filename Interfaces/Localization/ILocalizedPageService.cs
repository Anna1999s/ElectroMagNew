using DomainModel.Content;
using DomainModel.Localization;
using System.Collections.Generic;

namespace Interfaces.Localization
{
    public interface ILocalizedPageService
    {
        IEnumerable<LocalizedPage> GetAll(string cultureCode);
        LocalizedPage GetById(int Id, string cultureCode);
        LocalizedPage GetByStringKey(string StringKey, string cultureCode);
        public Page AddPage(Page page);
        public Page UpdatePage(Page page);
        public bool Delete(int id);
    }
}
