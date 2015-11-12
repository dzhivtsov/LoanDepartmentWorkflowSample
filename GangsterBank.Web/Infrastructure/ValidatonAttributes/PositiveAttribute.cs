namespace GangsterBank.Web.Infrastructure.ValidatonAttributes
{
    using System.ComponentModel.DataAnnotations;

    public class PositiveAttribute : RangeAttribute
    {
        #region Constants

        private const int MaxValue = int.MaxValue;

        private const int MinValue = 1;

        #endregion

        #region Constructors and Destructors

        public PositiveAttribute()
            : base(MinValue, MaxValue)
        {
        }

        #endregion
    }
}