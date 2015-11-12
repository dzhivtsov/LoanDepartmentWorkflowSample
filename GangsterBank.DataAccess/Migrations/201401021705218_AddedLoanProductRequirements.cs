namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanProductRequirements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanProductRequirements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinWorkOnLastJobInMonths = c.Int(nullable: false),
                        MinSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NeedEarningsRecord = c.Boolean(nullable: false),
                        NeedGuarantors = c.Boolean(nullable: false),
                        GuarantorsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.LoanProducts", "Requirements_Id", c => c.Int());
            CreateIndex("dbo.LoanProducts", "Requirements_Id");
            AddForeignKey("dbo.LoanProducts", "Requirements_Id", "dbo.LoanProductRequirements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoanProducts", "Requirements_Id", "dbo.LoanProductRequirements");
            DropIndex("dbo.LoanProducts", new[] { "Requirements_Id" });
            DropColumn("dbo.LoanProducts", "Requirements_Id");
            DropTable("dbo.LoanProductRequirements");
        }
    }
}
