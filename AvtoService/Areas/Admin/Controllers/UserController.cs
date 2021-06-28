using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Interfaces.Content;
using Microsoft.AspNetCore.Mvc;
using Services.Content;
using WebSite.Areas.Admin.Models.Common;
using WebSite.Models.Account;

namespace WebSite.Areas.Admin.Controllers
{
    
    public class UserController : BaseAdminController
    {
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService) : base(mapper)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            var users = _userService.GetAll();
            var model = _mapper.Map<UserViewModel>(users);

            return PartialView(model);
        }
    }
}
