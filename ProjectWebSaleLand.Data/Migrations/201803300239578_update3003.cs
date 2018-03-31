namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "CreatedUser", c => c.String());
            AddColumn("dbo.Products", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "ModifiedUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ModifiedUser");
            DropColumn("dbo.Products", "ModifiedDate");
            DropColumn("dbo.Products", "CreatedUser");
            DropColumn("dbo.Products", "CreatedDate");
        }
    }
}
