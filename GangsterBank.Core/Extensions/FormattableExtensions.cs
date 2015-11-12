namespace GangsterBank.Core.Extensions
{
    using System;
    using System.Globalization;

    public static class FormattableExtensions
    {
        public static string ToInvariantCultureString(this IFormattable value)
        {
            return value.ToString(null, CultureInfo.InvariantCulture);
        }
    }
}
