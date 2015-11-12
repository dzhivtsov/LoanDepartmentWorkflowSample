namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCountryCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "Name", c => c.String());
            DropColumn("dbo.Countries", "CountryName");
            DropColumn("dbo.Countries", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "Code", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "CountryName", c => c.String());
            DropColumn("dbo.Countries", "Name");
        }
    }
}
