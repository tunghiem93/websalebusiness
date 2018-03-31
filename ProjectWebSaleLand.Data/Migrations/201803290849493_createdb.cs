namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        PassWord = c.String(),
                        Phone = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        IsActive = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedUser = c.String(),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        PassWord = c.String(),
                        Phone = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        IsSupperAdmin = c.Boolean(nullable: false),
                        IsActive = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedUser = c.String(),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Houses",
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
                        BedRoom = c.Int(nullable: false),
                        LivingRoom = c.Int(nullable: false),
                        BathRoom = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Lands");
            DropTable("dbo.Houses");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
        }
    }
}
