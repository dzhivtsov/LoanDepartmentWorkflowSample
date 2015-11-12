namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressCaseNumberMadeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "CaseNumber", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "CaseNumber", c => c.Int(nullable: false));
        }
    }
}
