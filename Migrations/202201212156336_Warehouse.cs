namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Warehouse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        WarehouseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.WarehouseId);
            
            AddColumn("dbo.Commodities", "WarehouseId", c => c.Int());
            CreateIndex("dbo.Commodities", "WarehouseId");
            AddForeignKey("dbo.Commodities", "WarehouseId", "dbo.Warehouses", "WarehouseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Commodities", "WarehouseId", "dbo.Warehouses");
            DropIndex("dbo.Commodities", new[] { "WarehouseId" });
            DropColumn("dbo.Commodities", "WarehouseId");
            DropTable("dbo.Warehouses");
        }
    }
}
