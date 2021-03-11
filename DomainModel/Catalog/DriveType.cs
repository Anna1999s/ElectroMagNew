using System.Collections.Generic;
using DomainModel.Content;
using DomainModel.Localization;

namespace DomainModel.Catalog
{
    /// <summary>
    /// Тип привода (зависит от категории ТС)
    /// </summary>
    public class DriveType : BaseEntity
    {
        public DriveType()
        {
            Name = new LocalizationSet();
        }

        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }

        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public int ExternalId { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
}
