using AutoMapper;
using DomainModel.Content;
using DomainModel.Framework;
using DomainModel.Framework.Models;
using DomainModel.Localization;
using Interfaces.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models;

namespace WebSite.Areas.Admin.Controllers
{
    public class PageMenuController : BaseAdminController
    {
        private readonly ILocalizedPageService _localizedPageService;
        private readonly IOptions<GoogleTranslateConfig> _googleTranslateConfig;
        private readonly IStringLocalizer<PageMenuController> _localizer;

        public PageMenuController(IMapper mapper, ILocalizedPageService localizedPageService, IOptions<GoogleTranslateConfig> googleTranslateConfig, IStringLocalizer<PageMenuController> localizer) : base(mapper)
        {
            _localizedPageService = localizedPageService;
            _googleTranslateConfig = googleTranslateConfig;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            var pages = _mapper.Map<List<PageViewModel>>(_localizedPageService.GetAll(currentCulture));
            return View(pages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new PageViewModel());
        }

        [HttpPost]
        public IActionResult Create(PageViewModel model)
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var page = new Page
            {
                StringKey = model.StringKey,
                Name = new LocalizationSet
                {
                    Localizations = new List<Localization>
                        {
                            Heplers.Localization(translateClient.TranslateToRussian(model.Name), Culture.RuCode),
                            Heplers.Localization(translateClient.TranslateToEnglish(model.Name), Culture.EnCode),
                            Heplers.Localization(translateClient.TranslateToKorean(model.Name), Culture.KoCode)
                        }
                },
                Content = new LocalizationSet
                {
                    Localizations = new List<Localization>
                        {
                            Heplers.Localization(translateClient.TranslateToRussian(model.Content), Culture.RuCode),
                            Heplers.Localization(translateClient.TranslateToEnglish(model.Content), Culture.EnCode),
                            Heplers.Localization(translateClient.TranslateToKorean(model.Content), Culture.KoCode)
                        }
                },
                MetaDescription = new LocalizationSet
                {
                    Localizations = new List<Localization>
                        {
                            Heplers.Localization(translateClient.TranslateToRussian(model.MetaDescription), Culture.RuCode),
                            Heplers.Localization(translateClient.TranslateToEnglish(model.MetaDescription), Culture.EnCode),
                            Heplers.Localization(translateClient.TranslateToKorean(model.MetaDescription), Culture.KoCode)
                        }
                },
                MetaTitle = new LocalizationSet
                {
                    Localizations = new List<Localization>
                        {
                            Heplers.Localization(translateClient.TranslateToRussian(model.MetaTitle), Culture.RuCode),
                            Heplers.Localization(translateClient.TranslateToEnglish(model.MetaTitle), Culture.EnCode),
                            Heplers.Localization(translateClient.TranslateToKorean(model.MetaTitle), Culture.KoCode)
                        }
                },
                MetaKeywords = new LocalizationSet
                {
                    Localizations = new List<Localization>
                        {
                            Heplers.Localization(translateClient.TranslateToRussian(model.MetaKeywords), Culture.RuCode),
                            Heplers.Localization(translateClient.TranslateToEnglish(model.MetaKeywords), Culture.EnCode),
                            Heplers.Localization(translateClient.TranslateToKorean(model.MetaKeywords), Culture.KoCode)
                        }
                }
            };

            _localizedPageService.AddPage(page);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            var page = _localizedPageService.GetById(Id, currentCulture);

            return View(_mapper.Map<PageViewModel>(page));
        }

        [HttpPost]
        public IActionResult Edit(PageViewModel model)
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);
            var page = new Page
            {
                StringKey = model.StringKey,
                Name = new LocalizationSet
                {
                    Localizations = new List<Localization>
                        {
                            Heplers.Localization(translateClient.TranslateToRussian(model.Name), Culture.RuCode),
                            Heplers.Localization(translateClient.TranslateToEnglish(model.Name), Culture.EnCode),
                            Heplers.Localization(translateClient.TranslateToKorean(model.Name), Culture.KoCode)
                        }
                },
                Content = new LocalizationSet
                {
                    Localizations = new List<Localization>
                        {
                            Heplers.Localization(translateClient.TranslateToRussian(model.Content), Culture.RuCode),
                            Heplers.Localization(translateClient.TranslateToEnglish(model.Content), Culture.EnCode),
                            Heplers.Localization(translateClient.TranslateToKorean(model.Content), Culture.KoCode)
                        }
                },
                MetaDescription = new LocalizationSet
                {
                    Localizations = new List<Localization>
                        {
                            Heplers.Localization(translateClient.TranslateToRussian(model.MetaDescription), Culture.RuCode),
                            Heplers.Localization(translateClient.TranslateToEnglish(model.MetaDescription), Culture.EnCode),
                            Heplers.Localization(translateClient.TranslateToKorean(model.MetaDescription), Culture.KoCode)
                        }
                },
                MetaTitle = new LocalizationSet
                {
                    Localizations = new List<Localization>
                        {
                            Heplers.Localization(translateClient.TranslateToRussian(model.MetaTitle), Culture.RuCode),
                            Heplers.Localization(translateClient.TranslateToEnglish(model.MetaTitle), Culture.EnCode),
                            Heplers.Localization(translateClient.TranslateToKorean(model.MetaTitle), Culture.KoCode)
                        }
                },
                MetaKeywords = new LocalizationSet
                {
                    Localizations = new List<Localization>
                        {
                            Heplers.Localization(translateClient.TranslateToRussian(model.MetaKeywords), Culture.RuCode),
                            Heplers.Localization(translateClient.TranslateToEnglish(model.MetaKeywords), Culture.EnCode),
                            Heplers.Localization(translateClient.TranslateToKorean(model.MetaKeywords), Culture.KoCode)
                        }
                },
                Id = model.Id

            };

            _localizedPageService.UpdatePage(page);

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int Id)
        {
            _localizedPageService.Delete(Id);
            return RedirectToAction("Index");
        }
    }


}
