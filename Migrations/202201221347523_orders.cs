namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        BuyerId = c.Int(nullable: false),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            AddColumn("dbo.Commodities", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.Commodities", "Order_OrderId");
            AddForeignKey("dbo.Commodities", "Order_OrderId", "dbo.Orders", "OrderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Commodities", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.Commodities", new[] { "Order_OrderId" });
            DropColumn("dbo.Commodities", "Order_OrderId");
            DropTable("dbo.Orders");
        }
    }
}
