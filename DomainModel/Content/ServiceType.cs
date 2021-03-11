using DomainModel.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class ServiceType:BaseEntity
    {
        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }
    }
}
