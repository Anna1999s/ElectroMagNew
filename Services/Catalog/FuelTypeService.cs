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
    public class FuelTypeService : IFuelTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public FuelTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LocalizedData>> GetLocalizedTypes(string cultureCode, bool includeDeleted = false)
        {
            return await (from fuelType in _dbContext.FuelTypes
                    .Where(_ => includeDeleted == false ? _.IsDelete == false : true)
                join lName in _dbContext.Localizations on fuelType.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedData
                {
                    Name = lName.Value,
                    Id = fuelType.Id
                }).ToListAsync();
        }

        public List<FuelType> GetTypes(bool includeDeleted = false)
        {
            return _dbContext.FuelTypes.Where(_ => includeDeleted == false ? _.IsDelete == false : true).ToList();
        }

        public LocalizedFuelType GetLocalizedById(int? id, string cultureCode)
        {
            return (from fuelType in _dbContext.FuelTypes.Where(_ => _.Id == id)
                join lName in _dbContext.Localizations on fuelType.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedFuelType
                {
                    Name = lName.Value,
                    Id = fuelType.Id
                }).FirstOrDefault();
        }
      
        public FuelType GetById(int id)
        {
            throw new NotImplementedException();
        }


        public FuelType Update(FuelType fuelType)
        {
            _dbContext.FuelTypes.Update(fuelType);
            _dbContext.SaveChanges();
            return fuelType;
        }

        public FuelType Add(FuelType fuelType)
        {
            _dbContext.FuelTypes.Add(fuelType);
            _dbContext.SaveChanges();
            return fuelType;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.FuelTypes.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.FuelTypes.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool GetByExternalId(int externalId)
        {
            return _dbContext.FuelTypes.Any(_ => _.ExternalId == externalId);
        }
    }
}