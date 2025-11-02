namespace Tekus.Api.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string Apikeyname = "X-API-Key"; // Expected header name for the API Key

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //  Check if the required API Key header is present
        if (!context.Request.Headers.TryGetValue(Apikeyname, out var extractedApiKey))
        {
            context.Response.StatusCode = 401; 
            await context.Response.WriteAsync("API Key header was not provided.");
            return;
        }

        //  Retrieve the expected key from configuration (User Secrets)
        var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = appSettings.GetValue<string>("ApiKey");

        //  Compare the provided key with the expected key
        if (apiKey != null && !apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401; 
            await context.Response.WriteAsync("Invalid API Key provided.");
            return;
        }

        // If the key is valid, continue the request pipeline
        await _next(context);
    }
}