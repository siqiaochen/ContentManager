namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_lists3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "Play_ID", "dbo.Schedules");
            DropIndex("dbo.Players", new[] { "Play_ID" });
            DropColumn("dbo.Players", "Play_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "Play_ID", c => c.Int());
            CreateIndex("dbo.Players", "Play_ID");
            AddForeignKey("dbo.Players", "Play_ID", "dbo.Schedules", "ID");
        }
    }
}
