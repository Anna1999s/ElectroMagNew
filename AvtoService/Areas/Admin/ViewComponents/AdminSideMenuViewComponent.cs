using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel.Content;
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
                    Title = _localizer["Заказы"],
                    Childs = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel {Title = _localizer["List"], Controller = "Order", Action = "Index"}
                    },
                    Controller = "Order"
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
                },
                new MenuItemViewModel
                {
                    Title = _localizer["Продукты"],
                    Childs = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel {Title = _localizer["Add"], Controller = "Products", Action = "Create"},
                        new MenuItemViewModel {Title = _localizer["List"], Controller = "Products", Action = "Index"}
                    },
                    Controller = "Products"
                },
                new MenuItemViewModel
                {
                    Title = _localizer["Категории"],
                    Childs = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel {Title = _localizer["Add"], Controller = "ProductCategories", Action = "Create"},
                        new MenuItemViewModel {Title = _localizer["List"], Controller = "ProductCategories", Action = "Index"}
                    },
                    Controller = "ProductCategories"
                },
                 new MenuItemViewModel
                {
                    Title = _localizer["Бренды"],
                    Childs = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel {Title = _localizer["Add"], Controller = "Brands", Action = "Create"},
                        new MenuItemViewModel {Title = _localizer["List"], Controller = "Brands", Action = "Index"}
                    },
                    Controller = "Brands"
                },
                new MenuItemViewModel
                {
                    Title = _localizer["Склады"],
                    Childs = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel {Title = _localizer["Add"], Controller = "Warehouses", Action = "Create"},
                        new MenuItemViewModel {Title = _localizer["List"], Controller = "Warehouses", Action = "Index"}
                    },
                    Controller = "Warehouses"
                },
                 new MenuItemViewModel
                 {
                    Title = _localizer["Новости"],
                    Childs = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel {Title = _localizer["List"], Controller = "New", Action = "News"}
                    },
                    Controller = "New"
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