namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeBirthdayNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonalDetails", "BirthDate", c => c.DateTime(nullable:true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonalDetails", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
