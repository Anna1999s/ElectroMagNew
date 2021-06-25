using DomainModel.Content;
using DomainModel.Localization;

namespace DomainModel.Navigation
{
    public class MenuItem : BaseEntity
    {
        public MenuItem()
        {
            Name = new LocalizationSet();
            Controller = "Page";
            Action = "Index";
        }
        public int Order { get; set; }
        public int? NameId { get; set; }
        public virtual LocalizationSet Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public string CustomUrl { get; set; }
        public bool IsHidden { get; set; }
        public int? PageId { get; set; }
        public virtual Page Page { get; set; }
        
        public int? MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
