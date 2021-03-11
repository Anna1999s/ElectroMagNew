using DomainModel.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Box : BaseEntity
    {
        public string Name { get; set; }
        public int? FilialId { get; set; } 

        public Filial Filial { get; set; }
    }
}
