using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Catalog;
using DomainModel.Localization;
using Interfaces.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Services.Catalog
{
    public class TransmissionTypeService : ITransmissionTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public TransmissionTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LocalizedData>> GetLocalizedTransmissionTypes(string cultureCode, int vehicleTypeId, bool includeDeleted = false)
        {
            return await(from transmissionType in _dbContext.TransmissionTypes
                    .Where(_ => includeDeleted == false ? _.IsDelete == false : true)
                    .Where(_ => _.VehicleTypeId == vehicleTypeId)
                join lName in _dbContext.Localizations on transmissionType.NameId equals lName.LocalizationSetId
                where lName.CultureCode == cultureCode
                select new LocalizedData
                {
                    Name = lName.Value,
                    Id = transmissionType.Id
                }).ToListAsync();
        }

        public List<TransmissionType> GetTypes(bool includeDeleted = false)
        {
            return _dbContext.TransmissionTypes.Where(_ => includeDeleted == false ? _.IsDelete == false : true).ToList();
        }

        public LocalizedData GetLocalizedById(int id, string cultureCode)
        {
            throw new System.NotImplementedException();
        }

        public TransmissionType GetById(int id)
        {
            throw new System.NotImplementedException();
        }


        public TransmissionType Update(TransmissionType transmissionType)
        {
            _dbContext.TransmissionTypes.Update(transmissionType);
            _dbContext.SaveChanges();
            return transmissionType;
        }

        public TransmissionType Add(TransmissionType transmissionType)
        {
            _dbContext.TransmissionTypes.Add(transmissionType);
            _dbContext.SaveChanges();
            return transmissionType;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.TransmissionTypes.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.TransmissionTypes.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool GetByExternalId(int externalId)
        {
            return _dbContext.TransmissionTypes.Any(_ => _.ExternalId == externalId);
        }
    }
}