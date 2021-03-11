using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DomainModel.Localization;

namespace DomainModel.Catalog
{
    /// <summary>
    /// “ËÔ “—
    /// </summary>
    public class VehicleType : BaseEntity
    {
        public VehicleType()
        {
            Name = new LocalizationSet();
            VehicleMarks = new HashSet<VehicleMark>();
            DriveTypes = new HashSet<DriveType>();
            VehicleOptions = new List<VehicleOption>();
        }

        public int? NameId { get; set; }
        public virtual LocalizationSet Name { get; set; }

        public ICollection<VehicleMark> VehicleMarks { get; set; }
        public ICollection<DriveType> DriveTypes { get; set; }
        public ICollection<VehicleOption> VehicleOptions { get; set; }
        public int ExternalId { get; set; }
    }
}
