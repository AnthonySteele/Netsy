
namespace Netsy.DemoWeb.Controllers
{
    using System.Text.RegularExpressions;

    public static class Helpers
    {
        public static string RemoveNonAlphanumeric(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return string.Empty;
            }

            inputString = inputString.Trim();
            if (inputString.Length > 256)
            {
                inputString = inputString.Substring(0, 256);
            }
            inputString = Regex.Replace(inputString, "[^A-Za-z0-9]", "");
            return inputString;
        }

    }
}
