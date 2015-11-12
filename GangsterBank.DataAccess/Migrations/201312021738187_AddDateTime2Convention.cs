namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateTime2Convention : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "EmploymentDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Obligations", "ExpirationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PersonalDetails", "BirthDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EmploymentDatas", "HireDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PassportDatas", "IssueDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PassportDatas", "ExpirationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.LoanProducts", "Maturity", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoanProducts", "Maturity", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PassportDatas", "ExpirationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PassportDatas", "IssueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmploymentDatas", "HireDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PersonalDetails", "BirthDate", c => c.DateTime());
            AlterColumn("dbo.Obligations", "ExpirationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "EmploymentDate", c => c.DateTime());
        }
    }
}
