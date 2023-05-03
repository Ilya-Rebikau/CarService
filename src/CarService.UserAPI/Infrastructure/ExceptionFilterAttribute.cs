using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CarService.UserAPI.Infrastructure
{
    [AttributeUsage(AttributeTargets.All)]
    internal class ExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string exceptionMessage = context.Exception.Message;
            if (context.Exception is MyException)
            {
                context.Result = new BadRequestObjectResult(context.Exception)
                {
                    Value = exceptionMessage,
                };
            }
            else
            {
                context.Result = new StatusCodeResult(500);
            }

            context.ExceptionHandled = true;
        }
    }
}
