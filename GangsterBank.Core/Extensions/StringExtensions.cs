namespace GangsterBank.Core.Extensions
{
    using System.Diagnostics.Contracts;

    public static class StringExtensions
    {
        [Pure]
        public static bool IsNullOrEmpty(this string str)
        {
            bool result = string.IsNullOrEmpty(str);
            return result;
        }

        [Pure]
        public static bool IsNullOrWhiteSpace(this string str)
        {
            bool result = string.IsNullOrWhiteSpace(str);
            return result;
        }
    }
}