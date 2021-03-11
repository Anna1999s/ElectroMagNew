using DomainModel.Localization;
using System.Collections.Generic;

namespace DomainModel.Navigation
{
    public class Menu : BaseEntity
    {
        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }

        public IList<MenuItem> Items { get; set; }
    }
}
