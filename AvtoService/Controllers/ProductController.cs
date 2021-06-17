using AutoMapper;
using Interfaces.Content;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models.Content;

namespace WebSite.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IPhotoService _photoService;
        public ProductController(IMapper mapper, ApplicationDbContext context, IProductService productService, IPhotoService photoService) : base(mapper)
        {
            _context = context;
            _photoService = photoService;
            _productService = productService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id, string Error)
        {          
            var entity = _productService.GetById(id);
            var model = _mapper.Map<ProductViewModel>(entity);

            return View(model);
        }
    }
}
