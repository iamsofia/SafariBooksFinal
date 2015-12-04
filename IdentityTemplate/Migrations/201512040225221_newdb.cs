namespace IdentityTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        SKU = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AuthorFirst = c.String(),
                        AuthorLast = c.String(),
                        Genre = c.String(),
                        PublicationDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceLastPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Inventory = c.Int(nullable: false),
                        ReorderPoint = c.Int(nullable: false),
                        Discontinued = c.Boolean(nullable: false),
                        Order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.SKU)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        SKU = c.Int(nullable: false),
                        CustomerReview = c.String(maxLength: 100),
                        ReviewApproval = c.Boolean(nullable: false),
                        CustomerRating = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Books", t => t.SKU, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.SKU)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(),
                        MI = c.String(),
                        LName = c.String(),
                        Password = c.String(),
                        Address = c.String(),
                        Zip = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CreditCardID = c.Int(nullable: false, identity: true),
                        CardName = c.String(nullable: false),
                        CardNumber = c.String(nullable: false),
                        CardType = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CreditCardID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        SKU = c.Int(nullable: false),
                        CouponID = c.String(maxLength: 20),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Books", t => t.SKU, cascadeDelete: true)
                .ForeignKey("dbo.Coupons", t => t.CouponID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.SKU)
                .Index(t => t.CouponID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        CouponID = c.String(nullable: false, maxLength: 20),
                        Active = c.Boolean(nullable: false),
                        FreeShipIf = c.Boolean(nullable: false),
                        FreeShipAll = c.Boolean(nullable: false),
                        DiscountTotal = c.Boolean(nullable: false),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        OrderThreshold = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CouponID);
            
            CreateTable(
                "dbo.Reorders",
                c => new
                    {
                        ReorderID = c.Int(nullable: false, identity: true),
                        SKU = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReorderID)
                .ForeignKey("dbo.Books", t => t.SKU, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.SKU)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.RegisterViewModels",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        FName = c.String(nullable: false),
                        MI = c.String(),
                        LName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zip = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        IsManager = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reorders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reorders", "SKU", "dbo.Books");
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "CouponID", "dbo.Coupons");
            DropForeignKey("dbo.Books", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "SKU", "dbo.Books");
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CreditCards", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "SKU", "dbo.Books");
            DropIndex("dbo.Reorders", new[] { "User_Id" });
            DropIndex("dbo.Reorders", new[] { "SKU" });
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "CouponID" });
            DropIndex("dbo.Orders", new[] { "SKU" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.CreditCards", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "SKU" });
            DropIndex("dbo.Books", new[] { "Order_OrderID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.RegisterViewModels");
            DropTable("dbo.Reorders");
            DropTable("dbo.Coupons");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.CreditCards");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Reviews");
            DropTable("dbo.Books");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
