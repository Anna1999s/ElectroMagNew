using DomainModel.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Emploeer : BaseEntity
    {
        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public int? FilialId { get; set; }
        public Filial Filial { get; set; }
    }
}
