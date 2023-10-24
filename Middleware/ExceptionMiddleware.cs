using CitasApp.Errors;
using System.Net;
using System.Text.Json;

namespace CitasApp.Middleware;
public class ExceptionMiddleware
{
    public RequestDelegate _next { get; }
    public ILogger _Logger { get; }
    public IHostEnvironment _env { get; }


    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env){
        this._env = env;
        this._next = next;
        this._Logger = logger;   
    }


    public async Task InvokeAsync(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception ex){ _Logger.LogError(ex, ex.Message);
            context.Response.ContentType = "aplication/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            var response = _env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                : new ApiException(context.Response.StatusCode, ex.Message, "Internal server error");

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }

    }

}
