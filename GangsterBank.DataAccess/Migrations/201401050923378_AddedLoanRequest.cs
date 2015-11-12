namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanRequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Monthes = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Client_Id = c.Int(),
                        LoanProduct_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Client_Id)
                .ForeignKey("dbo.LoanProducts", t => t.LoanProduct_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.LoanProduct_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoanRequests", "LoanProduct_Id", "dbo.LoanProducts");
            DropForeignKey("dbo.LoanRequests", "Client_Id", "dbo.Users");
            DropIndex("dbo.LoanRequests", new[] { "LoanProduct_Id" });
            DropIndex("dbo.LoanRequests", new[] { "Client_Id" });
            DropTable("dbo.LoanRequests");
        }
    }
}
