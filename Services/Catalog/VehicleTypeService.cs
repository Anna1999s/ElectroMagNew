using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;
using Interfaces.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Services.Catalog
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicleTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LocalizedVehicleType>> GetLocalizedVehicleTypes(string cultureCode,
            bool includeDeleted = false)
        {
            return await (from vehicleType in _dbContext.VehicleTypes.Where(_ =>
                    includeDeleted == false ? _.IsDelete == false : true)
                join lName in _dbContext.Localizations on vehicleType.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedVehicleType
                {
                    Name = lName.Value,
                    Id = vehicleType.Id
                }).ToListAsync();
        }

        public List<VehicleType> GetVehicleTypes(bool includeDeleted = false)
        {
            return _dbContext.VehicleTypes.Where(_ => includeDeleted == false ? _.IsDelete == false : true).ToList();
        }

        public LocalizedVehicleType GetLocalizedById(int id, string cultureCode)
        {
            return (from vehicleType in _dbContext.VehicleTypes.Where(_ => _.Id == id)
                join lName in _dbContext.Localizations on vehicleType.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedVehicleType
                {
                    Name = lName.Value,
                    Id = vehicleType.Id
                }).FirstOrDefault();
        }

        public VehicleType GetById(int id)
        {
            return _dbContext.VehicleTypes.FirstOrDefault(_ => _.Id == id);
        }

        public VehicleType Update(VehicleType vehicleType)
        {
            _dbContext.VehicleTypes.Update(vehicleType);
            _dbContext.SaveChanges();
            return vehicleType;
        }

        public VehicleType AddVehicleType(VehicleType vehicleType)
        {
            _dbContext.VehicleTypes.Add(vehicleType);
            _dbContext.SaveChanges();
            return vehicleType;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.VehicleTypes.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.VehicleTypes.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool GetByExternalId(int externalId)
        {
            return _dbContext.VehicleTypes.Any(_ => _.ExternalId == externalId);
        }
    }
}