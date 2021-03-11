using DomainModel.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Service : BaseEntity
    {
        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }

        public int? CategoryId { get; set; }
        public ServiceCategory Category { get; set; }
        public int? TypeId { get; set; }
        public ServiceType Type { get; set; }

        List<Filial> Filials { get; set; }
    }
}
