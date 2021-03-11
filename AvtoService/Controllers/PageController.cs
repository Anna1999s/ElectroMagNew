using AutoMapper;
using Interfaces.Content;
using Interfaces.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class PageController : Controller
    {
        private readonly ILocalizedPageService _localizedPageService;
        private readonly IMapper _mapper;

        public PageController(ILocalizedPageService localizedPageService, IMapper mapper)
        {
            _localizedPageService = localizedPageService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string stringKey, string customUrl)
        {
            if(customUrl != null)
                return Redirect(customUrl);

            var page =  _localizedPageService.GetByStringKey(stringKey, "ru");
            if (page == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<PageViewModel>(page);
            return View(model);
        }
        public async Task<IActionResult> About(string stringKey, string customUrl)
        {
            if (customUrl != null)
                return Redirect(customUrl);

            var page = _localizedPageService.GetByStringKey(stringKey, "ru");
            if (page == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<PageViewModel>(page);
            return View(model);
        }
    }
}
