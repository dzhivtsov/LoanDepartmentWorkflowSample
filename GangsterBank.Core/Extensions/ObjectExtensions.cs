namespace GangsterBank.Core.Extensions
{
    using System.Diagnostics.Contracts;

    public static class ObjectExtensions
    {
        #region Public Methods and Operators

        [Pure]
        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }

        [Pure]
        public static bool IsNull(this object value)
        {
            bool isNull = value == null;
            return isNull;
        }

        #endregion
    }
}