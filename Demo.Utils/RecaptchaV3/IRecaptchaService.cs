namespace Demo.Utils.RecaptchaV3
{
    public interface IRecaptchaService
    {
        Task<RecaptchaResponse> ValidateToken(string token, CancellationToken cancellationToken);
    }
}