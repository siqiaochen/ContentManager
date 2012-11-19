namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "Mon", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Tue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Wed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Thr", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Fri", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Sat", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Sun", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "Sun");
            DropColumn("dbo.Schedules", "Sat");
            DropColumn("dbo.Schedules", "Fri");
            DropColumn("dbo.Schedules", "Thr");
            DropColumn("dbo.Schedules", "Wed");
            DropColumn("dbo.Schedules", "Tue");
            DropColumn("dbo.Schedules", "Mon");
        }
    }
}
