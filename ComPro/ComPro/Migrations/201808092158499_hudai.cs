namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hudai : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SiteImages", "EventViewModel_Id", "dbo.EventViewModels");
            DropForeignKey("dbo.EventMembers", "EventViewModel_Id", "dbo.EventViewModels");
            DropIndex("dbo.EventMembers", new[] { "EventViewModel_Id" });
            DropIndex("dbo.SiteImages", new[] { "EventViewModel_Id" });
            DropColumn("dbo.EventMembers", "EventViewModel_Id");
            DropColumn("dbo.SiteImages", "EventViewModel_Id");
            DropTable("dbo.EventViewModels");
            DropTable("dbo.User_Approval_Model");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.User_Approval_Model",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            AddColumn("dbo.SiteImages", "EventViewModel_Id", c => c.Int());
            AddColumn("dbo.EventMembers", "EventViewModel_Id", c => c.Int());
            CreateIndex("dbo.SiteImages", "EventViewModel_Id");
            CreateIndex("dbo.EventMembers", "EventViewModel_Id");
            AddForeignKey("dbo.EventMembers", "EventViewModel_Id", "dbo.EventViewModels", "Id");
            AddForeignKey("dbo.SiteImages", "EventViewModel_Id", "dbo.EventViewModels", "Id");
        }
    }
}
