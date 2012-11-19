namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MediaContents", "Schedule_ID", "dbo.Schedules");
            DropIndex("dbo.MediaContents", new[] { "Schedule_ID" });
            CreateTable(
                "dbo.ScheduledItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content_ID = c.Int(),
                        Schedule_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaContents", t => t.Content_ID)
                .ForeignKey("dbo.Schedules", t => t.Schedule_ID)
                .Index(t => t.Content_ID)
                .Index(t => t.Schedule_ID);
            
            DropColumn("dbo.MediaContents", "Schedule_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MediaContents", "Schedule_ID", c => c.Int());
            DropIndex("dbo.ScheduledItems", new[] { "Schedule_ID" });
            DropIndex("dbo.ScheduledItems", new[] { "Content_ID" });
            DropForeignKey("dbo.ScheduledItems", "Schedule_ID", "dbo.Schedules");
            DropForeignKey("dbo.ScheduledItems", "Content_ID", "dbo.MediaContents");
            DropTable("dbo.ScheduledItems");
            CreateIndex("dbo.MediaContents", "Schedule_ID");
            AddForeignKey("dbo.MediaContents", "Schedule_ID", "dbo.Schedules", "ID");
        }
    }
}
