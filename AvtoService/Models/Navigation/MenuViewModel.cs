using DomainModel.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models.Navigation
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        [Display(Name="Name")]
        public string Name { get; set; }
        public IList<MenuItemViewModel> Items { get; set; }
    }
}
