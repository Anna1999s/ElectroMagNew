using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager) : base(mapper)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }
        public IActionResult Details(int id)
        {            
            var order = _context.Orders.FirstOrDefault(_ => _.Id == id);
            return View(order);
        }
    }
}
