using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;
using Interfaces.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Services.Catalog
{
    public class VehicleOptionService : IVehicleOptionService
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicleOptionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LocalizedVehicleOption>> GetLocalizedOptionsByVehicleTypeId(string cultureCode,
            int vehicleTypeId, bool includeDeleted = false)
        {
            return await (from vehicleOption in _dbContext.VehicleOptions
                        .Where(_ => includeDeleted == false ? _.IsDelete == false : true)
                        .Where(_ => _.VehicleTypeId == vehicleTypeId)
                          join lName in _dbContext.Localizations on vehicleOption.NameId equals lName.LocalizationSetId
                          where lName.CultureCode == cultureCode
                          select new LocalizedVehicleOption
                          {
                              Name = lName.Value,
                              Id = vehicleOption.Id
                          }).ToListAsync();
        }

        public List<VehicleOption> GetOptions(bool includeDeleted = false)
        {
            return _dbContext.VehicleOptions.Where(_ => includeDeleted == false ? _.IsDelete == false : true).ToList();
        }

        public async Task<LocalizedVehicleOption> GetLocalizedById(int id, string cultureCode)
        {
            return await (from vehicleOption in _dbContext.VehicleOptions
                    .Where(_ => _.Id == id)
                join lName in _dbContext.Localizations on vehicleOption.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedVehicleOption
                {
                    Name = lName.Value,
                    Id = vehicleOption.Id
                }).FirstOrDefaultAsync();
        }

        public async Task<VehicleOption> GetById(int id)
        {
            return await _dbContext.VehicleOptions.FirstOrDefaultAsync(_ =>_.Id == id);
        }


        public VehicleOption Update(VehicleOption vehicleOption)
        {
            _dbContext.VehicleOptions.Update(vehicleOption);
            _dbContext.SaveChanges();
            return vehicleOption;
        }

        public VehicleOption Add(VehicleOption vehicleOption)
        {
            _dbContext.VehicleOptions.Add(vehicleOption);
            _dbContext.SaveChanges();
            return vehicleOption;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.VehicleOptions.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.VehicleOptions.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool GetByExternalId(int externalId)
        {
            return _dbContext.VehicleOptions.Any(_ => _.ExternalId == externalId);
        }
    }
}