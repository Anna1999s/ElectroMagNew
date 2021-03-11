using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;

namespace Interfaces.Catalog
{
    public interface IVehicleTypeService
    {
        Task<List<LocalizedVehicleType>> GetLocalizedVehicleTypes(string cultureCode, bool includeDeleted = false);
        List<VehicleType> GetVehicleTypes(bool includeDeleted = false);
        LocalizedVehicleType GetLocalizedById(int id, string cultureCode);
        VehicleType GetById(int id);
        VehicleType Update(VehicleType vehicleType);
        VehicleType AddVehicleType(VehicleType vehicleType);
        bool Delete(int id);
        bool GetByExternalId(int externalId);
    }
}