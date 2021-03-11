using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DomainModel.Content;
using DomainModel.Localization;

namespace DomainModel.Catalog
{
    /// <summary>
    /// Тип топлива
    /// </summary>
    public class FuelType : BaseEntity
    {
        public FuelType()
        {
            Name = new LocalizationSet();
        }

        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }
        public int ExternalId { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
