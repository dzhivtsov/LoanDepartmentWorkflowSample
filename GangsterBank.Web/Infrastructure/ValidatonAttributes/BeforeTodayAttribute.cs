namespace GangsterBank.Web.Infrastructure.ValidatonAttributes
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class BeforeTodayAttribute : RangeAttribute
    {
        #region Constants

        private const int MaximumYearsBefore = 100;

        private const int MinimumDaysBefore = 1;

        #endregion

        #region Constructors and Destructors

        public BeforeTodayAttribute()
            : base(typeof(DateTime), MinimumDate, MaximumDate)
        {
        }

        #endregion

        #region Properties

        private static string MaximumDate
        {
            get
            {
                return DateTime.Now.AddDays(-MinimumDaysBefore).ToShortDateString();
            }
        }

        private static string MinimumDate
        {
            get
            {
                return DateTime.Now.AddYears(-MaximumYearsBefore).ToShortDateString();
            }
        }

        #endregion
    }
}