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
    public class BodyTypeService : IBodyTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public BodyTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LocalizedData>> GetLocalizedBodyTypesByVehicleTypeId(string cultureCode,
            int vehicleTypeId, bool includeDeleted = false)
        {
            return await (from bodyType in _dbContext.BodyTypes
                    .Where(_ => includeDeleted == false ? _.IsDelete == false : true)
                    .Where(_ => _.VehicleTypeId == vehicleTypeId)
                join lName in _dbContext.Localizations on bodyType.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedData
                {
                    Name = lName.Value,
                    Id = bodyType.Id
                }).ToListAsync();
        }

        public List<BodyType> GetTypes(bool includeDeleted = false)
        {
            return _dbContext.BodyTypes.Where(_ => includeDeleted == false ? _.IsDelete == false : true).ToList();
        }

        public LocalizedData GetLocalizedById(int id, string cultureCode)
        {
            throw new NotImplementedException();
        }

        public BodyType GetById(int id)
        {
            throw new NotImplementedException();
        }


        public BodyType Update(BodyType BodyType)
        {
            _dbContext.BodyTypes.Update(BodyType);
            _dbContext.SaveChanges();
            return BodyType;
        }

        public BodyType Add(BodyType BodyType)
        {
            _dbContext.BodyTypes.Add(BodyType);
            _dbContext.SaveChanges();
            return BodyType;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.BodyTypes.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.BodyTypes.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool GetByExternalId(int externalId)
        {
            return _dbContext.BodyTypes.Any(_ => _.ExternalId == externalId);
        }
    }
}