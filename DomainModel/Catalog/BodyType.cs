using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DomainModel.Content;
using DomainModel.Localization;

namespace DomainModel.Catalog
{
    /// <summary>
    /// Тип кузова
    /// </summary>
    public class BodyType : BaseEntity
    {
        public BodyType()
        {
            Name = new LocalizationSet();
        }
        
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }
        public int ExternalId { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
}
