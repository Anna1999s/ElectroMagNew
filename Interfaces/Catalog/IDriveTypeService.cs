using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;

namespace Interfaces.Catalog
{
    public interface IDriveTypeService
    {
        Task<List<LocalizedData>> GetLocalizedDriveTypesByVehicleTypeId(string cultureCode, int vehicleTypeId,
            bool includeDeleted = false);
        List<DriveType> GetTypes(bool includeDeleted = false);
        LocalizedData GetLocalizedById(int id, string cultureCode);
        DriveType GetById(int id);
        DriveType Update(DriveType driveType);
        DriveType Add(DriveType driveType);
        bool Delete(int id);
        bool GetByExternalId(int externalId);
    }
}