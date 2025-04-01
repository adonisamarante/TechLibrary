using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.Filters
{
    // every time there is an exception in the application, this filter will be triggered
    // after creating this file, you need to register it in the Program.cs file, before the line "var app = builder.Build();"
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is TechLibraryException techLibraryException)
            {
                context.HttpContext.Response.StatusCode = (int)techLibraryException.GetStatusCode();
                context.Result = new ObjectResult(new ResponseErrorMessagesJson
                {
                    Errors = techLibraryException.GetErrorMessages(),
                });
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult(new ResponseErrorMessagesJson
                {
                    Errors = ["Erro desconhecido."],
                });
            }
        }
    }
}
