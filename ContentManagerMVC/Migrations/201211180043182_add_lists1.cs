namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_lists1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Address");
        }
    }
}
