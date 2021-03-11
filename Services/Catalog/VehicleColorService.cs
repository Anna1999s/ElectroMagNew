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
    public class VehicleColorService : IVehicleColorService
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicleColorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LocalizedData>> GetLocalizedColors(string cultureCode, bool includeDeleted = false)
        {
            return await (from vehicleColor in _dbContext.VehicleColors
                    .Where(_ => includeDeleted == false ? _.IsDelete == false : true)
                join lName in _dbContext.Localizations on vehicleColor.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedData
                {
                    Name = lName.Value,
                    Id = vehicleColor.Id
                }).ToListAsync();
        }

        public List<VehicleColor> GetColors(bool includeDeleted = false)
        {
            return _dbContext.VehicleColors.Where(_ => includeDeleted == false ? _.IsDelete == false : true).ToList();
        }

        public LocalizedData GetLocalizedById(int id, string cultureCode)
        {
            throw new NotImplementedException();
        }

        public VehicleColor GetById(int id)
        {
            throw new NotImplementedException();
        }


        public VehicleColor Update(VehicleColor vehicleColor)
        {
            _dbContext.VehicleColors.Update(vehicleColor);
            _dbContext.SaveChanges();
            return vehicleColor;
        }

        public VehicleColor Add(VehicleColor vehicleColor)
        {
            _dbContext.VehicleColors.Add(vehicleColor);
            _dbContext.SaveChanges();
            return vehicleColor;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.VehicleColors.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.VehicleColors.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool GetByExternalId(int externalId)
        {
            return _dbContext.VehicleColors.Any(_ => _.ExternalId == externalId);
        }
    }
}