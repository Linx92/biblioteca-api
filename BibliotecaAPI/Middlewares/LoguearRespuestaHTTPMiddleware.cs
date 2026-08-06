using Microsoft.Extensions.Logging;

namespace BibliotecaAPI.Middlewares
{
    public class LoguearRespuestaHTTPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoguearRespuestaHTTPMiddleware> _logger;
        public LoguearRespuestaHTTPMiddleware(RequestDelegate next, ILogger<LoguearRespuestaHTTPMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var ms = new MemoryStream())
            {
                var cuerpoOriginalRespuesta = context.Response.Body;
                context.Response.Body = ms;

                await _next(context);

                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(cuerpoOriginalRespuesta);
                context.Response.Body = cuerpoOriginalRespuesta;

                _logger.LogInformation(respuesta);
            }
        }
    }
    public static class LoguearRespuestaHTTPMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoguearRespuestaHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();
        }
    }

}
