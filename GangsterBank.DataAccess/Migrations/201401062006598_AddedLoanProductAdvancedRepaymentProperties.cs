namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanProductAdvancedRepaymentProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanProducts", "AdvancedRepaymentFinePercentage", c => c.Int(nullable: false));
            AddColumn("dbo.LoanProducts", "AdvancedRepaymentFirstPossibleMonth", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoanProducts", "AdvancedRepaymentFirstPossibleMonth");
            DropColumn("dbo.LoanProducts", "AdvancedRepaymentFinePercentage");
        }
    }
}
