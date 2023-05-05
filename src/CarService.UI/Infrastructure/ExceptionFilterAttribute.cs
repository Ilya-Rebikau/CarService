using CarService.UI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace CarService.UI.Infrastructure
{
    [AttributeUsage(AttributeTargets.All)]
    public class ExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            var content = exceptionType.GetProperty("Content");
            string exceptionMessage;
            if (content != null)
            {
                exceptionMessage = content.GetValue(context.Exception).ToString();
            }
            else
            {
                exceptionMessage = context.Exception.Message;
            }

            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
            {
                Model = new ErrorViewModel { Message = exceptionMessage },
            };
            context.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = viewData,
            };

            context.ExceptionHandled = true;
        }
    }
}
