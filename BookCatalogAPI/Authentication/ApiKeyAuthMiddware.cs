using Microsoft.AspNetCore.Mvc;

namespace BookCatalogAPI.Authentication;

public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;

    public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            var details = new ProblemDetails()
            {
                Title = "API key missing",
                Detail = "The API key couldn't be found in the response header",
                Status = 401,
                Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/401"
            };
            await context.Response.WriteAsJsonAsync(details);
            return;
        }

        string apiKey = _config.GetValue<string>(AuthConstants.ApiKeySectionName);
        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            var details = new ProblemDetails()
            {
                Title = "Invalid API key",
                Detail = "The value provided doesn't match any API key",
                Status = 401,
                Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/401"
            };
            await context.Response.WriteAsJsonAsync(details);
            return;
        }

        await _next(context);
    }
}