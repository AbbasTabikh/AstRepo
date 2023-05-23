using System.Diagnostics.Metrics;

namespace Demo.Api.Extension_Methods
{
    public static class StringExtension
    {

        public static bool IsEnglishLetterOrWhiteSpace(this char value)
        {
            return char.IsUpper(value) || char.IsLower(value) || char.IsWhiteSpace(value);
        }


        public static bool IsEnglish(this string value)
        {
           return value.All(c => c.IsEnglishLetterOrWhiteSpace());
        }


    }
}
