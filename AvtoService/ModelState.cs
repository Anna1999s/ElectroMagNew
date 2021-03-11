using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebSite
{
    public class ModelState
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public ModelValidationState ValidationState { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}