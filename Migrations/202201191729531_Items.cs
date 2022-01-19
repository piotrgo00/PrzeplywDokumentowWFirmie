namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Items : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConsumableItems",
                c => new
                    {
                        ConsumableItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConsumableItemId);
            
            CreateTable(
                "dbo.ElectronicItems",
                c => new
                    {
                        ElectronicItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ElectronicItemId);
            
            CreateTable(
                "dbo.FurnitureItems",
                c => new
                    {
                        FurnitureItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Condition = c.String(),
                        IsUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FurnitureItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FurnitureItems");
            DropTable("dbo.ElectronicItems");
            DropTable("dbo.ConsumableItems");
        }
    }
}
