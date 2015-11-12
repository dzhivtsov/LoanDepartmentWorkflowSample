namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanProductDailyFinePercentage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanProducts", "FineDayPercentage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoanProducts", "FineDayPercentage");
        }
    }
}
