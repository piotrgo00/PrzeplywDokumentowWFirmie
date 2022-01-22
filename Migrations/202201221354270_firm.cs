namespace PrzeplywDokumentowWFirmie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Firms",
                c => new
                    {
                        FirmId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.FirmId);
            
            AddColumn("dbo.Orders", "Buyer_FirmId", c => c.Int());
            CreateIndex("dbo.Orders", "Buyer_FirmId");
            AddForeignKey("dbo.Orders", "Buyer_FirmId", "dbo.Firms", "FirmId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Buyer_FirmId", "dbo.Firms");
            DropIndex("dbo.Orders", new[] { "Buyer_FirmId" });
            DropColumn("dbo.Orders", "Buyer_FirmId");
            DropTable("dbo.Firms");
        }
    }
}
