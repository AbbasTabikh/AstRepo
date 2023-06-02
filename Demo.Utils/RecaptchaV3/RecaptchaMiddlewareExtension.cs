using Microsoft.AspNetCore.Builder;

namespace Demo.Utils.RecaptchaV3
{
    public static class RecaptchaMiddlewareExtensions
    {
        public static IApplicationBuilder UseRecaptchaMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RecaptchaMiddleware>();
            return app;
        }
    }
}