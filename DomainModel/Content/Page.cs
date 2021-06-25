using DomainModel.Localization;
using DomainModel.Navigation;
using System.Collections.Generic;

namespace DomainModel.Content
{
    public class Page : BaseEntity
    {
        public Page()
        {
            MenuItems = new List<MenuItem>();

            Name = new LocalizationSet();
            Content = new LocalizationSet();

            MetaTitle = new LocalizationSet();
            MetaKeywords = new LocalizationSet();
            MetaDescription = new LocalizationSet();
        }       

        public bool IsHidden { get; set; }

        public string StringKey { get; set; }

        public int? NameId { get; set; }
        public virtual LocalizationSet Name { get; set; }

        public int? ContentId { get; set; }
        public virtual LocalizationSet Content { get; set; }

        public bool IsSection { get; set; }

        public int? MetaTitleId { get; set; }
        public virtual LocalizationSet MetaTitle { get; set; }

        public int? MetaKeywordsId { get; set; }
        public virtual LocalizationSet MetaKeywords { get; set; }

        public int? MetaDescriptionId { get; set; }
        public virtual LocalizationSet MetaDescription { get; set; }

        public virtual IList<MenuItem> MenuItems { get; set; }
    }
}
