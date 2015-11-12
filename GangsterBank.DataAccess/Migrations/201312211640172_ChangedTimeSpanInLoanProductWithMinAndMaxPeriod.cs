namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTimeSpanInLoanProductWithMinAndMaxPeriod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanProducts", "MinPeriodInMonth", c => c.Int(nullable: false));
            AddColumn("dbo.LoanProducts", "MaxPeriodInMonth", c => c.Int(nullable: false));
            DropColumn("dbo.LoanProducts", "Maturity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanProducts", "Maturity", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.LoanProducts", "MaxPeriodInMonth");
            DropColumn("dbo.LoanProducts", "MinPeriodInMonth");
        }
    }
}
