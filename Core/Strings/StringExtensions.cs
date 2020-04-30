namespace Core
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string content)
        {
            return string.IsNullOrEmpty(content);
        }

        public static bool IsNullOrwhitespace(this string content)
        {
            return string.IsNullOrWhiteSpace(content);
        }
    }
}
