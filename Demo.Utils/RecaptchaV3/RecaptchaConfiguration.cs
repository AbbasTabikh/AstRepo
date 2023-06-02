namespace Demo.Utils.RecaptchaV3
{
    public class RecaptchaConfiguration
    {
        public RecaptchaConfiguration(string secretKey, string verifyURL)
        {
            SecretKey = secretKey;
            VerifyURL = verifyURL;
        }

        public string SecretKey { get; }
        public string VerifyURL { get; }
    }
}