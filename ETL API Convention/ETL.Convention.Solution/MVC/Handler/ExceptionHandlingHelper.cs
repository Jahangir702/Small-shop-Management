using Microsoft.AspNetCore.Diagnostics;

namespace MVC.Handler
{
    public class ExceptionHandlingHelper
    {
        public static void HandleException(IApplicationBuilder app, Exception exception)
        {
            var context = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>().HttpContext;
            var session = context?.Session;

            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "text/html";

            var errorMessage = $"An error occurred: {exception.Message}";

            string redirectUrl = "";
            var requestPath = context.Features.Get<IExceptionHandlerPathFeature>()?.Path;
            {
                redirectUrl = "/MessageHandle/ErrorHandle";
            }
            context.Response.Redirect(redirectUrl);
        }
    }
}
