using DomainModel.Content;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebSite.Models.Navigation;

namespace WebSite.Models
{
    public class PageViewModel
    {
        public int Id { get; set; }
        public bool IsHidden { get; set; }

        public string StringKey { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        public bool IsSection { get; set; }

        [Display(Name = "Meta-title")]
        public string MetaTitle { get; set; }

        [Display(Name = "Meta-keywords")]
        public string MetaKeywords { get; set; }

        [Display(Name = "Meta-description")]
        public string MetaDescription { get; set; }

        public IList<MenuItemViewModel> MenuItems { get; set; }
    }
}
