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
    public class DriveTypeService : IDriveTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public DriveTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LocalizedData>> GetLocalizedDriveTypesByVehicleTypeId(string cultureCode, int vehicleTypeId,
            bool includeDeleted = false)
        {
            return await (from driveType in _dbContext.DriveTypes
                    .Where(_ => includeDeleted == false ? _.IsDelete == false : true)
                    .Where(_ => _.VehicleTypeId == vehicleTypeId)
                join lName in _dbContext.Localizations on driveType.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedData
                {
                    Name = lName.Value,
                    Id = driveType.Id
                }).ToListAsync();
        }

        public List<DriveType> GetTypes(bool includeDeleted = false)
        {
            return _dbContext.DriveTypes.Where(_ => includeDeleted == false ? _.IsDelete == false : true).ToList();
        }

        public LocalizedData GetLocalizedById(int id, string cultureCode)
        {
            throw new NotImplementedException();
        }

        public DriveType GetById(int id)
        {
            throw new NotImplementedException();
        }


        public DriveType Update(DriveType DriveType)
        {
            _dbContext.DriveTypes.Update(DriveType);
            _dbContext.SaveChanges();
            return DriveType;
        }

        public DriveType Add(DriveType DriveType)
        {
            _dbContext.DriveTypes.Add(DriveType);
            _dbContext.SaveChanges();
            return DriveType;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.DriveTypes.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.DriveTypes.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool GetByExternalId(int externalId)
        {
            return _dbContext.DriveTypes.Any(_ => _.ExternalId == externalId);
        }
    }
}