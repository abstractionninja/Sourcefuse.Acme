using Microsoft.Extensions.Options;
using Sourcefuse.Acme.WebApi.Configuration;

namespace Sourcefuse.Acme.WebApi.Middleware
{
    /// <summary>
    /// Middleware Implementation of Api Key Auth
    /// </summary>
    /// <param name="next"></param>
    /// <param name="settings"><see cref="ApiKeySettings"/></param>
    public class ApiKeyMiddleware(RequestDelegate next, IOptions<ApiKeySettings> settings)
    {
        private readonly RequestDelegate _next = next;

        private readonly ApiKeySettings _settings = settings.Value;

        /// <summary>
        /// Context Request Delegate
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(_settings.ApiKeyHeaderKey, out var apikeyheadervalue) ||
                !apikeyheadervalue.Equals(_settings.DefaultApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(_settings.ApiKeyFailureMessage);
                return;
            }

            await _next(context);
        }
    }
}
