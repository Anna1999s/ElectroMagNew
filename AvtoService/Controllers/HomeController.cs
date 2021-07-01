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
using DomainModel.Content;
using Services;

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
        private readonly ApplicationDbContext _context;
        public HomeController(IMapper mapper, ILogger<HomeController> logger, ApplicationDbContext context, IStringLocalizer<HomeController> localizer, INewService newService, ILocalizedPageService localizedPageRepository, IProductService productService, IProductCategoryService productCategoryService) : base(mapper)
        {
            _logger = logger;
            _localizer = localizer;
            _localizedPageRepository = localizedPageRepository;
            _productService = productService;
            _productCategoryService = productCategoryService;
            _newService = newService;
            _context = context;
        }
        public ActionResult AutocompleteSearch(string term)
        {
            var a_suppliers = _context.Products.Where(_ => _.Name.Contains(term)).Take(10).ToList().Select(
                a => new
                {
                    value = $"{a.Name}",
                    id = a.Id
                }
            ).Distinct();

            return Json(a_suppliers);
        }
        public async Task<IActionResult> Index()
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;

            var entity = _productService.GetAll();
            var products = _mapper.Map<List<ProductViewModel>>(entity);
            var productsTelek = new List<ProductViewModel>();
            var productsKomp = new List<ProductViewModel>();
            var productsBit = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var productParentCategory = GetParentCategory(product.Category);
                if (productParentCategory.Name == "Компьютерная техника")
                {
                    productsKomp.Add(product);
                }
                else if (productParentCategory.Name == "Телевизоры и электроника")
                {
                    productsTelek.Add(product);
                }
                else if (productParentCategory.Name == "Бытовая техника")
                {
                    productsBit.Add(product);
                }
            }


            var entityCategories = _productCategoryService.GetAll();
            var categories = _mapper.Map<List<ProductCategoryViewModel>>(entityCategories);

            var entityNews = await _newService.GetAll();
            var news = _mapper.Map<List<NewViewModel>>(entityNews).OrderByDescending(_ => _.Created).Take(3).ToList();

            return View(new HomeViewModel { Products = products, ProductCategory = categories, News = news, ProductsTelek = productsTelek, ProductsBit = productsBit.Take(3).ToList(), ProductsKomp = productsKomp });
        }

        public async Task<IActionResult> IndexOtbor(int? categoryId, string searchHere, int? searchHereId, decimal? priceMax, decimal? priceMin, List<BrandViewModel> brands)
        {
            var products = new List<Product>();

            if (categoryId.HasValue)
            {
                products = _productService.GetByIdFromCategory(categoryId.Value);
            }
            else
            {
                products = _productService.GetAll();
            }

            if (priceMax.HasValue && priceMin.HasValue)
            {
                products = products.Where(_ => _.Price >= priceMin.Value && _.Price <= priceMax.Value).ToList();
            }

            if (searchHereId.HasValue)
            {
                categoryId = _productService.GetById(searchHereId.Value)?.CategoryId;
                products = products.Where(_ => _.Id == searchHereId.Value || _.CategoryId == categoryId.Value).ToList();
            }
            var brandIds = brands.Where(_ => _.IsSelected).Select(_ => _.Id).ToList();
            if(brandIds.Count > 0)
            {
                products = products.Where(_ => brandIds.Any(x=>_.BrandId == x)).ToList();
            }
            
            var productsItems = _mapper.Map<List<ProductViewModel>>(products);
            var brandsItems = _mapper.Map<List<BrandViewModel>>(_context.Brands.ToList());
            return View(new ProductsViewModel { Items = productsItems , Brands = brandsItems });
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contacts()
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

        ProductCategory GetParentCategory(ProductCategory category)
        {
            ProductCategory outCategory = category;
            if (category?.ParentCategory != null)
                outCategory = GetParentCategory(category.ParentCategory);

            return outCategory;
        }

        public JsonResult GetPrice()
        {
            return Json(new { MinPrice = _context.Products.Min(_ => _.Price), MaxPrice = _context.Products.Max(_ => _.Price) });
        }

    }
}
