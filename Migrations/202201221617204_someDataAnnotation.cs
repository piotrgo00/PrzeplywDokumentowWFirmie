namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someDataAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ConsumableItems", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ElectronicItems", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.FurnitureItems", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FurnitureItems", "Name", c => c.String());
            AlterColumn("dbo.ElectronicItems", "Name", c => c.String());
            AlterColumn("dbo.ConsumableItems", "Name", c => c.String());
        }
    }
}
