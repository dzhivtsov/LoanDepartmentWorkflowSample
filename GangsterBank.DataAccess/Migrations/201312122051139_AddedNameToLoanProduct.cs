namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameToLoanProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanProducts", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoanProducts", "Name");
        }
    }
}
