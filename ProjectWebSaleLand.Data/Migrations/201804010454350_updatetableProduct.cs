namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Type");
        }
    }
}
