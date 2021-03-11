using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Areas.Admin.Models.Common
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel()
        {
            Action = "Index";
            Childs = new List<MenuItemViewModel>();
        }

        public string Controller { get; set; }
        public string Action { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }
        public bool IsActive { get; set; }

        public List<MenuItemViewModel> Childs { get; set; }
    }
}
