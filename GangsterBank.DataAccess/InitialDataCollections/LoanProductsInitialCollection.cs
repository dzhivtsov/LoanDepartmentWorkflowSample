namespace GangsterBank.DataAccess.InitialDataCollections
{
    using System.Collections.Generic;
    using GangsterBank.Domain.Entities.Credits;

    internal static partial class InitialDataCollections
    {
        public static IEnumerable<LoanProduct> LoanProducts = new List<LoanProduct>
        {
            new LoanProduct
            {
                Description = "It's awesome!!!",
                Name = "SuperMega",
                MaxPeriodInMonth = 12,
                MinPeriodInMonth = 3,
                Percentage = 13,
                MaxAmount = 1000000,
                MinAmount = 100,
                Type = LoanProductType.Anuitet,
                Status = LoanProductStatus.Active,
                Requirements = new LoanProductRequirements
                                   {
                                       MinSalary = 100000,
                                       MinWorkOnLastJobInMonths = 6,
                                       NeedGuarantors = false,
                                       NeedEarningsRecord = true
                                   }
            },

            new LoanProduct
            {
                Description = "It's good!!!",
                Name = "Mega",
                MaxPeriodInMonth = 12,
                MinPeriodInMonth = 3,
                Percentage = 13,
                MaxAmount = 1000000,
                MinAmount = 100,
                Type = LoanProductType.Differential,
                Status = LoanProductStatus.Active,
                Requirements = new LoanProductRequirements
                                   {
                                       MinSalary = 100000,
                                       MinWorkOnLastJobInMonths = 6,
                                       NeedGuarantors = true,
                                       NeedEarningsRecord = true,
                                       GuarantorsCount = 2
                                   }
            }
        };
    }
}
