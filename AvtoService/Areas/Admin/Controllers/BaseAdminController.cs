using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BaseAdminController : Controller
    {
        public readonly IMapper _mapper;

        public BaseAdminController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected void SuccessMessage(string message)
        {
            TempData[Constants.NotificationsSuccess] = message;
        }

        protected void ErrorMessage(string message)
        {
            TempData[Constants.NotificationsError] = message;
        }

        public class Constants
        {
            public const string NotificationsSuccess = "NotificationsSuccess";
            public const string NotificationsError = "NotificationsError";
        }
    }
}
