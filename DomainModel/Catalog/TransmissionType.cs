using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DomainModel.Content;
using DomainModel.Localization;

namespace DomainModel.Catalog
{
    /// <summary>
    /// “ËÔ  œœ
    /// </summary>
    public class TransmissionType : BaseEntity
    {
        public TransmissionType()
        {
            Name = new LocalizationSet();
        }

        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }
        public int ExternalId { get; set; }
        public int? VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
