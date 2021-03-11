using DomainModel.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models.Navigation
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel()
        {
            Action = "Index";
            Controller = "Page";
        }
        public int Id { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }

        public int? MenuId { get; set; }
        public string StringKey { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        [Display(Name = "External reference")]
        public string CustomUrl { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Is hidden")]
        public bool IsHidden { get; set; }

        public int? PageId { get; set; }        
        public PageViewModel Page { get; set; }

        public List<PageViewModel> Pages { get; set; }
    }
}
