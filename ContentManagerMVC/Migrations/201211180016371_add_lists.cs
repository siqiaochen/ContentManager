namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_lists : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Play_ID", c => c.Int());
            AddColumn("dbo.MediaContents", "Schedule_ID", c => c.Int());
            AddColumn("dbo.Schedules", "Player_ID", c => c.Int());
            AddForeignKey("dbo.Players", "Play_ID", "dbo.Schedules", "ID");
            AddForeignKey("dbo.Schedules", "Player_ID", "dbo.Players", "ID");
            AddForeignKey("dbo.MediaContents", "Schedule_ID", "dbo.Schedules", "ID");
            CreateIndex("dbo.Players", "Play_ID");
            CreateIndex("dbo.Schedules", "Player_ID");
            CreateIndex("dbo.MediaContents", "Schedule_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MediaContents", new[] { "Schedule_ID" });
            DropIndex("dbo.Schedules", new[] { "Player_ID" });
            DropIndex("dbo.Players", new[] { "Play_ID" });
            DropForeignKey("dbo.MediaContents", "Schedule_ID", "dbo.Schedules");
            DropForeignKey("dbo.Schedules", "Player_ID", "dbo.Players");
            DropForeignKey("dbo.Players", "Play_ID", "dbo.Schedules");
            DropColumn("dbo.Schedules", "Player_ID");
            DropColumn("dbo.MediaContents", "Schedule_ID");
            DropColumn("dbo.Players", "Play_ID");
        }
    }
}
