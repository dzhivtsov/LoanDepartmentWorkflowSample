namespace GangsterBank.Models.Kendo
{
    using GangsterBank.Web.Models.Kendo;

    public class AutoCompleteSourceRequest
    {
        #region Public Properties

        public bool IgnoreCase { get; set; }

        public AutoCompleteOperator Operator { get; set; }

        public string Value { get; set; }

        #endregion
    }
}