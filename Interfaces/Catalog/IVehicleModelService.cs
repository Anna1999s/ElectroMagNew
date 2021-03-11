using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;

namespace Interfaces.Catalog
{
    public interface IVehicleModelService
    {
        List<VehicleModel> GeVehicleModels(bool includeDeleted = false);

        Task<List<LocalizedVehicleModel>> GetVehicleModelsByVehicleMarkId(int vehicleMarkId,
            bool includeDeleted = false);

        VehicleModel Update(VehicleModel vehicleModel);
        VehicleModel AddVehicleModel(VehicleModel vehicleModel);
        bool Delete(int id);
        bool GetByExternalId(int externalId);
    }
}