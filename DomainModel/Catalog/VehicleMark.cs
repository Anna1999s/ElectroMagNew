using System.Collections.Generic;
using DomainModel.Localization;

namespace DomainModel.Catalog
{
    /// <summary>
    /// Марка ТС
    /// </summary>
    public class VehicleMark : BaseEntity
    {
        public VehicleMark()
        {
            VehicleModels = new HashSet<VehicleModel>();
        }

        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }

        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

        public ICollection<VehicleModel> VehicleModels { get; set; }
        public int ExternalId { get; set; }
    }
}
