using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Localization
{
    public class LocalizedMenuItem
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int? MenuId { get; set; }
        public string StringKey { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }        
        public string CustomUrl { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public int? PageId { get; set; }
        public LocalizedPage Page { get; set; }
    }
}
