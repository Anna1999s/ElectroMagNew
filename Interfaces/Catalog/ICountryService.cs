using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;

namespace Interfaces.Catalog
{
    public interface ICountryService
    {
        Task<List<LocalizedData>> GetLocalizedCountries(string cultureCode, bool includeDeleted = false);
        List<Country> GetCountries(bool includeDeleted = false);
        LocalizedData GetLocalizedById(int id, string cultureCode);
        Country GetById(int id);
        Country Update(Country manufacturerCountry);
        Country Add(Country manufacturerCountry);
        bool Delete(int id);
        bool GetByExternalId(int externalId);
    }
}