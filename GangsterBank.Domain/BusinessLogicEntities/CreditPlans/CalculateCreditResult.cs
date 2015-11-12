using System.Collections.Generic;

namespace GangsterBank.Domain.BusinessLogicEntities.CreditPlans
{
    public class CalculateCreditResult
    {
        public IEnumerable<decimal> Payments { get; set; }

        public decimal TotalPayment { get; set; }
    }
}
