namespace ProjectWebSaleLand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb44213 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FirstName", c => c.String());
            AddColumn("dbo.Customers", "LastName", c => c.String());
            AddColumn("dbo.Customers", "Company", c => c.String());
            AddColumn("dbo.Customers", "ZipCode", c => c.String());
            AddColumn("dbo.Customers", "WebSite", c => c.String());
            AddColumn("dbo.Employees", "FirstName", c => c.String());
            AddColumn("dbo.Employees", "LastName", c => c.String());
            AddColumn("dbo.Employees", "Company", c => c.String());
            AddColumn("dbo.Employees", "ZipCode", c => c.String());
            AddColumn("dbo.Employees", "WebSite", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "WebSite");
            DropColumn("dbo.Employees", "ZipCode");
            DropColumn("dbo.Employees", "Company");
            DropColumn("dbo.Employees", "LastName");
            DropColumn("dbo.Employees", "FirstName");
            DropColumn("dbo.Customers", "WebSite");
            DropColumn("dbo.Customers", "ZipCode");
            DropColumn("dbo.Customers", "Company");
            DropColumn("dbo.Customers", "LastName");
            DropColumn("dbo.Customers", "FirstName");
        }
    }
}
