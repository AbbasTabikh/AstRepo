using System.Net.Http.Headers;
using System.Text.Json;

namespace Demo.Utils.RecaptchaV3
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly RecaptchaConfiguration _recaptchaConfiguration;

        public RecaptchaService(
            RecaptchaConfiguration recaptchaConfiguration
        )
        {
            _recaptchaConfiguration = recaptchaConfiguration;
        }

        public async Task<RecaptchaResponse> ValidateToken(string token, CancellationToken cancellationToken)
        {
            using var client = new HttpClient();
        
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _recaptchaConfiguration.SecretKey),
                new KeyValuePair<string, string>("response", token)
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await client.PostAsync(_recaptchaConfiguration.VerifyURL, content, cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<RecaptchaResponse>(result);
        }
    }
}