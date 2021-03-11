using DomainModel.Localization;
using Interfaces.Localization.Common;
using System.Collections.Generic;
using System.Linq;

namespace Services.Localization.Common
{
    public class CultureService : ICultureService
    {
        private readonly ApplicationDbContext _dbContext;

        public CultureService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Culture> GetAll()
        {
            return _dbContext.Cultures.OrderBy(c => c.Name).ToList();
        }
    }
}