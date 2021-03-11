using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Content
{
    public interface IVehicleService
    {
        Task<Vehicle> GetById(int? id);
        Task<Vehicle> GetByExternalId(string id);

        Task<IList<Vehicle>> GetAll();

        Task<Vehicle> Update(Vehicle vehicle);

        Task<Vehicle> Add(Vehicle entity);

        Task<bool> Delete(int id);
    }
}
