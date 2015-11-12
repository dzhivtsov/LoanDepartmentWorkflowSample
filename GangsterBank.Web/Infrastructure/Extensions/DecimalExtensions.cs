namespace GangsterBank.Web.Infrastructure.Extensions
{
    using System;

    public static class DecimalExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static string ToGBString(this decimal value)
        {
            //value = Math.Ceiling(value / 100) * 100;
            return string.Format("{0:c0}", value);
        }
    }
}