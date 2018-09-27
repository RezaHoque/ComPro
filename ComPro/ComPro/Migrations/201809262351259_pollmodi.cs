namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pollmodi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PerticipentModels", "ActivityId", c => c.Int(nullable: false));
            AddColumn("dbo.PollingAndSyrvayModels", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PollingAndSyrvayModels", "Description");
            DropColumn("dbo.PerticipentModels", "ActivityId");
        }
    }
}
