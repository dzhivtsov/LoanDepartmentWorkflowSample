namespace GangsterBank.Domain.Entities.Credits
{
    using GangsterBank.Domain.Entities.Base;

    public class LoanProduct : BaseEntity
    {
        public virtual decimal MinAmount { get; set; }

        public virtual decimal MaxAmount { get; set; }

        public virtual int Percentage { get; set; }

        public virtual int MinPeriodInMonth { get; set; }

        public virtual int MaxPeriodInMonth { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual LoanProductRequirements Requirements { get; set; }

        public virtual LoanProductType Type { get; set; }

        public virtual LoanProductStatus Status { get; set; }

        public virtual int FineDayPercentage { get; set; }

        public virtual int AdvancedRepaymentFinePercentage { get; set; }

        public virtual int AdvancedRepaymentFirstPossibleMonth { get; set; }

        public LoanProduct()
        {
            
        }

        public LoanProduct(LoanProduct product)
        {
            Update(product);
        }

        public void Update(LoanProduct product)
        {
            this.IsDeleted = product.IsDeleted;
            this.MinAmount = product.MinAmount;
            this.Percentage = product.Percentage;
            this.MinPeriodInMonth = product.MinPeriodInMonth;
            this.MaxPeriodInMonth = product.MaxPeriodInMonth;
            this.Name = product.Name;
            this.Description = product.Description;
            this.Type = product.Type;
            this.Status = product.Status;
            this.Requirements.Update(product.Requirements);
            this.FineDayPercentage = product.FineDayPercentage;
            this.AdvancedRepaymentFirstPossibleMonth = product.AdvancedRepaymentFirstPossibleMonth;
            this.AdvancedRepaymentFinePercentage = product.AdvancedRepaymentFinePercentage;
        }
    }
}