using System.Collections.Generic;

namespace GangsterBank.Web.Models.Credit.CalculateCredit
{
    using System.Web.Mvc;

    public class CreditCalculatorModel
    {
        public IEnumerable<SelectListItem> LoanProducts { get; set; }
    }
}