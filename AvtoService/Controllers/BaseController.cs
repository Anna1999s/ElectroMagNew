using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    public class BaseController : Controller
    {
        public readonly IMapper _mapper;

        public BaseController(IMapper mapper)
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
    }

    public class Constants
    {
        public const string NotificationsSuccess = "NotificationsSuccess";
        public const string NotificationsError = "NotificationsError";
    }
}
