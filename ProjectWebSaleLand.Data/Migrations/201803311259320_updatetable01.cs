namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "CreatedUser", c => c.String());
            AddColumn("dbo.Categories", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "ModifiedUser", c => c.String());
            AddColumn("dbo.Categories", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Locations", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Locations", "CreatedUser", c => c.String());
            AddColumn("dbo.Locations", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Locations", "ModifiedUser", c => c.String());
            AddColumn("dbo.Locations", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Customers", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Employees", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "IsActive", c => c.String());
            AlterColumn("dbo.Customers", "IsActive", c => c.String());
            DropColumn("dbo.Products", "IsActive");
            DropColumn("dbo.Locations", "IsActive");
            DropColumn("dbo.Locations", "ModifiedUser");
            DropColumn("dbo.Locations", "ModifiedDate");
            DropColumn("dbo.Locations", "CreatedUser");
            DropColumn("dbo.Locations", "CreatedDate");
            DropColumn("dbo.Categories", "IsActive");
            DropColumn("dbo.Categories", "ModifiedUser");
            DropColumn("dbo.Categories", "ModifiedDate");
            DropColumn("dbo.Categories", "CreatedUser");
            DropColumn("dbo.Categories", "CreatedDate");
        }
    }
}
