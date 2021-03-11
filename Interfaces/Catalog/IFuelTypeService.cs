using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;

namespace Interfaces.Catalog
{
    public interface IFuelTypeService
    {
        Task<List<LocalizedData>> GetLocalizedTypes(string cultureCode, bool includeDeleted = false);
        List<FuelType> GetTypes(bool includeDeleted = false);
        LocalizedFuelType GetLocalizedById(int? id, string cultureCode);
        FuelType GetById(int id);
        FuelType Update(FuelType fuelType);
        FuelType Add(FuelType fuelType);
        bool Delete(int id);
        bool GetByExternalId(int externalId);
    }
}