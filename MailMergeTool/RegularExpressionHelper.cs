namespace MailMergeTool
{
    using System.Text.RegularExpressions;

    public class RegularExpressionHelper
    {
        public string Replace(string input, string replacement, string pattern)
        {
            return Regex.Replace(input, pattern, replacement);
        }
    }
}