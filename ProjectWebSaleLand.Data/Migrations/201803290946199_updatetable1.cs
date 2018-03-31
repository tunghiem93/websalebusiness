namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CategoryID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CategoryID");
        }
    }
}
