using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using my_books_V1._0.Data.ViewModels;

namespace my_books_V1._0.Exceptions
{
    public static class ExceptionMiddleWareExtensions
    {
        public static void ConfigureBuildInExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var logger = loggerFactory.CreateLogger("ConfigureBuildInExceptionHandler");

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if (contextFeature != null)
                    {
                        var errorVMSString = new ErrorVM()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Path = contextRequest.Path
                        }.ToString();

                        logger.LogError(errorVMSString);
                        await context.Response.WriteAsync(errorVMSString);
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleWare>();
        }
    }
}
