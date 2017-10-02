namespace PRS_web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpurchaserequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vendors", "Vendor_ID", "dbo.Vendors");
            DropIndex("dbo.Vendors", new[] { "Vendor_ID" });
            CreateTable(
                "dbo.PurchaseRequestLineItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseRequestId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseRequests", t => t.PurchaseRequestId, cascadeDelete: true)
                .Index(t => t.PurchaseRequestId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.PurchaseRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 30),
                        Justification = c.String(nullable: false, maxLength: 30),
                        DateNeeded = c.DateTime(nullable: false),
                        DeliveryMode = c.String(nullable: false, maxLength: 30),
                        Status = c.String(nullable: false, maxLength: 30),
                        Total = c.Double(nullable: false),
                        SubmittedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Vendors", "Vendor_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vendors", "Vendor_ID", c => c.Int());
            DropForeignKey("dbo.PurchaseRequestLineItems", "PurchaseRequestId", "dbo.PurchaseRequests");
            DropForeignKey("dbo.PurchaseRequests", "UserId", "dbo.Users");
            DropForeignKey("dbo.PurchaseRequestLineItems", "ProductId", "dbo.Products");
            DropIndex("dbo.PurchaseRequests", new[] { "UserId" });
            DropIndex("dbo.PurchaseRequestLineItems", new[] { "ProductId" });
            DropIndex("dbo.PurchaseRequestLineItems", new[] { "PurchaseRequestId" });
            DropTable("dbo.PurchaseRequests");
            DropTable("dbo.PurchaseRequestLineItems");
            CreateIndex("dbo.Vendors", "Vendor_ID");
            AddForeignKey("dbo.Vendors", "Vendor_ID", "dbo.Vendors", "ID");
        }
    }
}
