﻿using AutoMapper;
using DomainModel.Content;
using Interfaces.Content;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models.Content;

namespace WebSite.Controllers
{
    public class CartController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IPhotoService _photoService;
        private readonly IBasketService _basketService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBasketItemService _basketItemService;
        private readonly IMemoryCache _memoryCache;

        public CartController(IMapper mapper, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IProductService productService,
            IPhotoService photoService, UserManager<IdentityUser> userManager, IBasketService basketService, IBasketItemService basketItemService, IMemoryCache memoryCache
            ) : base(mapper)
        {
            _context = context;
            _photoService = photoService;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _basketService = basketService;
            _basketItemService = basketItemService;
            _memoryCache = memoryCache;
        }
        public ActionResult AddToBasket(int ProductId, int count = 1)
        {
            var UserId = _userManager.GetUserId(User);
            var product = _productService.GetById(ProductId);
            var korz = _memoryCache.Get(UserId) as List<BasketItem>;
            if (korz != null)
            {
                var basketItem = korz.FirstOrDefault(_ => _.ProductId == ProductId);
                if (basketItem != null)
                    basketItem.Count += count;
                else
                    korz.Add(new BasketItem { ProductId = ProductId, Count = count, Product = product  });

                _memoryCache.Set(UserId, korz);
            }
            else
            {
                korz = new List<BasketItem> { new BasketItem { ProductId = ProductId, Count = count, Product = product } };
                _memoryCache.Set(UserId, korz);
            }

            return Json(korz);
        }

        public IActionResult DetailsCart(string userId)
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
