using System.Globalization;

namespace VisaHackathon2020.Giveback
{
    public static class Extensions
    {
        public static string ToTitleCase(this string str)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(str.ToLower());
        }
    }
}