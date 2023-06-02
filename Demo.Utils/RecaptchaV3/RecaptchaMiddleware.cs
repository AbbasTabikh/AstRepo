using Demo.Utils.Models;
using Microsoft.AspNetCore.Http;

namespace Demo.Utils.RecaptchaV3
{
    public class RecaptchaMiddleware
    {
        private readonly RequestDelegate _next;

        public RecaptchaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRecaptchaService recaptchaService)
        {
            var cancellationToken = new CancellationTokenSource().Token;

            if (context.Request.Method == "POST")
            {
                // Can be done also FromQuery
                var recaptchaToken = context.Request.Form?["recaptcha"];

                // Validate the token using the ReCaptchaService
                var result = await recaptchaService.ValidateToken(recaptchaToken, cancellationToken);
                if (result != null && !result.success)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(new LogModel
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "This is a bot"
                    }.ToString(), cancellationToken);
                    return;
                }
            }

            await _next(context);
        }
    }
}