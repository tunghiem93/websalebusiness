namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Phone");
        }
    }
}
