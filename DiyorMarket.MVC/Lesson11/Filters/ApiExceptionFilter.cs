using Lesson11.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson11.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException exception)
            {
                int statusCode = (int)exception.StatusCode;
                context.Result = new RedirectToActionResult("Error", "Home", new { statusCode });
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new RedirectToActionResult("Error", "Home", new { statusCode = 500 });
                context.ExceptionHandled = true;
            }
        }
    }
}
