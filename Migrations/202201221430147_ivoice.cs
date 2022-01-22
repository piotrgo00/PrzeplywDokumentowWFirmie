namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ivoice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Orders", t => t.InvoiceId)
                .Index(t => t.InvoiceId);
            
            AddColumn("dbo.Commodities", "Invoice_InvoiceId", c => c.Int());
            AlterColumn("dbo.Orders", "InvoiceId", c => c.Int());
            CreateIndex("dbo.Commodities", "Invoice_InvoiceId");
            AddForeignKey("dbo.Commodities", "Invoice_InvoiceId", "dbo.Invoices", "InvoiceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "InvoiceId", "dbo.Orders");
            DropForeignKey("dbo.Commodities", "Invoice_InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Invoices", new[] { "InvoiceId" });
            DropIndex("dbo.Commodities", new[] { "Invoice_InvoiceId" });
            AlterColumn("dbo.Orders", "InvoiceId", c => c.Int(nullable: false));
            DropColumn("dbo.Commodities", "Invoice_InvoiceId");
            DropTable("dbo.Invoices");
        }
    }
}
