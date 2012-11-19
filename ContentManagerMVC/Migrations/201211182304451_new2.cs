namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "Player_ID", "dbo.Players");
            DropIndex("dbo.Schedules", new[] { "Player_ID" });
            AddColumn("dbo.PlayerSchedules", "Player_ID", c => c.Int());
            AddForeignKey("dbo.PlayerSchedules", "Player_ID", "dbo.Players", "ID");
            CreateIndex("dbo.PlayerSchedules", "Player_ID");
            DropColumn("dbo.Schedules", "Player_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "Player_ID", c => c.Int());
            DropIndex("dbo.PlayerSchedules", new[] { "Player_ID" });
            DropForeignKey("dbo.PlayerSchedules", "Player_ID", "dbo.Players");
            DropColumn("dbo.PlayerSchedules", "Player_ID");
            CreateIndex("dbo.Schedules", "Player_ID");
            AddForeignKey("dbo.Schedules", "Player_ID", "dbo.Players", "ID");
        }
    }
}
