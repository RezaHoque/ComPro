namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newevent : DbMigration
    {
        public override void Up()
        {
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
                        UserActivity = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventTitel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MemberViewModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.EventMembers", "DetailViewModel_Id", c => c.Int());
            AddColumn("dbo.EventPerticepents", "DetailViewModel_Id", c => c.Int());
            CreateIndex("dbo.EventMembers", "DetailViewModel_Id");
            CreateIndex("dbo.EventPerticepents", "DetailViewModel_Id");
            AddForeignKey("dbo.EventMembers", "DetailViewModel_Id", "dbo.DetailViewModels", "Id");
            AddForeignKey("dbo.EventPerticepents", "DetailViewModel_Id", "dbo.DetailViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventPerticepents", "DetailViewModel_Id", "dbo.DetailViewModels");
            DropForeignKey("dbo.EventMembers", "DetailViewModel_Id", "dbo.DetailViewModels");
            DropIndex("dbo.EventPerticepents", new[] { "DetailViewModel_Id" });
            DropIndex("dbo.EventMembers", new[] { "DetailViewModel_Id" });
            DropColumn("dbo.EventPerticepents", "DetailViewModel_Id");
            DropColumn("dbo.EventMembers", "DetailViewModel_Id");
            DropTable("dbo.MemberViewModels");
            DropTable("dbo.EventViewModels");
            DropTable("dbo.DetailViewModels");
        }
    }
}
