using Microsoft.AspNetCore.Mvc;
using WebSite.Models.Account;

namespace WebSite.Areas.Admin.ViewComponents
{
    public class AdminMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = GetUserModel();
            return View(model);
        }

        public UserMenuViewModel GetUserModel()
        {
            var model = new UserMenuViewModel
            {
                UserName = User?.Identity?.Name
            };

            return model;
        }
    }
}
