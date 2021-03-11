using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;

namespace Interfaces.Catalog
{
    public interface IVehicleMarkService
    {
        List<LocalizedVehicleMark> GetLocalizedVehicleMarks(string cultureCode, bool includeDeleted = false);

        Task<List<LocalizedVehicleMark>> GetLocalizedVehicleMarksByVehicleTypeId(string cultureCode, int vehicleTypeId,
            bool includeDeleted = false);

        List<VehicleMark> GeVehicleMarks(bool includeDeleted = false);
        LocalizedVehicleMark GetLocalizedById(int id, string cultureCode);
        VehicleMark GetById(int id);
        VehicleMark Update(VehicleMark vehicleMark);
        VehicleMark AddVehicleMark(VehicleMark vehicleMark);
        bool Delete(int id);
        bool GetByExternalId(int externalId);
    }
}