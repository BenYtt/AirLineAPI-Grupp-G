using AirLineAPI.Db_Context;
using AirLineAPI.Model;
using AirLineAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AirLineAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        public const string ApiKeyHeaderName = "username";
        public const string ApiKeyHeaderPassword = "password";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Query.TryGetValue(ApiKeyHeaderName, out var potentialApiKeyName))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!context.HttpContext.Request.Query.TryGetValue(ApiKeyHeaderPassword, out var potentialApiKeypassword))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userlogin = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var apiKey = await userlogin.Login(potentialApiKeyName, potentialApiKeypassword);

            if (!apiKey.Success)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}