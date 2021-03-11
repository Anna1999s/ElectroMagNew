using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;

namespace Interfaces.Catalog
{
    public interface IBodyTypeService
    {
        Task<List<LocalizedData>> GetLocalizedBodyTypesByVehicleTypeId(string cultureCode, int vehicleTypeId,
            bool includeDeleted = false);

        List<BodyType> GetTypes(bool includeDeleted = false);
        LocalizedData GetLocalizedById(int id, string cultureCode);
        BodyType GetById(int id);
        BodyType Update(BodyType bodyType);
        BodyType Add(BodyType bodyType);
        bool Delete(int id);
        bool GetByExternalId(int externalId);
    }
}