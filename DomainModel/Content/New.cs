using DomainModel.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class New : BaseEntity
    {
        public int? NameId { get; set; }
        public string Name { get; set; }

        public int? ContextId { get; set; }
        public string Context { get; set; }

        public int? PhotoId { get; set; }
        public Photo Photo { get; set; }
    }
}
