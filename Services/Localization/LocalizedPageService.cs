using System.Collections.Generic;
using System.Linq;
using DomainModel.Content;
using DomainModel.Localization;
using Interfaces.Localization;

namespace Services.Localization
{
    public class LocalizedPageRepository : ILocalizedPageService
    {
        private readonly ApplicationDbContext _dbContext;

        public LocalizedPageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //TODO: добавить meta поля
        public IEnumerable<LocalizedPage> GetAll(string cultureCode)
        {
            return (from b in _dbContext.Pages
                    join lName in this._dbContext.Localizations on b.NameId equals lName.LocalizationSetId
                    join lContent in this._dbContext.Localizations on b.ContentId equals lContent.LocalizationSetId
                    where lName.CultureCode == cultureCode && lContent.CultureCode == cultureCode
                    select new LocalizedPage
                    {
                        Id = b.Id,
                        Name = lName.Value,
                        Content = lContent.Value,
                        IsHidden = b.IsHidden,
                        IsSection = b.IsSection,
                        StringKey = b.StringKey,
                        Created = b.Created,
                        Updated = b.Updated
                    }).ToList();
        }
        public LocalizedPage GetById(int Id, string cultureCode)
        {
            return (from page in _dbContext.Pages.Where(_ => _.Id == Id)
                    join lName in this._dbContext.Localizations on page.NameId equals lName.LocalizationSetId
                    where lName.CultureCode == cultureCode
                    join lContext in this._dbContext.Localizations on page.ContentId equals lContext.LocalizationSetId
                    where lContext.CultureCode == cultureCode
                    join lMetaTitle in this._dbContext.Localizations on page.MetaTitleId equals lMetaTitle.LocalizationSetId
                    where lMetaTitle.CultureCode == cultureCode
                    join lMetaDescription in this._dbContext.Localizations on page.MetaDescriptionId equals lMetaDescription.LocalizationSetId
                    where lMetaDescription.CultureCode == cultureCode
                    join lMetaKeywords in this._dbContext.Localizations on page.MetaKeywordsId equals lMetaKeywords.LocalizationSetId
                    where lMetaKeywords.CultureCode == cultureCode
                    select new LocalizedPage
                    {
                        Id = page.Id,
                        Name = lName.Value,
                        Content = lContext.Value,
                        MetaDescription = lMetaDescription.Value,
                        MetaKeywords = lMetaKeywords.Value,
                        MetaTitle = lMetaTitle.Value,
                        Created = page.Created,
                        IsHidden = page.IsHidden,
                        StringKey = page.StringKey,
                        Updated = page.Updated
                    }).FirstOrDefault();
        }
        public LocalizedPage GetByStringKey(string StringKey, string cultureCode)
        {
            return (from page in _dbContext.Pages.Where(_ => _.StringKey == StringKey)
                    join lName in this._dbContext.Localizations on page.NameId equals lName.LocalizationSetId
                    where lName.CultureCode == cultureCode
                    join lContext in this._dbContext.Localizations on page.ContentId equals lContext.LocalizationSetId
                    where lContext.CultureCode == cultureCode
                    join lMetaTitle in this._dbContext.Localizations on page.MetaTitleId equals lMetaTitle.LocalizationSetId
                    where lMetaTitle.CultureCode == cultureCode
                    join lMetaDescription in this._dbContext.Localizations on page.MetaDescriptionId equals lMetaDescription.LocalizationSetId
                    where lMetaDescription.CultureCode == cultureCode
                    join lMetaKeywords in this._dbContext.Localizations on page.MetaKeywordsId equals lMetaKeywords.LocalizationSetId
                    where lMetaKeywords.CultureCode == cultureCode
                    select new LocalizedPage
                    {
                        Id = page.Id,
                        Name = lName.Value,
                        Content = lContext.Value,
                        MetaDescription = lMetaDescription.Value,
                        MetaKeywords = lMetaKeywords.Value,
                        MetaTitle = lMetaTitle.Value,
                        Created = page.Created,
                        IsHidden = page.IsHidden,
                        StringKey = page.StringKey,
                        Updated = page.Updated
                    }).FirstOrDefault();
        }

        public Page AddPage(Page page)
        {
            _dbContext.Pages.Add(page);
            _dbContext.SaveChanges();
            return page;
        }

        public Page UpdatePage(Page page)
        {
            _dbContext.Pages.Update(page);
            _dbContext.SaveChanges();
            return page;
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.Pages.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.Pages.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }
    }
}