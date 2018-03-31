namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Houses", newName: "Products");
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Lands");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Lands",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        Length = c.Double(nullable: false),
                        Width = c.Double(nullable: false),
                        Acreage = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Location = c.String(),
                        Right = c.String(),
                        Description = c.String(),
                        Phone1 = c.String(),
                        Address1 = c.String(),
                        Phone2 = c.String(),
                        Address2 = c.String(),
                        ImageURL1 = c.String(),
                        ImageURL2 = c.String(),
                        ImageURL3 = c.String(),
                        ImageURL4 = c.String(),
                        ImageURL5 = c.String(),
                        ImageURL6 = c.String(),
                        ImageURL7 = c.String(),
                        ImageURL8 = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Categories");
            RenameTable(name: "dbo.Products", newName: "Houses");
        }
    }
}
