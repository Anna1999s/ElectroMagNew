using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using AspNetCoreContentLocalization.ViewModels;
using DomainModel.Localization;
using Interfaces.Localization;
using Interfaces.Localization.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Services;
using WebSite.Models;

namespace WebSite.Controllers
{
    public abstract class LocalizableController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            base.OnActionExecuting(actionExecutingContext);
            this.HandleViewModelMultilingualProperties(actionExecutingContext);
        }

        protected IEnumerable<Models.Shared.Localization> CreateEmptyLocalizations()
        {
            ICultureService cultureRepository = this.HttpContext.RequestServices.GetService<ICultureService>();

            return cultureRepository.GetAll().Select(c => new Models.Shared.Localization() { CultureCode = c.Code });
        }

        protected IEnumerable<Models.Shared.Localization> CreateLocalizationsFor(LocalizationSet localizationSet)
        {
            ICultureService cultureRepository = this.HttpContext.RequestServices.GetService<ICultureService>();

            return cultureRepository.GetAll()
              .Select(c => new Models.Shared.Localization() { CultureCode = c.Code, Value = localizationSet.Localizations.FirstOrDefault(l => l.CultureCode == c.Code)?.Value });
        }

        protected void CreateOrUpdateLocalizationsFor(object entity)
        {
            foreach (PropertyInfo propertyInfo in this.GetLocalizationSetPropertiesFromEntity(entity))
            {
                LocalizationSet localizationSet = this.GetOrCreateLocalizationSetForProperty(entity, propertyInfo);

                this.DeleteLocalizations(localizationSet);
                this.CreateLocalizations(propertyInfo, localizationSet);
            }
        }

        private void HandleViewModelMultilingualProperties(ActionExecutingContext actionExecutingContext)
        {
            ViewModelBase viewModel = this.GetViewModelFromActionExecutingContext(actionExecutingContext);

            if (viewModel == null)
                return;

            ICultureService cultureRepository = this.HttpContext.RequestServices.GetService<ICultureService>();

            try
            {
                foreach (PropertyInfo propertyInfo in this.GetLocalizablePropertiesFromViewModel(viewModel))
                {
                    this.ModelState.Remove(propertyInfo.Name);

                    bool hasRequiredAttribute = propertyInfo.CustomAttributes.Any(ca => ca.AttributeType == typeof(RequiredAttribute));

                    foreach (Culture culture in cultureRepository.GetAll())
                    {
                        string identity = propertyInfo.Name + culture.Code;
                        string value = this.Request.Form[identity];

                        this.ModelState.SetModelValue(identity, value, value);

                        if (hasRequiredAttribute && string.IsNullOrEmpty(value))
                        {
                            this.ModelState[identity].ValidationState = ModelValidationState.Invalid;
                            this.ModelState[identity].Errors.Add(string.Empty);
                        }

                        else this.ModelState[identity].ValidationState = ModelValidationState.Valid;
                    }
                }
            }

            catch { }
        }

        private ViewModelBase GetViewModelFromActionExecutingContext(ActionExecutingContext actionExecutingContext)
        {
            foreach (KeyValuePair<string, object> actionArgument in actionExecutingContext.ActionArguments)
                if (actionArgument.Value is ViewModelBase)
                    return actionArgument.Value as ViewModelBase;

            return null;
        }

        private IEnumerable<PropertyInfo> GetLocalizablePropertiesFromViewModel(ViewModelBase viewModel)
        {
            return viewModel.GetType().GetProperties().Where(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(LocalizableAttribute)));
        }

        private IEnumerable<PropertyInfo> GetLocalizationSetPropertiesFromEntity(object entity)
        {
            return entity.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(LocalizationSet));
        }

        private LocalizationSet GetOrCreateLocalizationSetForProperty(object entity, PropertyInfo propertyInfo)
        {
            ILocalizationSetService localizationSetRepository = this.HttpContext.RequestServices.GetService<ILocalizationSetService>();
            var _dbContext = this.HttpContext.RequestServices.GetService<ApplicationDbContext>();
            PropertyInfo localizationSetIdPropertyInfo = entity.GetType().GetProperty(propertyInfo.Name + "Id");
            int? localizationSetId = (int?)localizationSetIdPropertyInfo.GetValue(entity);
            LocalizationSet localizationSet = null;

            if (localizationSetId == null || localizationSetId == 0)
            {
                localizationSet = new LocalizationSet();
                localizationSetRepository.Create(localizationSet);
                _dbContext.SaveChanges();
                localizationSetIdPropertyInfo.SetValue(entity, localizationSet.Id);
            }

            else localizationSet = localizationSetRepository.GetById((int)localizationSetId);

            return localizationSet;
        }

        private void DeleteLocalizations(LocalizationSet localizationSet)
        {
            if (localizationSet.Localizations == null || localizationSet.Localizations.Count == 0)
                return;

            ILocalizationService localizationRepository = this.HttpContext.RequestServices.GetService<ILocalizationService>();
            var _dbContext = this.HttpContext.RequestServices.GetService<ApplicationDbContext>();

            localizationRepository.Delete(localizationSet.Localizations);
            _dbContext.SaveChanges();
        }

        private void CreateLocalizations(PropertyInfo propertyInfo, LocalizationSet localizationSet)
        {
            ICultureService cultureRepository = this.HttpContext.RequestServices.GetService<ICultureService>();
            ILocalizationService localizationRepository = this.HttpContext.RequestServices.GetService<ILocalizationService>();
            var _dbContext = this.HttpContext.RequestServices.GetService<ApplicationDbContext>();

            foreach (Culture culture in cultureRepository.GetAll())
            {
                Localization localization = new Localization();

                localization.LocalizationSetId = localizationSet.Id;
                localization.CultureCode = culture.Code;
                localization.Value = this.Request.Form[propertyInfo.Name + culture.Code];
                localizationRepository.Create(localization);
            }

            _dbContext.SaveChanges();
        }
    }
}