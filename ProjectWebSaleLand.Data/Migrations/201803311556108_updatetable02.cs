namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Address", c => c.String());
            AddColumn("dbo.Customers", "MaritalStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "Street", c => c.String());
            AddColumn("dbo.Customers", "City", c => c.String());
            AddColumn("dbo.Customers", "Country", c => c.String());
            AddColumn("dbo.Customers", "Description", c => c.String());
            AddColumn("dbo.Employees", "MaritalStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "Street", c => c.String());
            AddColumn("dbo.Employees", "City", c => c.String());
            AddColumn("dbo.Employees", "Country", c => c.String());
            AddColumn("dbo.Employees", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Description");
            DropColumn("dbo.Employees", "Country");
            DropColumn("dbo.Employees", "City");
            DropColumn("dbo.Employees", "Street");
            DropColumn("dbo.Employees", "MaritalStatus");
            DropColumn("dbo.Customers", "Description");
            DropColumn("dbo.Customers", "Country");
            DropColumn("dbo.Customers", "City");
            DropColumn("dbo.Customers", "Street");
            DropColumn("dbo.Customers", "MaritalStatus");
            DropColumn("dbo.Customers", "Address");
        }
    }
}
