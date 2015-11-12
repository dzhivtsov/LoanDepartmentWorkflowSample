namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApproversForLoanRequirements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentityRoleEntityLoanProductRequirements",
                c => new
                    {
                        IdentityRoleEntity_Id = c.Int(nullable: false),
                        LoanProductRequirements_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdentityRoleEntity_Id, t.LoanProductRequirements_Id })
                .ForeignKey("dbo.Roles", t => t.IdentityRoleEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.LoanProductRequirements", t => t.LoanProductRequirements_Id, cascadeDelete: true)
                .Index(t => t.IdentityRoleEntity_Id)
                .Index(t => t.LoanProductRequirements_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityRoleEntityLoanProductRequirements", "LoanProductRequirements_Id", "dbo.LoanProductRequirements");
            DropForeignKey("dbo.IdentityRoleEntityLoanProductRequirements", "IdentityRoleEntity_Id", "dbo.Roles");
            DropIndex("dbo.IdentityRoleEntityLoanProductRequirements", new[] { "LoanProductRequirements_Id" });
            DropIndex("dbo.IdentityRoleEntityLoanProductRequirements", new[] { "IdentityRoleEntity_Id" });
            DropTable("dbo.IdentityRoleEntityLoanProductRequirements");
        }
    }
}
