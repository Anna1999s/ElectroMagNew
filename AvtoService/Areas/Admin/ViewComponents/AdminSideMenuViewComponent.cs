using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebSite.Areas.Admin.Models.Common;

namespace WebSite.Areas.Admin.ViewComponents
{
    public class AdminSideMenuViewComponent : ViewComponent
    {
        private readonly ILocalizedPageService _localizedPageService;
        private readonly IStringLocalizer<AdminSideMenuViewComponent> _localizer;
        public AdminSideMenuViewComponent(ILocalizedPageService localizedPageService, IStringLocalizer<AdminSideMenuViewComponent> localizer)
        {
            _localizer = localizer;
            _localizedPageService = localizedPageService;
        }
        public IViewComponentResult Invoke()
        {
            var items = GetItems();
            return View(items);
        }

        private List<MenuItemViewModel> GetItems()
        {
            var currentController = ViewContext.RouteData.Values["Controller"];
            var currentAction = ViewContext.RouteData.Values["Action"];
         
            var model = new List<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Title = _localizer["Main"],
                    //Title = "Главная",
                    Controller = "Home"
                },
                new MenuItemViewModel
                {
                    Title = _localizer["Menu"],
                    Childs = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel {Title = _localizer["List"], Controller = "Menu", Action = "Menus"},
                    },
                    Controller = "Menu"
                },
                new MenuItemViewModel
                {
                    Title = _localizer["Pages"],
                    Childs = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel {Title = _localizer["List"], Controller = "PageMenu", Action = "Index"}
                    },
                    Controller = "PageMenu"
                }
            };

            foreach (var item in model)
            {
                if (item.Controller != null &&
                    Equals(currentController, StringComparison.InvariantCultureIgnoreCase)
                    ||
                    item.Childs.Any(
                        _ =>
                            _.Controller != null &&
                            Equals(currentController, StringComparison.InvariantCultureIgnoreCase)))
                {
                    item.IsActive = true;
                }

                if (item.Childs.Count <= 0)
                    continue;
                foreach (var child in item.Childs)
                {
                    if (child.Controller != null &&
                        Equals(currentController, StringComparison.InvariantCultureIgnoreCase)
                        && Equals(currentAction, StringComparison.InvariantCultureIgnoreCase))
                    {
                        child.IsActive = true;
                    }
                }
            }

            return model;
        }
    }
}