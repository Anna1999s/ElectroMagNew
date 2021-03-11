using DomainModel.Catalog;
using DomainModel.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Organization :BaseEntity
    {
        public int? NameId { get; set; } 
        public LocalizationSet Name { get; set; }
        public int? DiscriptionId { get; set; }
        public LocalizationSet Discription { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }

        public List<Filial> Filials { get; set; }
    }
}
