namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanPayment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fine = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        TakenLoan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TakenLoans", t => t.TakenLoan_Id)
                .Index(t => t.TakenLoan_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoanPayments", "TakenLoan_Id", "dbo.TakenLoans");
            DropIndex("dbo.LoanPayments", new[] { "TakenLoan_Id" });
            DropTable("dbo.LoanPayments");
        }
    }
}
