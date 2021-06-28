using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models.Content
{
    public class BrandViewModel : Brand
    {
        public bool IsSelected { get; set; }
    }
}
