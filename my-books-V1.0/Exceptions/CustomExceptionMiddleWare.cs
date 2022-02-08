using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using my_books_V1._0.Data.ViewModels;

namespace my_books_V1._0.Exceptions
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext,ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorVM()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Server Error from Custom MiddleWare",
                Path = "path-goes-here"
            };

            return httpContext.Response.WriteAsync(response.ToString());
        }
    }
}
