namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Buyer_FirmId", "dbo.Firms");
            DropIndex("dbo.Orders", new[] { "Buyer_FirmId" });
            RenameColumn(table: "dbo.Orders", name: "Buyer_FirmId", newName: "FirmId");
            AlterColumn("dbo.Orders", "FirmId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "FirmId");
            AddForeignKey("dbo.Orders", "FirmId", "dbo.Firms", "FirmId", cascadeDelete: true);
            DropColumn("dbo.Orders", "BuyerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "BuyerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orders", "FirmId", "dbo.Firms");
            DropIndex("dbo.Orders", new[] { "FirmId" });
            AlterColumn("dbo.Orders", "FirmId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "FirmId", newName: "Buyer_FirmId");
            CreateIndex("dbo.Orders", "Buyer_FirmId");
            AddForeignKey("dbo.Orders", "Buyer_FirmId", "dbo.Firms", "FirmId");
        }
    }
}
