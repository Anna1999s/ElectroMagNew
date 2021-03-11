using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace WebSite
{
    public class ImportModelStateFromTempDataAttribute : ModelStateTempDataTransferAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var controller = filterContext.Controller as Controller;

            if (controller.TempData.ContainsKey(Key))
            {
                var modelStates =
                    JsonConvert.DeserializeObject<IEnumerable<ModelState>>(controller.TempData[Key] as string);

                if (filterContext.Result is ViewResult)
                    foreach (var modelState in modelStates)
                    {
                        controller.ViewData.ModelState.SetModelValue(modelState.Key, modelState.Value,
                            modelState.Value);
                        controller.ViewData.ModelState[modelState.Key].ValidationState = modelState.ValidationState;

                        if (modelState.ValidationState == ModelValidationState.Invalid)
                            foreach (var error in modelState.Errors)
                                controller.ViewData.ModelState[modelState.Key].Errors.Add(new ModelError(error));
                    }

                else controller.TempData.Remove(Key);
            }
        }
    }
}