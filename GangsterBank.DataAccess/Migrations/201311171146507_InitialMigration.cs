namespace GangsterBank.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(nullable: false),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        IsConfirmed = c.Boolean(nullable: false),
                        EmploymentDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        PersonalDetails_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalDetails", t => t.PersonalDetails_Id)
                .Index(t => t.PersonalDetails_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Obligations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        OutstandingAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontlyPayments = c.Decimal(precision: 18, scale: 2),
                        ExpirationDate = c.DateTime(nullable: false),
                        DelayAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.PersonalDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Contacts_Id = c.Int(),
                        EmploymentData_Id = c.Int(),
                        PassportData_Id = c.Int(),
                        RegistrationAddress_Id = c.Int(),
                        ResidentialAddress_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.Contacts_Id)
                .ForeignKey("dbo.EmploymentDatas", t => t.EmploymentData_Id)
                .ForeignKey("dbo.PassportDatas", t => t.PassportData_Id)
                .ForeignKey("dbo.Addresses", t => t.RegistrationAddress_Id)
                .ForeignKey("dbo.Addresses", t => t.ResidentialAddress_Id)
                .Index(t => t.Contacts_Id)
                .Index(t => t.EmploymentData_Id)
                .Index(t => t.PassportData_Id)
                .Index(t => t.RegistrationAddress_Id)
                .Index(t => t.ResidentialAddress_Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmploymentDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Company = c.String(),
                        Position = c.String(),
                        HireDate = c.DateTime(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PassportDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DigitalCopy = c.Binary(),
                        PersonalNumber = c.String(),
                        PassportNumber = c.String(),
                        Issuer = c.String(),
                        IssueDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostIndex = c.String(),
                        Street = c.String(),
                        HouseNumber = c.Int(nullable: false),
                        CaseNumber = c.Int(nullable: false),
                        FlatNumber = c.Int(nullable: false),
                        City_Id = c.Int(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LoanProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Percentage = c.Double(nullable: false),
                        Maturity = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Properties", "Client_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PersonalDetails_Id", "dbo.PersonalDetails");
            DropForeignKey("dbo.PersonalDetails", "ResidentialAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.PersonalDetails", "RegistrationAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.PersonalDetails", "PassportData_Id", "dbo.PassportDatas");
            DropForeignKey("dbo.PersonalDetails", "EmploymentData_Id", "dbo.EmploymentDatas");
            DropForeignKey("dbo.PersonalDetails", "Contacts_Id", "dbo.Contacts");
            DropForeignKey("dbo.Obligations", "Client_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Accounts", "ClientId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.Properties", new[] { "Client_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "PersonalDetails_Id" });
            DropIndex("dbo.PersonalDetails", new[] { "ResidentialAddress_Id" });
            DropIndex("dbo.PersonalDetails", new[] { "RegistrationAddress_Id" });
            DropIndex("dbo.Addresses", new[] { "Country_Id" });
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            DropIndex("dbo.PersonalDetails", new[] { "PassportData_Id" });
            DropIndex("dbo.PersonalDetails", new[] { "EmploymentData_Id" });
            DropIndex("dbo.PersonalDetails", new[] { "Contacts_Id" });
            DropIndex("dbo.Obligations", new[] { "Client_Id" });
            DropIndex("dbo.Accounts", new[] { "ClientId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LoanProducts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Properties");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
            DropTable("dbo.PassportDatas");
            DropTable("dbo.EmploymentDatas");
            DropTable("dbo.Contacts");
            DropTable("dbo.PersonalDetails");
            DropTable("dbo.Obligations");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Accounts");
        }
    }
}
