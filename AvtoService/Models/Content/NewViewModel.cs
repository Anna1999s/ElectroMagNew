using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models.Content
{
    public class NewViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Содержание")]
        public string Context { get; set; }
        public DateTime Created { get; set; }

        public PhotoViewModel Photo { get; set; }
        public IFormFile UploadedPhoto { get; set; }
    }
}
