namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventMembers", "DetailViewModel_Id", "dbo.DetailViewModels");
            DropIndex("dbo.EventMembers", new[] { "DetailViewModel_Id" });
            DropColumn("dbo.EventMembers", "DetailViewModel_Id");
            DropTable("dbo.DetailViewModels");
            DropTable("dbo.EventViewModels");
            DropTable("dbo.MemberViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MemberViewModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventTitel = c.String(),
                        Approval = c.Boolean(nullable: false),
                        CreatorID = c.String(),
                        Activity = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetailViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Place = c.String(),
                        Members = c.Int(nullable: false),
                        Going = c.Int(nullable: false),
                        NotGoing = c.Int(nullable: false),
                        Maybe = c.Int(nullable: false),
                        Seen = c.Int(nullable: false),
                        Creation = c.DateTime(nullable: false),
                        CreatorName = c.String(),
                        ApprovalDate = c.DateTime(nullable: false),
                        CreatorId = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        UserActivity = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EventMembers", "DetailViewModel_Id", c => c.Int());
            CreateIndex("dbo.EventMembers", "DetailViewModel_Id");
            AddForeignKey("dbo.EventMembers", "DetailViewModel_Id", "dbo.DetailViewModels", "Id");
        }
    }
}
