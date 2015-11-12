namespace GangsterBank.Domain.BusinessLogicEntities.CreditPlans.Base
{
    public interface IBasePaymentWithPenaltyCreditPlanBusinessLogicEntity
    {
        /// <summary>
        /// Returns penalty if pay in concrete months
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        decimal GetPenalty(int month);

        /// <summary>
        /// Returns total payment with penalty
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        decimal GetPaymentWithPenalty(int month);
    }
}
