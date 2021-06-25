using DomainModel.Localization;
using System.Collections.Generic;

namespace DomainModel.Navigation
{
    public class Menu : BaseEntity
    {
        public int? NameId { get; set; }
        public virtual LocalizationSet Name { get; set; }

        public virtual IList<MenuItem> Items { get; set; }
    }
}
