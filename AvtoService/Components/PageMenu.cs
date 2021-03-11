using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Localization;
using DomainModel.Navigation;
using Interfaces.Navigation;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models.Navigation;

namespace WebSite.Components
{
    public class PageMenu : ViewComponent
    {
        private readonly IMenuItemService _menuItemService;

        public  PageMenu(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        public IViewComponentResult Invoke()
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            var menus = _menuItemService.GetMenus(currentCulture);       
            
            return View(menus);
        }
    }

}
