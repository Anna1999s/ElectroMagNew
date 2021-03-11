using System.Collections.Generic;

namespace Interfaces.Localization.Common
{
    public interface ILocalizationService
    {
        void Create(DomainModel.Localization.Localization localization);
        void Delete(IEnumerable<DomainModel.Localization.Localization> localizations);
    }
}