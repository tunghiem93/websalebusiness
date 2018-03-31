namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Products", "LocationID", c => c.String());
            DropColumn("dbo.Products", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Location", c => c.String());
            DropColumn("dbo.Products", "LocationID");
            DropTable("dbo.Locations");
        }
    }
}
