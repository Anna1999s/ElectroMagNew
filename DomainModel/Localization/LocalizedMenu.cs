using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Localization
{
    public class LocalizedMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public IList<LocalizedMenuItem> Items { get; set; }
    }
}
