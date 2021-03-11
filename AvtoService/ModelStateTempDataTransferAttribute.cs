using Microsoft.AspNetCore.Mvc.Filters;

namespace WebSite
{
    public abstract class ModelStateTempDataTransferAttribute : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTempDataTransferAttribute).FullName;
    }
}