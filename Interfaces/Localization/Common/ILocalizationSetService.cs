using DomainModel.Localization;

namespace Interfaces.Localization.Common
{
    public interface ILocalizationSetService
    {
        LocalizationSet GetById(int id);
        void Create(LocalizationSet localizationSet);
    }
}