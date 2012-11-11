namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newplayersettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Description");
            DropColumn("dbo.Players", "Status");
        }
    }
}
