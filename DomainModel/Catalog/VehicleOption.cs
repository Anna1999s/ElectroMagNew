using System.Collections.Generic;
using DomainModel.Content;
using DomainModel.Localization;

namespace DomainModel.Catalog
{
    /// <summary>
    /// Îïöèè ÒÑ
    /// </summary>
    public class VehicleOption : BaseEntity
    {
        public VehicleOption()
        {
            Name = new LocalizationSet();
        }

        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }

        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public int ExternalId { get; set; }
        public virtual List<Vehicle> Vehicles { get; set; }
    }
}