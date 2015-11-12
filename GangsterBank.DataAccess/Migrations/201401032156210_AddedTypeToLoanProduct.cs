namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTypeToLoanProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanProducts", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.LoanProducts", "Percentage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoanProducts", "Percentage", c => c.Double(nullable: false));
            DropColumn("dbo.LoanProducts", "Type");
        }
    }
}
