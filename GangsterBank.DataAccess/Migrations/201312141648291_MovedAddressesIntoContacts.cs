namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovedAddressesIntoContacts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonalDetails", "RegistrationAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.PersonalDetails", "ResidentialAddress_Id", "dbo.Addresses");
            DropIndex("dbo.PersonalDetails", new[] { "RegistrationAddress_Id" });
            DropIndex("dbo.PersonalDetails", new[] { "ResidentialAddress_Id" });
            AddColumn("dbo.Contacts", "RegistrationAddress_Id", c => c.Int());
            AddColumn("dbo.Contacts", "ResidentialAddress_Id", c => c.Int());
            AlterColumn("dbo.PersonalDetails", "Gender", c => c.Int());
            CreateIndex("dbo.Contacts", "RegistrationAddress_Id");
            CreateIndex("dbo.Contacts", "ResidentialAddress_Id");
            AddForeignKey("dbo.Contacts", "RegistrationAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Contacts", "ResidentialAddress_Id", "dbo.Addresses", "Id");
            DropColumn("dbo.PersonalDetails", "RegistrationAddress_Id");
            DropColumn("dbo.PersonalDetails", "ResidentialAddress_Id");
            DropColumn("dbo.Contacts", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "Email", c => c.String());
            AddColumn("dbo.PersonalDetails", "ResidentialAddress_Id", c => c.Int());
            AddColumn("dbo.PersonalDetails", "RegistrationAddress_Id", c => c.Int());
            DropForeignKey("dbo.Contacts", "ResidentialAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Contacts", "RegistrationAddress_Id", "dbo.Addresses");
            DropIndex("dbo.Contacts", new[] { "ResidentialAddress_Id" });
            DropIndex("dbo.Contacts", new[] { "RegistrationAddress_Id" });
            AlterColumn("dbo.PersonalDetails", "Gender", c => c.Int(nullable: false));
            DropColumn("dbo.Contacts", "ResidentialAddress_Id");
            DropColumn("dbo.Contacts", "RegistrationAddress_Id");
            CreateIndex("dbo.PersonalDetails", "ResidentialAddress_Id");
            CreateIndex("dbo.PersonalDetails", "RegistrationAddress_Id");
            AddForeignKey("dbo.PersonalDetails", "ResidentialAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.PersonalDetails", "RegistrationAddress_Id", "dbo.Addresses", "Id");
        }
    }
}
