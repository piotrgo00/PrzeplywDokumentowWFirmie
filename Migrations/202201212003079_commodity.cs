namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commodity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commodities",
                c => new
                    {
                        CommodityId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        ElectronicItemId = c.Int(),
                        FurnitureItemId = c.Int(),
                        ConsumableItemId = c.Int(),
                    })
                .PrimaryKey(t => t.CommodityId)
                .ForeignKey("dbo.ConsumableItems", t => t.ConsumableItemId)
                .ForeignKey("dbo.ElectronicItems", t => t.ElectronicItemId)
                .ForeignKey("dbo.FurnitureItems", t => t.FurnitureItemId)
                .Index(t => t.ElectronicItemId)
                .Index(t => t.FurnitureItemId)
                .Index(t => t.ConsumableItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Commodities", "FurnitureItemId", "dbo.FurnitureItems");
            DropForeignKey("dbo.Commodities", "ElectronicItemId", "dbo.ElectronicItems");
            DropForeignKey("dbo.Commodities", "ConsumableItemId", "dbo.ConsumableItems");
            DropIndex("dbo.Commodities", new[] { "ConsumableItemId" });
            DropIndex("dbo.Commodities", new[] { "FurnitureItemId" });
            DropIndex("dbo.Commodities", new[] { "ElectronicItemId" });
            DropTable("dbo.Commodities");
        }
    }
}
