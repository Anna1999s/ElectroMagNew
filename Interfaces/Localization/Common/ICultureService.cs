using DomainModel.Localization;
using System.Collections.Generic;

namespace Interfaces.Localization.Common
{
    public interface ICultureService
    {
        IEnumerable<Culture> GetAll();
    }
}