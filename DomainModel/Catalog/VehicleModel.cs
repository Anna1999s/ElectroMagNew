using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DomainModel.Content;

namespace DomainModel.Catalog
{
    /// <summary>
    /// Модель ТС
    /// </summary>
    public class VehicleModel : BaseEntity
    {
        public string Name { get; set; }
        public int VehicleMarkId { get; set; }
        public VehicleMark VehicleMark { get; set; }
        public int ExternalId { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
