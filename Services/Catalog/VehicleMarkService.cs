using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;
using Interfaces.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Services.Catalog
{
    public class VehicleMarkService : IVehicleMarkService
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicleMarkService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<LocalizedVehicleMark> GetLocalizedVehicleMarks(string cultureCode, bool includeDeleted = false)
        {
            return (from vehicleMark in _dbContext.VehicleMarks.Where(_ =>
                    includeDeleted == false ? _.IsDelete == false : true)
                join lName in _dbContext.Localizations on vehicleMark.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedVehicleMark
                {
                    Name = lName.Value,
                    Id = vehicleMark.Id
                }).ToList();
        }

        public async Task<List<LocalizedVehicleMark>> GetLocalizedVehicleMarksByVehicleTypeId(string cultureCode,
            int vehicleTypeId, bool includeDeleted = false)
        {
            return await (from vehicleMark in _dbContext.VehicleMarks
                    .Where(_ => includeDeleted == false ? _.IsDelete == false : true)
                    .Where(_ => _.VehicleTypeId == vehicleTypeId)
                join lName in _dbContext.Localizations on vehicleMark.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedVehicleMark
                {
                    Name = lName.Value,
                    Id = vehicleMark.Id
                }).ToListAsync();
        }

        public List<VehicleMark> GeVehicleMarks(bool includeDeleted = false)
        {
            return _dbContext.VehicleMarks.Include(_ => _.VehicleType)
                .Where(_ => includeDeleted == false ? _.IsDelete == false : true).ToList();
        }

        public LocalizedVehicleMark GetLocalizedById(int id, string cultureCode)
        {
            return (from vehicleMark in _dbContext.VehicleMarks.Where(_ => _.Id == id)
                join lName in _dbContext.Localizations on vehicleMark.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedVehicleMark
                {
                    Name = lName.Value,
                    Id = vehicleMark.Id
                }).FirstOrDefault();
        }

        public VehicleMark GetById(int id)
        {
            return _dbContext.VehicleMarks.FirstOrDefault(_ => _.Id == id);
        }

        public VehicleMark Update(VehicleMark VehicleMark)
        {
            _dbContext.VehicleMarks.Update(VehicleMark);
            _dbContext.SaveChanges();
            return VehicleMark;
        }

        public VehicleMark AddVehicleMark(VehicleMark VehicleMark)
        {
            _dbContext.VehicleMarks.Add(VehicleMark);
            _dbContext.SaveChanges();
            return VehicleMark;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.VehicleMarks.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.VehicleMarks.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool GetByExternalId(int externalId)
        {
            return _dbContext.VehicleMarks.Any(_ => _.ExternalId == externalId);
        }
    }
}