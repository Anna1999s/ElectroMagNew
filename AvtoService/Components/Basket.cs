using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Content;
using DomainModel.Localization;
using DomainModel.Navigation;
using Interfaces.Navigation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebSite.Models.Navigation;

namespace WebSite.Components
{
    public class Basket : ViewComponent
    {
        private readonly IMemoryCache _memoryCache;

        public Basket(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke(string userId)
        {
            var korz = new List<BasketItem>();
            if (!string.IsNullOrEmpty(userId))
            {
                korz = _memoryCache.Get(userId) as List<BasketItem>;
            }

            return View(korz);
        }
    }

}
