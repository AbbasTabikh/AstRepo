namespace Demo.Utils.RecaptchaV3
{
    public class RecaptchaResponse
    {
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
        public double score { get; set; }
        public string[] Errorcodes { get; set; }
    }
}