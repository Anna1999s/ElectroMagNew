using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Content;
using DomainModel.Localization;
using Interfaces.Content;
using Microsoft.EntityFrameworkCore;

namespace Services.Content
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicleService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Vehicle> GetById(int? id)
        {
            var result = await _dbContext.Vehicles.Include(_=>_.VehicleOptions).FirstOrDefaultAsync(_ => _.Id == id);
            return result;
        }
        public async Task<Vehicle> GetByExternalId(string id)
        {
            var result = await _dbContext.Vehicles.FirstOrDefaultAsync(_ => _.ExternalId == id);
            return result;
        }

        public async Task<IList<Vehicle>> GetAll()
        {
            var result = await _dbContext.Vehicles.Include(_=>_.Photos).Include(_=>_.VehicleOptions).ToListAsync();
            return result;
        }

        public async Task<Vehicle> Update(Vehicle vehicle)
        {
            _dbContext.Vehicles.Update(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> Add(Vehicle entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.Vehicles.FirstOrDefaultAsync(_ => _.Id == id);
            if (entity == null) return false;

            _dbContext.Vehicles.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}