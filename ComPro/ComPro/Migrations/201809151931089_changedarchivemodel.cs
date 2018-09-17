namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedarchivemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArchiveModels", "EventId", "dbo.EventModels");
            DropIndex("dbo.ArchiveModels", new[] { "EventId" });
            DropColumn("dbo.ArchiveModels", "EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArchiveModels", "EventId", c => c.Int());
            CreateIndex("dbo.ArchiveModels", "EventId");
            AddForeignKey("dbo.ArchiveModels", "EventId", "dbo.EventModels", "EventId");
        }
    }
}
