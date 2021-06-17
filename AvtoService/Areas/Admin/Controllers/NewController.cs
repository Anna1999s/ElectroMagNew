using AutoMapper;
using DomainModel.Content;
using DomainModel.Framework;
using DomainModel.Framework.Models;
using DomainModel.Localization;
using Interfaces.Content;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models.Content;

namespace WebSite.Areas.Admin.Controllers
{
    public class NewController : BaseAdminController
    {
        private readonly INewService _newService;
        private readonly IPhotoService _photoService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOptions<GoogleTranslateConfig> _googleTranslateConfig;

        public NewController(IMapper mapper, INewService newService, IPhotoService photoService, IWebHostEnvironment webHostEnvironment, IOptions<GoogleTranslateConfig> googleTranslateConfig) : base(mapper)
        {
            _newService = newService;
            _photoService = photoService;
            _webHostEnvironment = webHostEnvironment;
            _googleTranslateConfig = googleTranslateConfig;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new NewViewModel{};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var news = new New{};
            news.Id = model.Id;
            news.Name = model.Name;
            news.Context = model.Context;
            news.Created = DateTime.Now;
           
            if (model.UploadedPhoto != null)

            news.Photo = await _photoService.AddNew(model.UploadedPhoto, news.Id, _webHostEnvironment.WebRootPath);
            await _newService.Add(news);
            return RedirectToAction("News");
        }

        public async Task<IActionResult> News()
        {
            
            var news = _mapper.Map<List<NewViewModel>>(await _newService.GetAll()); 
            return View(news);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var entity = await _newService.GetById(Id);
            var model = _mapper.Map<NewViewModel>(entity);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id, NewViewModel model)
        {
            var news = await _newService.GetById(id);
            news.Name = model.Name;
            news.Context = model.Context;
            news.Updated = DateTime.Now;

            if (model.UploadedPhoto != null)
                news.Photo = await _photoService.AddNew(model.UploadedPhoto, news.Id, _webHostEnvironment.WebRootPath);

            await _newService.Update(news);
            return RedirectToAction("News", "New");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _newService.Delete(id);
            return RedirectToAction("News");
        }

    }
}
