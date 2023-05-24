using System.Diagnostics.Metrics;
using System.Globalization;

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

        //Not working
        public static string ToEnglish(this string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            // Create a CultureInfo object for Lebanon.
            CultureInfo arabicCulture = CultureInfo.GetCultureInfo("ar-LB");

            CultureInfo englishCulture = CultureInfo.GetCultureInfo("en");

            TextInfo arText = arabicCulture.TextInfo;

            string englishText = arText.ToTitleCase(value.ToLower(englishCulture));

            return englishText;
        }

    }
}
