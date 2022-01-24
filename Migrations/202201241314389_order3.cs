namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Commodities", "Invoice_InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Commodities", new[] { "Invoice_InvoiceId" });
            RenameColumn(table: "dbo.Commodities", name: "Order_OrderId", newName: "OrderId");
            RenameIndex(table: "dbo.Commodities", name: "IX_Order_OrderId", newName: "IX_OrderId");
            AddColumn("dbo.Orders", "Name", c => c.String());
            DropColumn("dbo.Commodities", "Invoice_InvoiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Commodities", "Invoice_InvoiceId", c => c.Int());
            DropColumn("dbo.Orders", "Name");
            RenameIndex(table: "dbo.Commodities", name: "IX_OrderId", newName: "IX_Order_OrderId");
            RenameColumn(table: "dbo.Commodities", name: "OrderId", newName: "Order_OrderId");
            CreateIndex("dbo.Commodities", "Invoice_InvoiceId");
            AddForeignKey("dbo.Commodities", "Invoice_InvoiceId", "dbo.Invoices", "InvoiceId");
        }
    }
}
