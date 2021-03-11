using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;
using Interfaces.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Services.Catalog
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicleModelService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LocalizedVehicleModel>> GetVehicleModelsByVehicleMarkId(int vehicleMarkId,
            bool includeDeleted = false)
        {
            return await _dbContext.VehicleModels.Where(_ => includeDeleted == false ? _.IsDelete == false : true)
                .Where(_ => _.VehicleMarkId == vehicleMarkId)
                .Select(vehicleModel => new LocalizedVehicleModel {Name = vehicleModel.Name, Id = vehicleModel.Id})
                .ToListAsync();
        }

        public List<VehicleModel> GeVehicleModels(bool includeDeleted = false)
        {
            return _dbContext.VehicleModels.Where(_ => includeDeleted == false ? _.IsDelete == false : true).ToList();
        }


        public VehicleModel Update(VehicleModel vehicleModel)
        {
            _dbContext.VehicleModels.Update(vehicleModel);
            _dbContext.SaveChanges();
            return vehicleModel;
        }

        public VehicleModel AddVehicleModel(VehicleModel vehicleModel)
        {
            _dbContext.VehicleModels.Add(vehicleModel);
            _dbContext.SaveChanges();
            return vehicleModel;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.VehicleModels.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.VehicleModels.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool GetByExternalId(int externalId)
        {
            return _dbContext.VehicleModels.Any(_ => _.ExternalId == externalId);
        }
    }
}