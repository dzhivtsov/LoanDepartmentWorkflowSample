namespace GangsterBank.BusinessLogic.Statistics
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.BusinessLogic.Contracts.Statistics;
    using GangsterBank.Domain.BusinessLogicEntities.Statistics;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;

    public class StatisticsManager : IStatisticsManager
    {
        private readonly ICreditService creditService;
        
        public StatisticsManager(ICreditService creditService)
        {
            this.creditService = creditService;
        }

        public IEnumerable<PossiblePaymentsStatistic> GetPossiblePaymentsStatistics(IStatisticContext context)
        {
            var allTakenLoans = this.creditService.GetAllTakenLoans().ToList();
            var result = allTakenLoans.GroupBy(x => x.ProductLoan.Id).Select(x => new PossiblePaymentsStatistic
                {
                    LoanProductName = x.First().ProductLoan.Name,
                    TotalAmount = x.SelectMany(y => y.Payments
                        .Where(p => p.Date <= context.End && p.Date >= context.Start)
                        .Select(p => p.Amount))
                        .Sum()
                });
            return result;
        }

        public IEnumerable<TakenLoanPaymentStatistic> GetTakenLoanPaymentStatistics(IStatisticContext context)
        {
            var allTakenLoans = this.creditService.GetAllTakenLoans().ToList();
            var notOnTime =
                allTakenLoans.Where(x => x.TakeDate >= context.Start && x.TakeDate <= context.End)
                    .Where(
                        x =>
                        x.Payments.Any(
                            payment => payment.Status == LoanPaymentStatus.Active && payment.Date < context.Today)).ToArray();
            var onTime = allTakenLoans.Except(notOnTime).ToArray();
            var result = new Collection<TakenLoanPaymentStatistic>
                             {
                                 new TakenLoanPaymentStatistic
                                     {
                                         PaymentCategory = "Not On Time",
                                         TakeCount = notOnTime.Count(),
                                         TotalAmount = notOnTime.Select(x => x.Amount).Sum()
                                     },
                                 new TakenLoanPaymentStatistic
                                     {
                                         PaymentCategory = "On Time",
                                         TakeCount = onTime.Count(),
                                         TotalAmount = onTime.Select(x => x.Amount).Sum()
                                     }
                             };
            return result;
        }

        public IEnumerable<SummaryLoanProductStatistic> GetSummaryLoanProductStatistics(IStatisticContext context)
        {
            var allTakenLoans = this.creditService.GetAllTakenLoans().ToList();
            //var allTakenLoans = new Collection<TakenLoan>
            //                        {
            //                            new TakenLoan
            //                                {
            //                                    Amount = 1000,
            //                                    Id = 1,
            //                                    ProductLoan = new LoanProduct
            //                                                      {
            //                                                          Id = 1,
            //                                                          Name = "a",
            //                                                          Status = LoanProductStatus.Active,
            //                                                      }
            //                                },
            //                            new TakenLoan
            //                                {
            //                                    Amount = 100,
            //                                    Id = 2,
            //                                    ProductLoan = new LoanProduct
            //                                                      {
            //                                                          Id = 2,
            //                                                          Name = "b",
            //                                                          Status = LoanProductStatus.Active,
            //                                                      }
            //                                }
            //                        };
            var statistics = allTakenLoans.Where(x => x.TakeDate >= context.Start && x.TakeDate <= context.End)
                .GroupBy(x => x.ProductLoan.Id).Select(x => new SummaryLoanProductStatistic
                                          {
                                              LoanProductId = x.First().ProductLoan.Id,
                                              LoanProductName = x.First().ProductLoan.Name,
                                              Status = x.First().ProductLoan.Status,
                                              TakeCount = x.Count(),
                                              TotalAmount = x.Select(takenLoan => takenLoan.Amount).ToArray().Sum()
                                          });
            return statistics;
        }
    }
}
