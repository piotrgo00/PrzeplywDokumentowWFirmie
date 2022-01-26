namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class warehouseAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouses", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Warehouses", "Address");
        }
    }
}
