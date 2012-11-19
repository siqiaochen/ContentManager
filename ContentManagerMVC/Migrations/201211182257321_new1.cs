namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlayerSchedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        schedule_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Schedules", t => t.schedule_ID)
                .Index(t => t.schedule_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PlayerSchedules", new[] { "schedule_ID" });
            DropForeignKey("dbo.PlayerSchedules", "schedule_ID", "dbo.Schedules");
            DropTable("dbo.PlayerSchedules");
        }
    }
}
