namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableCus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "IsAdmin");
        }
    }
}
