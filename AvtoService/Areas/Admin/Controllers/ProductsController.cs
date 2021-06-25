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

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category).Include(p => p.Warehouse).Include(p =>p.Photos);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
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

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Admin/Products/Edit/5
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
            return View(model);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    
                    _context.Update(product);
                    await _context.SaveChangesAsync();
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
            return View(model);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/Products/Delete/5
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
