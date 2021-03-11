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
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LocalizedData>> GetLocalizedCountries(string cultureCode, bool includeDeleted = false)
        {
            return await (from manufacturerСountry in _dbContext.ManufacturerCountries
                    .Where(_ => includeDeleted == false ? _.IsDelete == false : true)
                join lName in _dbContext.Localizations on manufacturerСountry.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedData
                {
                    Name = lName.Value,
                    Id = manufacturerСountry.Id
                }).ToListAsync();
        }

        public List<Country> GetCountries(bool includeDeleted = false)
        {
            return _dbContext.ManufacturerCountries.Where(_ => includeDeleted == false ? _.IsDelete == false : true)
                .ToList();
        }

        public LocalizedData GetLocalizedById(int id, string cultureCode)
        {
            throw new NotImplementedException();
        }

        public Country GetById(int id)
        {
            throw new NotImplementedException();
        }


        public Country Update(Country manufacturerCountry)
        {
            _dbContext.ManufacturerCountries.Update(manufacturerCountry);
            _dbContext.SaveChanges();
            return manufacturerCountry;
        }

        public Country Add(Country manufacturerCountry)
        {
            _dbContext.ManufacturerCountries.Add(manufacturerCountry);
            _dbContext.SaveChanges();
            return manufacturerCountry;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.ManufacturerCountries.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.ManufacturerCountries.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool GetByExternalId(int externalId)
        {
            return _dbContext.ManufacturerCountries.Any(_ => _.ExternalId == externalId);
        }
    }
}