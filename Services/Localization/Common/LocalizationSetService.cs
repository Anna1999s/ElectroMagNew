using DomainModel.Localization;
using Interfaces.Localization.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Services.Localization.Common
{
    public class LocalizationSetService : ILocalizationSetService
    {
        private readonly ApplicationDbContext _dbContext;

        public LocalizationSetService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public LocalizationSet GetById(int id)
        {
            return _dbContext.LocalizationSets
              .Include(ls => ls.Localizations)
              .FirstOrDefault(ls => ls.Id == id);
        }

        public void Create(LocalizationSet localizationSet)
        {
            _dbContext.LocalizationSets.Add(localizationSet);
        }
    }
}