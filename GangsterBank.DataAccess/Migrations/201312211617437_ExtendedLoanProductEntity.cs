namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendedLoanProductEntity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LoanProducts", "Name");
            AddColumn("dbo.LoanProducts", "Name", c => c.String());
            AddColumn("dbo.LoanProducts", "Description", c => c.String());
            AlterColumn("dbo.LoanProducts", "Maturity", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoanProducts", "Maturity", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.LoanProducts", "Description");
        }
    }
}
