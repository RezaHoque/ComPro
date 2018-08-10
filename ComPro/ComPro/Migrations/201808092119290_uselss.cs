namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uselss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventTitel = c.String(),
                        Approval = c.Boolean(nullable: false),
                        CreatorID = c.String(),
                        CreatorName = c.String(),
                        Activity = c.String(),
                        Description = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        EventEndDate = c.DateTime(nullable: false),
                        Place = c.String(),
                        TotalYes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EventMembers", "EventViewModel_Id", c => c.Int());
            AddColumn("dbo.SiteImages", "EventViewModel_Id", c => c.Int());
            CreateIndex("dbo.EventMembers", "EventViewModel_Id");
            CreateIndex("dbo.SiteImages", "EventViewModel_Id");
            AddForeignKey("dbo.SiteImages", "EventViewModel_Id", "dbo.EventViewModels", "Id");
            AddForeignKey("dbo.EventMembers", "EventViewModel_Id", "dbo.EventViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventMembers", "EventViewModel_Id", "dbo.EventViewModels");
            DropForeignKey("dbo.SiteImages", "EventViewModel_Id", "dbo.EventViewModels");
            DropIndex("dbo.SiteImages", new[] { "EventViewModel_Id" });
            DropIndex("dbo.EventMembers", new[] { "EventViewModel_Id" });
            DropColumn("dbo.SiteImages", "EventViewModel_Id");
            DropColumn("dbo.EventMembers", "EventViewModel_Id");
            DropTable("dbo.EventViewModels");
        }
    }
}
