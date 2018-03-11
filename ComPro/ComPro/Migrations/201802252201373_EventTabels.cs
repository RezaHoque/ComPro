namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventTabels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventModels",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Creation = c.DateTime(nullable: false),
                        CreatorId = c.String(),
                        Place = c.String(),
                        EventStatus = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        ApprovalDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.EventMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberID = c.String(),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventModels", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.EventPerticepents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberID = c.String(),
                        PerticipetingType = c.String(),
                        ResponseDate = c.DateTime(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventModels", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventPerticepents", "EventId", "dbo.EventModels");
            DropForeignKey("dbo.EventMembers", "EventId", "dbo.EventModels");
            DropIndex("dbo.EventPerticepents", new[] { "EventId" });
            DropIndex("dbo.EventMembers", new[] { "EventId" });
            DropTable("dbo.EventPerticepents");
            DropTable("dbo.EventMembers");
            DropTable("dbo.EventModels");
        }
    }
}
