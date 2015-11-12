namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTakenLoanStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TakenLoans", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TakenLoans", "Status");
        }
    }
}
