using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Content;
using DomainModel.Localization;

namespace DomainModel.Catalog
{
    /// <summary>
    /// Тип повреждений
    /// </summary>
    public class DamageType : BaseEntity
    {
        public DamageType()
        {
            Name = new LocalizationSet();
        }

        public int? NameId { get; set; }
        public virtual LocalizationSet Name { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
