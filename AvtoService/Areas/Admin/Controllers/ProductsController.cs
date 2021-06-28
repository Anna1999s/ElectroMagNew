using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainModel.Content;
using Services;
using WebSite.Models.Content;
using Interfaces.Content;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;

namespace WebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IPhotoService _photoService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(IMapper mapper, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IProductService productService, IPhotoService photoService) : base(mapper)
        {
            _context = context;
            _photoService = photoService;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Name");
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(model);
               
                if (model.Description != null) product.Description =(model.Description);
                await _productService.Add(product);

                if (model.UploadedPhotos != null)
                    foreach (var photo in model.UploadedPhotos)
                        product.Photos.Add(await _photoService.Add(photo, product.Id, _webHostEnvironment.WebRootPath));
            }

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Edit(int id)
        {  
            var entity = _productService.GetById(id);

            if (entity == null)
            {
                ErrorMessage("Not found");
                return RedirectToAction("InactiveLots");
            }

            var model = _mapper.Map<ProductViewModel>(entity);
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", entity.CategoryId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Name", entity.WarehouseId);
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", entity.BrandId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _productService.GetById(id);
                    product = _mapper.Map(model, product);
                    if (model.UploadedPhotos != null)
                        foreach (var photo in model.UploadedPhotos)
                            product.Photos.Add(await _photoService.Add(photo, product.Id,
                                _webHostEnvironment.WebRootPath));
                    
                    _context.Products.Update(product);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", model.CategoryId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Name", model.WarehouseId);
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", model.BrandId);
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
