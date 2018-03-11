namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEventPerticipent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventMembers", "EventId", "dbo.EventModels");
            DropForeignKey("dbo.EventPerticepents", "EventId", "dbo.EventModels");
            DropForeignKey("dbo.EventPerticepents", "DetailViewModel_Id", "dbo.DetailViewModels");
            DropIndex("dbo.EventMembers", new[] { "EventId" });
            DropIndex("dbo.EventPerticepents", new[] { "EventId" });
            DropIndex("dbo.EventPerticepents", new[] { "DetailViewModel_Id" });
            AddColumn("dbo.EventMembers", "PerticipetingType", c => c.String());
            AddColumn("dbo.EventMembers", "ResponseDate", c => c.DateTime(nullable: false));
            DropTable("dbo.EventPerticepents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventPerticepents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberID = c.String(),
                        PerticipetingType = c.String(),
                        ResponseDate = c.DateTime(nullable: false),
                        EventId = c.Int(nullable: false),
                        DetailViewModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.EventMembers", "ResponseDate");
            DropColumn("dbo.EventMembers", "PerticipetingType");
            CreateIndex("dbo.EventPerticepents", "DetailViewModel_Id");
            CreateIndex("dbo.EventPerticepents", "EventId");
            CreateIndex("dbo.EventMembers", "EventId");
            AddForeignKey("dbo.EventPerticepents", "DetailViewModel_Id", "dbo.DetailViewModels", "Id");
            AddForeignKey("dbo.EventPerticepents", "EventId", "dbo.EventModels", "EventId", cascadeDelete: true);
            AddForeignKey("dbo.EventMembers", "EventId", "dbo.EventModels", "EventId", cascadeDelete: true);
        }
    }
}
