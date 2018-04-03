namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDb264 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Products", newName: "Houses");
            CreateTable(
                "dbo.Lands",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        LocationID = c.String(),
                        Type = c.Int(nullable: false),
                        Segment = c.Int(nullable: false),
                        Address = c.String(),
                        Phone = c.String(),
                        Length = c.Double(nullable: false),
                        Width = c.Double(nullable: false),
                        Acreage = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Right = c.String(),
                        Description = c.String(),
                        Phone1 = c.String(),
                        Address1 = c.String(),
                        Phone2 = c.String(),
                        Address2 = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedUser = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Houses", "CategoryID");
            DropColumn("dbo.Houses", "ImageURL1");
            DropColumn("dbo.Houses", "ImageURL2");
            DropColumn("dbo.Houses", "ImageURL3");
            DropColumn("dbo.Houses", "ImageURL4");
            DropColumn("dbo.Houses", "ImageURL5");
            DropColumn("dbo.Houses", "ImageURL6");
            DropColumn("dbo.Houses", "ImageURL7");
            DropColumn("dbo.Houses", "ImageURL8");
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedUser = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Houses", "ImageURL8", c => c.String());
            AddColumn("dbo.Houses", "ImageURL7", c => c.String());
            AddColumn("dbo.Houses", "ImageURL6", c => c.String());
            AddColumn("dbo.Houses", "ImageURL5", c => c.String());
            AddColumn("dbo.Houses", "ImageURL4", c => c.String());
            AddColumn("dbo.Houses", "ImageURL3", c => c.String());
            AddColumn("dbo.Houses", "ImageURL2", c => c.String());
            AddColumn("dbo.Houses", "ImageURL1", c => c.String());
            AddColumn("dbo.Houses", "CategoryID", c => c.String());
            DropTable("dbo.Lands");
            RenameTable(name: "dbo.Houses", newName: "Products");
        }
    }
}
