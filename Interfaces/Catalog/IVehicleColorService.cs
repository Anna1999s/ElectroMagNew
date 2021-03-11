using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;

namespace Interfaces.Catalog
{
    public interface IVehicleColorService
    {
        Task<List<LocalizedData>> GetLocalizedColors(string cultureCode, bool includeDeleted = false);
        List<VehicleColor> GetColors(bool includeDeleted = false);
        LocalizedData GetLocalizedById(int id, string cultureCode);
        VehicleColor GetById(int id);
        VehicleColor Update(VehicleColor vehicleColor);
        VehicleColor Add(VehicleColor vehicleColor);
        bool Delete(int id);
        bool GetByExternalId(int externalId);
    }
}