namespace PRS_web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedgetaddchangeremovetoPurchaserequestlineitems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.PurchaseRequests", "UserId", "dbo.Users");
            DropIndex("dbo.Products", new[] { "VendorId" });
            DropIndex("dbo.PurchaseRequests", new[] { "UserId" });
            RenameColumn(table: "dbo.Products", name: "VendorId", newName: "vendor_ID");
            RenameColumn(table: "dbo.PurchaseRequests", name: "UserId", newName: "User_id");
            AddColumn("dbo.Products", "VendorId_ID", c => c.Int());
            AddColumn("dbo.PurchaseRequests", "UserId_id", c => c.Int());
            AlterColumn("dbo.Products", "vendor_ID", c => c.Int());
            AlterColumn("dbo.Products", "PhotoPath", c => c.String(maxLength: 50));
            AlterColumn("dbo.PurchaseRequests", "User_id", c => c.Int());
            CreateIndex("dbo.Products", "vendor_ID");
            CreateIndex("dbo.Products", "VendorId_ID");
            CreateIndex("dbo.Vendors", "Code", unique: true);
            CreateIndex("dbo.PurchaseRequests", "User_id");
            CreateIndex("dbo.PurchaseRequests", "UserId_id");
            AddForeignKey("dbo.Products", "VendorId_ID", "dbo.Vendors", "ID");
            AddForeignKey("dbo.PurchaseRequests", "UserId_id", "dbo.Users", "id");
            AddForeignKey("dbo.Products", "vendor_ID", "dbo.Vendors", "ID");
            AddForeignKey("dbo.PurchaseRequests", "User_id", "dbo.Users", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseRequests", "User_id", "dbo.Users");
            DropForeignKey("dbo.Products", "vendor_ID", "dbo.Vendors");
            DropForeignKey("dbo.PurchaseRequests", "UserId_id", "dbo.Users");
            DropForeignKey("dbo.Products", "VendorId_ID", "dbo.Vendors");
            DropIndex("dbo.PurchaseRequests", new[] { "UserId_id" });
            DropIndex("dbo.PurchaseRequests", new[] { "User_id" });
            DropIndex("dbo.Vendors", new[] { "Code" });
            DropIndex("dbo.Products", new[] { "VendorId_ID" });
            DropIndex("dbo.Products", new[] { "vendor_ID" });
            AlterColumn("dbo.PurchaseRequests", "User_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "PhotoPath", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "vendor_ID", c => c.Int(nullable: false));
            DropColumn("dbo.PurchaseRequests", "UserId_id");
            DropColumn("dbo.Products", "VendorId_ID");
            RenameColumn(table: "dbo.PurchaseRequests", name: "User_id", newName: "UserId");
            RenameColumn(table: "dbo.Products", name: "vendor_ID", newName: "VendorId");
            CreateIndex("dbo.PurchaseRequests", "UserId");
            CreateIndex("dbo.Products", "VendorId");
            AddForeignKey("dbo.PurchaseRequests", "UserId", "dbo.Users", "id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "VendorId", "dbo.Vendors", "ID", cascadeDelete: true);
        }
    }
}
