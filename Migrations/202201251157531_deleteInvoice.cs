namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteInvoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "InvoiceId", "dbo.Orders");
            DropIndex("dbo.Invoices", new[] { "InvoiceId" });
            AddColumn("dbo.Orders", "WarehouseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "WarehouseId");
            AddForeignKey("dbo.Orders", "WarehouseId", "dbo.Warehouses", "WarehouseId", cascadeDelete: true);
            DropColumn("dbo.Orders", "InvoiceId");
            DropTable("dbo.Invoices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId);
            
            AddColumn("dbo.Orders", "InvoiceId", c => c.Int());
            DropForeignKey("dbo.Orders", "WarehouseId", "dbo.Warehouses");
            DropIndex("dbo.Orders", new[] { "WarehouseId" });
            DropColumn("dbo.Orders", "WarehouseId");
            CreateIndex("dbo.Invoices", "InvoiceId");
            AddForeignKey("dbo.Invoices", "InvoiceId", "dbo.Orders", "OrderId");
        }
    }
}
