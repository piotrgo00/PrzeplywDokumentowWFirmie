namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirmCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Firms", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Firms", "Country");
        }
    }
}
