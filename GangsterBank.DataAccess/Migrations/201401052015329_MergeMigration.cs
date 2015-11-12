namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergeMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanRequests", "Months", c => c.Int(nullable: false));
            DropColumn("dbo.LoanRequests", "Monthes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanRequests", "Monthes", c => c.Int(nullable: false));
            DropColumn("dbo.LoanRequests", "Months");
        }
    }
}
