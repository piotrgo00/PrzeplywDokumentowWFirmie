namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsumableItems", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.ElectronicItems", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.FurnitureItems", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FurnitureItems", "Price");
            DropColumn("dbo.ElectronicItems", "Price");
            DropColumn("dbo.ConsumableItems", "Price");
        }
    }
}
