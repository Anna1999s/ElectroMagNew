using System.Collections.Generic;
using DomainModel.Navigation;

namespace DomainModel.Localization
{
    public class LocalizedPage : BaseEntity
    {
        public bool IsHidden { get; set; }

        public string StringKey { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public bool IsSection { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public IList<MenuItem> MenuItems { get; set; }
    }
}