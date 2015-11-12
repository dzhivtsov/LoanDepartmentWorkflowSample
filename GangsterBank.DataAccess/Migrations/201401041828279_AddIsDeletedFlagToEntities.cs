namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedFlagToEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Obligations", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonalDetails", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contacts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Addresses", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cities", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Countries", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmploymentDatas", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PassportDatas", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Properties", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TakenLoans", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.LoanProducts", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.LoanProducts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.LoanProductRequirements", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "IsDeleted");
            DropColumn("dbo.LoanProductRequirements", "IsDeleted");
            DropColumn("dbo.LoanProducts", "IsDeleted");
            DropColumn("dbo.LoanProducts", "Status");
            DropColumn("dbo.TakenLoans", "IsDeleted");
            DropColumn("dbo.Properties", "IsDeleted");
            DropColumn("dbo.PassportDatas", "IsDeleted");
            DropColumn("dbo.EmploymentDatas", "IsDeleted");
            DropColumn("dbo.Countries", "IsDeleted");
            DropColumn("dbo.Cities", "IsDeleted");
            DropColumn("dbo.Addresses", "IsDeleted");
            DropColumn("dbo.Contacts", "IsDeleted");
            DropColumn("dbo.PersonalDetails", "IsDeleted");
            DropColumn("dbo.Obligations", "IsDeleted");
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.Accounts", "IsDeleted");
        }
    }
}
