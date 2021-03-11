using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;

namespace Interfaces.Catalog
{
    public interface IVehicleOptionService
    {
        Task<List<LocalizedVehicleOption>> GetLocalizedOptionsByVehicleTypeId(string cultureCode, int vehicleTypeId,
            bool includeDeleted = false);

        List<VehicleOption> GetOptions(bool includeDeleted = false);
        Task<LocalizedVehicleOption> GetLocalizedById(int id, string cultureCode);
        Task<VehicleOption> GetById(int id);
        VehicleOption Update(VehicleOption vehicleOption);
        VehicleOption Add(VehicleOption vehicleOption);
        bool Delete(int id);
        bool GetByExternalId(int externalId);
    }
}