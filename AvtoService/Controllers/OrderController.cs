using AutoMapper;
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

namespace WebSite.Controllers
{
    public class OrderController : BaseController
    {

        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IPhotoService _photoService;
        private readonly IBasketService _basketService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBasketItemService _basketItemService;
        private readonly IMemoryCache _memoryCache;

        public OrderController(IMapper mapper, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IProductService productService,
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
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var orders = _context.Orders.Where(_ => _.UserId == userId).ToList();
            return View(orders);
        }

        public ActionResult AddToOrder()
        {
            var userId = _userManager.GetUserId(User);
            var korz = _memoryCache.Get(userId) as List<BasketItem>;
            if (korz != null)
            {
                var order = new Order
                {
                    UserId = userId,
                    OrderProducts = korz.Select(_ => new OrderProduct
                    {
                        ProductId = _.ProductId,
                        PriceUnit = _.Product.Price,
                        PriceTotal = _.Product.Price * _.Count,
                        Count = _.Count,
                    }).ToList(),
                    OrderDate = DateTime.UtcNow,
                };
                order.OrderPrice = order.OrderProducts.Sum(_ => _.PriceTotal);

                _context.Add(order);
                _context.SaveChanges();

                //_memoryCache.Remove(userId);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var userId = _userManager.GetUserId(User);
            var order = _context.Orders.FirstOrDefault(_ => _.Id == id);
            return View(order);
        }

    }
}
