using DomainModel.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Filial : BaseEntity
    {
        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }

        public int? OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public string City { get; set; }
        public string Adress { get; set; }
        public string Mail { get; set; }
        public string Site { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public List<PaymentType> PaymentTypes { get; set; }

        public List<Service> Services { get; set; }

        public List<FilialOption> Options { get; set; }

        public List<Emploeer> Emploeers { get; set; }
        public List<Box> Boxes { get; set; }

    }
}
