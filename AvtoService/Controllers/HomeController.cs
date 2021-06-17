using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using Interfaces.Localization;
using WebSite.Models;
using System.Globalization;
using Interfaces.Content;
using AutoMapper;
using System.Collections.Generic;
using WebSite.Models.Content;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ILocalizedPageService _localizedPageRepository;
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly INewService _newService;

        public HomeController(IMapper mapper,ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer, INewService newService, ILocalizedPageService localizedPageRepository, IProductService productService, IProductCategoryService productCategoryService) : base(mapper)
        {
            _logger = logger;
            _localizer = localizer;
            _localizedPageRepository = localizedPageRepository;
            _productService = productService;
            _productCategoryService = productCategoryService;
            _newService = newService;
        }

        public async Task<IActionResult> Index()
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;

            var entity = _productService.GetLastLots(3);
            var products = _mapper.Map<List<ProductViewModel>>(entity);


            var entityCategories = _productCategoryService.GetAll();
            var categories = _mapper.Map<List<ProductCategoryViewModel>>(entityCategories);

            var entityNews = await _newService.GetAll();
            var news = _mapper.Map<List<NewViewModel>>(entityNews).OrderByDescending(_ => _.Created).Take(3).ToList();
            
            return View(new HomeViewModel { Products = products , ProductCategory = categories, News = news});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

    }
}
