using Interfaces.Localization.Common;
using System.Collections.Generic;

namespace Services.Localization.Common
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ApplicationDbContext _dbContext;

        public LocalizationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(DomainModel.Localization.Localization localization)
        {
            _dbContext.Localizations.Add(localization);
        }

        public void Delete(IEnumerable<DomainModel.Localization.Localization> localizations)
        {
            _dbContext.Localizations.RemoveRange(localizations);
        }
    }
}