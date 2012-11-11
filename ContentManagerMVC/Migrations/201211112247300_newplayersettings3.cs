namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newplayersettings3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaContents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ContentType = c.String(),
                        Duration = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MediaContents");
        }
    }
}
