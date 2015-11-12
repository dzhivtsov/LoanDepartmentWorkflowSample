namespace GangsterBank.Core.Extensions
{
    using System.Diagnostics.Contracts;

    public static class IntExtensions
    {
        #region Public Methods and Operators

        [Pure]
        public static bool IsPositive(this int number)
        {
            bool isPositive = number > 0;
            return isPositive;
        }

        #endregion
    }
}