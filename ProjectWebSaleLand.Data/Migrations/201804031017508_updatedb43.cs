namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb43 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Houses", newName: "Products");
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        ProductID = c.String(),
                        ImageUrL = c.String(),
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
            
            DropTable("dbo.Images");
            RenameTable(name: "dbo.Products", newName: "Houses");
        }
    }
}
