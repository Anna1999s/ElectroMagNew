using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;

namespace Interfaces.Catalog
{
    public interface ITransmissionTypeService
    {
        Task<List<LocalizedData>> GetLocalizedTransmissionTypes(string cultureCode, int vehicleTypeId, bool includeDeleted = false);
        List<TransmissionType> GetTypes(bool includeDeleted = false);
        LocalizedData GetLocalizedById(int id, string cultureCode);
        TransmissionType GetById(int id);
        TransmissionType Update(TransmissionType transmissionType);
        TransmissionType Add(TransmissionType transmissionType);
        bool Delete(int id);
        bool GetByExternalId(int externalId);
    }
}