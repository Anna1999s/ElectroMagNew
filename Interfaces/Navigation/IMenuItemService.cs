using DomainModel.Localization;
using DomainModel.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Navigation
{
    public interface IMenuItemService
    {
        List<LocalizedMenu> GetMenus(string cultureCode, bool onlyActive = false);
        LocalizedMenu GetMenuById(int Id, string cultureCode, bool onlyActive = false);
        List<LocalizedMenuItem> GetMenuItems(int Id, string cultureCode, bool onlyActive = false);
        LocalizedMenuItem GetMenuItemById(int Id, string cultureCode, bool onlyActive = false);
        MenuItem GetById(int id);
        Menu UpdateMenu(Menu menu);
        MenuItem UpdateMenuItem(MenuItem menuItem);
        MenuItem AddMenuItem(MenuItem menuItem);
        Menu AddMenu(Menu menu);
        bool DeleteMenuItem(int id);
        bool DeleteMenu(int id);
    }
}