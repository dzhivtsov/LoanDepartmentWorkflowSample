namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUnemployedColumnToEmploymentDataTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentDatas", "IsUnemployed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Addresses", "CaseNumber", c => c.Int());
            AlterColumn("dbo.EmploymentDatas", "HireDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EmploymentDatas", "Salary", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmploymentDatas", "Salary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.EmploymentDatas", "HireDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Addresses", "CaseNumber", c => c.Int(nullable: false));
            DropColumn("dbo.EmploymentDatas", "IsUnemployed");
        }
    }
}
