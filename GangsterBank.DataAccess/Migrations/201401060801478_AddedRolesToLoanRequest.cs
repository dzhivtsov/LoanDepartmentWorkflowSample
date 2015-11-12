namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRolesToLoanRequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanRequestIdentityRoleEntities",
                c => new
                    {
                        LoanRequest_Id = c.Int(nullable: false),
                        IdentityRoleEntity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoanRequest_Id, t.IdentityRoleEntity_Id })
                .ForeignKey("dbo.LoanRequests", t => t.LoanRequest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.IdentityRoleEntity_Id, cascadeDelete: true)
                .Index(t => t.LoanRequest_Id)
                .Index(t => t.IdentityRoleEntity_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoanRequestIdentityRoleEntities", "IdentityRoleEntity_Id", "dbo.Roles");
            DropForeignKey("dbo.LoanRequestIdentityRoleEntities", "LoanRequest_Id", "dbo.LoanRequests");
            DropIndex("dbo.LoanRequestIdentityRoleEntities", new[] { "IdentityRoleEntity_Id" });
            DropIndex("dbo.LoanRequestIdentityRoleEntities", new[] { "LoanRequest_Id" });
            DropTable("dbo.LoanRequestIdentityRoleEntities");
        }
    }
}
