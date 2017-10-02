namespace PRS_web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendors", "Vendor_ID", c => c.Int());
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Products", "PhotoPath", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Products", "VendorId");
            CreateIndex("dbo.Vendors", "Vendor_ID");
            AddForeignKey("dbo.Vendors", "Vendor_ID", "dbo.Vendors", "ID");
            AddForeignKey("dbo.Products", "VendorId", "dbo.Vendors", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Vendors", "Vendor_ID", "dbo.Vendors");
            DropIndex("dbo.Vendors", new[] { "Vendor_ID" });
            DropIndex("dbo.Products", new[] { "VendorId" });
            AlterColumn("dbo.Products", "PhotoPath", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Vendors", "Vendor_ID");
        }
    }
}
