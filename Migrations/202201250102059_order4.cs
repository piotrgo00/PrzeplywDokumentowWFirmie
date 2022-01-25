namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "StateName", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "StateName");
        }
    }
}
