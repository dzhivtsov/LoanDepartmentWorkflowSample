namespace GangsterBank.Web.Models.Credit.CalculateCredit
{
    using System.Collections.Generic;
    using System.Web.Script.Services;

    public class CalculationResultModel
    {
        public IEnumerable<MonthlyPaymentViewModel> MonthlyPayments { get; set; }

        public string TotalPayment { get; set; }
    }
}