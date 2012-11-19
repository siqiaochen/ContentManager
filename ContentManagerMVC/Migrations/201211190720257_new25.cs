namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MediaContents", "Path", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MediaContents", "Path");
        }
    }
}
