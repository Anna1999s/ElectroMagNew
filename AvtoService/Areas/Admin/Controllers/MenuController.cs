using AutoMapper;
using DomainModel.Content;
using DomainModel.Framework;
using DomainModel.Framework.Models;
using DomainModel.Localization;
using DomainModel.Navigation;
using Interfaces.Localization;
using Interfaces.Navigation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models;
using WebSite.Models.Navigation;

namespace WebSite.Areas.Admin.Controllers
{
    public class MenuController : BaseAdminController
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IOptions<GoogleTranslateConfig> _googleTranslateConfig;
        private readonly ILocalizedPageService _localizedPageService;

        public MenuController(IMapper mapper, IMenuItemService menuItemService, IOptions<GoogleTranslateConfig> googleTranslateConfig, ILocalizedPageService localizedPageService) : base(mapper)
        {
            _menuItemService = menuItemService;
            _googleTranslateConfig = googleTranslateConfig;
            _localizedPageService = localizedPageService;
        }
        [HttpGet]
        public IActionResult Menus()
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            var menus = _mapper.Map<List<MenuViewModel>>(_menuItemService.GetMenus(currentCulture));
            return View(menus);
        }        
        [HttpGet]
        public IActionResult CreateMenu()
        {
            
            return View(new MenuViewModel());
        }
        [HttpPost]
        public IActionResult CreateMenu(MenuViewModel model)
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var menu = new Menu 
            {
                Name = new LocalizationSet
                {
                    Localizations = new List<Localization>
                    {
                        Heplers.Localization(translateClient.TranslateToRussian(model.Name), Culture.RuCode),
                        Heplers.Localization(translateClient.TranslateToEnglish(model.Name), Culture.EnCode),
                        Heplers.Localization(translateClient.TranslateToKorean(model.Name), Culture.KoCode)
                    }  
                }              
            };

             _menuItemService.AddMenu(menu);

            return RedirectToAction("Menus");
        }
        
        [HttpGet]
        public IActionResult EditMenu(int Id)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            var menu = _menuItemService.GetMenuById(Id, currentCulture);

            return View(_mapper.Map<MenuViewModel>(menu));
        }

        [HttpPost]
        public IActionResult EditMenu(MenuViewModel model)
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);
            var menu = new Menu
            {
                Name = new LocalizationSet
                {
                    Localizations = new List<Localization>
                    {
                        Heplers.Localization(translateClient.TranslateToRussian(model.Name), Culture.RuCode),
                        Heplers.Localization(translateClient.TranslateToEnglish(model.Name), Culture.EnCode),
                        Heplers.Localization(translateClient.TranslateToKorean(model.Name), Culture.KoCode)
                    }
                },
                Id = model.Id            
                
            };

            _menuItemService.UpdateMenu(menu);

            return RedirectToAction("Menus");
        }


        public IActionResult DeleteMenu(int Id)
        {
            _menuItemService.DeleteMenu(Id);
            return RedirectToAction("Menus");
        }


        [HttpGet]
        public IActionResult MenuItems(int Id)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            var menuItemss = _mapper.Map<MenuViewModel>(_menuItemService.GetMenuById(Id, currentCulture));
            return View(menuItemss);
        }
        [HttpGet]
        public IActionResult CreateMenuItem(int menuId)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            return View(new MenuItemViewModel { MenuId = menuId, Pages = _mapper.Map<List<PageViewModel>>(_localizedPageService.GetAll(currentCulture).ToList()) });
        }
        [HttpPost]
        public IActionResult CreateMenuItem(MenuItemViewModel model)
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);
            var menuItem = new MenuItem
            {
                Name = new LocalizationSet
                {
                    Localizations = new List<Localization>
                    {
                        Heplers.Localization(translateClient.TranslateToRussian(model.Name), Culture.RuCode),
                        Heplers.Localization(translateClient.TranslateToEnglish(model.Name), Culture.EnCode),
                        Heplers.Localization(translateClient.TranslateToKorean(model.Name), Culture.KoCode)
                    }
                },
                Order = model.Order,
                MenuId = model.MenuId,
                CustomUrl = model.CustomUrl,
                Action = model.Action,
                Controller = model.Controller,
                IsHidden = model.IsHidden,
                PageId = model.PageId
            };

            _menuItemService.AddMenuItem(menuItem);

            return RedirectToAction("MenuItems", new {Id = model.MenuId});
        }
        [HttpGet]
        public IActionResult EditMenuItem(int Id)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            var menuItem = _mapper.Map<MenuItemViewModel>(_menuItemService.GetMenuItemById(Id, currentCulture));
            menuItem.Pages = _mapper.Map<List<PageViewModel>>(_localizedPageService.GetAll(currentCulture).ToList());
            return View(menuItem);
        }
        [HttpPost]
        public IActionResult EditMenuItem(MenuItemViewModel model)
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);
            var menuItem = new MenuItem
            {
                Name = new LocalizationSet
                {
                    Localizations = new List<Localization>
                    {
                        Heplers.Localization(translateClient.TranslateToRussian(model.Name), Culture.RuCode),
                        Heplers.Localization(translateClient.TranslateToEnglish(model.Name), Culture.EnCode),
                        Heplers.Localization(translateClient.TranslateToKorean(model.Name), Culture.KoCode)
                    }
                },
                Id = model.Id,
                Order = model.Order,
                MenuId = model.MenuId,
                CustomUrl = model.CustomUrl,
                Action = model.Action,
                Controller = model.Controller,
                IsHidden = model.IsHidden,
                PageId = model.PageId
            };

            _menuItemService.UpdateMenuItem(menuItem);

            return RedirectToAction("MenuItems", new { Id = model.MenuId });
        }
        [HttpGet]
        public IActionResult DetailsMenuItem(int Id)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            var menuItem = _menuItemService.GetMenuItemById(Id, currentCulture);
            return View(_mapper.Map<MenuItemViewModel>(menuItem));
        }
        public IActionResult DeleteMenuItem(int Id)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            var menuItem = _menuItemService.GetMenuItemById(Id, currentCulture);
            _menuItemService.DeleteMenuItem(Id);
            return RedirectToAction("MenuItems", new { Id = menuItem.MenuId });
        }
    }
}
