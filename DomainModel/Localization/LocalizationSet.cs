using System.Collections.Generic;

namespace DomainModel.Localization
{
    public class LocalizationSet
    {
        public int Id { get; set; }

        public virtual ICollection<Localization> Localizations { get; set; }
    }
}