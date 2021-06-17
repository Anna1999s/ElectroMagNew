using AutoMapper;
using Interfaces.Content;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models.Content;

namespace WebSite.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewService _newService;
        private readonly IMapper _mapper;

        public NewsController(INewService newService, IMapper mapper)
        {
            _mapper = mapper;
            _newService = newService;
        }
        public async Task<IActionResult> Details(int id)
        {            
            var news = await _newService.GetById(id);
            var model = _mapper.Map<NewViewModel>(news);
            return View(model);
        }
    }
}
