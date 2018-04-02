namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSegment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Segment", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Segment");
        }
    }
}
