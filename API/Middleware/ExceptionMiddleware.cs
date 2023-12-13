using System;
using System.Net;
using System.Text.Json;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Middleware;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> logger;
    private readonly IHostEnvironment hostEnvironment;
    private readonly RequestDelegate requestDelegate;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger,IHostEnvironment hostEnvironment,RequestDelegate requestDelegate) 
    {
        this.logger = logger;
        this.hostEnvironment = hostEnvironment;
        this.requestDelegate = requestDelegate;
    }

        public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await requestDelegate(httpContext);
        }
        catch (System.Exception e)
        {
            logger.LogError(e, e.Message);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = hostEnvironment.IsDevelopment()
                ? new ApiException(httpContext.Response.StatusCode, e.Message, e.StackTrace?.ToString())
                : new ApiException(httpContext.Response.StatusCode, e.Message, "InternalServerError");

            var opt = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, opt);

            await httpContext.Response.WriteAsync(json);
        }
    }
}
