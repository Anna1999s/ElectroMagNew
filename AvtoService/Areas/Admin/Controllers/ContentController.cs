using AutoMapper;
using DomainModel.Framework;
using DomainModel.Framework.Models;
using DomainModel.Localization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebSite.Areas.Admin.Controllers
{
    public class ContentController : BaseAdminController
    {
        private readonly IOptions<GoogleTranslateConfig> _googleTranslateConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContentController(IMapper mapper, IOptions<GoogleTranslateConfig> googleTranslateConfig, IWebHostEnvironment webHostEnvironment) : base(mapper)
        {
            _googleTranslateConfig = googleTranslateConfig;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {        
            return View();
        }       
    }
}
