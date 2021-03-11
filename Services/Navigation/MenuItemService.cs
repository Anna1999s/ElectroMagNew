using DomainModel.Navigation;
using Interfaces.Navigation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Localization;

namespace Services.Navigation
{
    public class MenuItemService : IMenuItemService
    {
        private readonly ApplicationDbContext _dbContext;

        public MenuItemService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<LocalizedMenu> GetMenus(string cultureCode, bool onlyActive = false)
        {
            return (from menu in _dbContext.Menus
                    join lName in this._dbContext.Localizations on menu.NameId equals lName.LocalizationSetId                           
                    where lName.CultureCode == cultureCode 
                    select new LocalizedMenu
                    {
                        Id = menu.Id,
                        Name = lName.Value,
                        Items = (from menuItem in _dbContext.MenuItems.Where(_=>_.MenuId == menu.Id)
                                     join lName in this._dbContext.Localizations on menuItem.NameId equals lName.LocalizationSetId
                                     where lName.CultureCode == cultureCode
                                     select new LocalizedMenuItem
                                     {
                                         Id = menuItem.Id,
                                         Name = lName.Value,
                                         Action = menuItem.Action,
                                         Controller = menuItem.Controller,
                                         CustomUrl = menuItem.CustomUrl,
                                         IsHidden = menuItem.IsHidden,
                                         StringKey = menuItem.Page.StringKey,
                                         MenuId = menuItem.MenuId,
                                         Order = menuItem.Order
                                     }).ToList()
                    }).ToList();
        }
        public LocalizedMenu GetMenuById( int Id, string cultureCode, bool onlyActive = false)
        {
            return (from menu in _dbContext.Menus.Where(_=>_.Id == Id)
                    join lName in this._dbContext.Localizations on menu.NameId equals lName.LocalizationSetId
                    where lName.CultureCode == cultureCode
                    select new LocalizedMenu
                    {
                        Id = menu.Id,
                        Name = lName.Value,                      

                        Items = (from menuItem in _dbContext.MenuItems.Where(_ => _.MenuId == menu.Id)
                                 join lName in this._dbContext.Localizations on menuItem.NameId equals lName.LocalizationSetId
                                 where lName.CultureCode == cultureCode
                                 select new LocalizedMenuItem
                                 {
                                     Id = menuItem.Id,
                                     Name = lName.Value,
                                     Action = menuItem.Action,
                                     Controller = menuItem.Controller,
                                     CustomUrl = menuItem.CustomUrl,
                                     IsHidden = menuItem.IsHidden,
                                     StringKey = menuItem.Page.StringKey,
                                     MenuId = menuItem.MenuId,
                                     Order = menuItem.Order
                                 }).ToList()
                    }).FirstOrDefault();
        }
        public List<LocalizedMenuItem> GetMenuItems(int Id, string cultureCode, bool onlyActive = false)
        {
            return (from menuItem in _dbContext.MenuItems.Where(_ => _.MenuId == Id)
                    join lName in this._dbContext.Localizations on menuItem.NameId equals lName.LocalizationSetId
                    where lName.CultureCode == cultureCode
                    select new LocalizedMenuItem
                    {
                        Id = menuItem.Id,
                        Name = lName.Value,
                        Action = menuItem.Action,
                        Controller = menuItem.Controller,
                        CustomUrl = menuItem.CustomUrl,
                        IsHidden = menuItem.IsHidden,
                        StringKey = menuItem.Page.StringKey,
                        MenuId = menuItem.MenuId,
                        Order = menuItem.Order

                    }).ToList();
        }
        public LocalizedMenuItem GetMenuItemById(int Id, string cultureCode, bool onlyActive = false)
        {
            return (from menuItem in _dbContext.MenuItems.Where(_ => _.Id == Id)
                    join lName in this._dbContext.Localizations on menuItem.NameId equals lName.LocalizationSetId
                    where lName.CultureCode == cultureCode
                    select new LocalizedMenuItem
                    {
                        Id = menuItem.Id,
                        Name = lName.Value,
                        Action = menuItem.Action,
                        Controller = menuItem.Controller,
                        CustomUrl = menuItem.CustomUrl,
                        IsHidden = menuItem.IsHidden,
                        StringKey = menuItem.Page.StringKey,
                        MenuId = menuItem.MenuId,
                        PageId = menuItem.PageId,
                        Order = menuItem.Order,
                        
                        Page = (from page in _dbContext.Pages.Where(_ => _.Id == menuItem.PageId)
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
                                    MetaTitle = lMetaTitle.Value


                                }).FirstOrDefault()
                    }).FirstOrDefault();       
        }

        public MenuItem GetById(int id)
        {
            var result = _dbContext.MenuItems.FirstOrDefault(_ => _.Id == id);
            return result;
        }
        public Menu UpdateMenu(Menu menu)
        {
            _dbContext.Menus.Update(menu);
            _dbContext.SaveChanges();
            return menu;
        }
        public MenuItem UpdateMenuItem(MenuItem menuItem)
        {
            _dbContext.MenuItems.Update(menuItem);
            _dbContext.SaveChanges();
            return menuItem;
        }

        public MenuItem AddMenuItem(MenuItem menuItem)
        {
            _dbContext.MenuItems.Add(menuItem);
            _dbContext.SaveChanges();
            return menuItem;
        }
        public Menu AddMenu(Menu menu)
        {
            _dbContext.Menus.Add(menu);
            _dbContext.SaveChanges();
            return menu;
        }

        public bool DeleteMenuItem(int id)
        {
            var entity = _dbContext.MenuItems.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.MenuItems.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteMenu(int id)
        {
            var entity = _dbContext.Menus.FirstOrDefault(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.Menus.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }
    }
}