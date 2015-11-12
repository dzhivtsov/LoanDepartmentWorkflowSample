namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendedTakenLoan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TakenLoans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TakeDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaturityInMonth = c.Int(nullable: false),
                        ProductLoan_Id = c.Int(),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoanProducts", t => t.ProductLoan_Id)
                .ForeignKey("dbo.Users", t => t.Client_Id)
                .Index(t => t.ProductLoan_Id)
                .Index(t => t.Client_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TakenLoans", "Client_Id", "dbo.Users");
            DropForeignKey("dbo.TakenLoans", "ProductLoan_Id", "dbo.LoanProducts");
            DropIndex("dbo.TakenLoans", new[] { "Client_Id" });
            DropIndex("dbo.TakenLoans", new[] { "ProductLoan_Id" });
            DropTable("dbo.TakenLoans");
        }
    }
}
