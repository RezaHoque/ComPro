namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.FeedModels",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            UserId = c.String(),
            //            UserName = c.String(),
            //            Verb = c.String(),
            //            Action = c.String(),
            //            ActionId = c.String(),
            //            ActionTitle = c.String(),
            //            UrlToAction = c.String(),
            //            ActionDate = c.DateTime(nullable: false),
            //            Description = c.String(),
            //            ImagePath = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PerticipentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        PerticipentId = c.String(),
                        AnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PollingAndSyrvayModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Creation = c.DateTime(nullable: false),
                        CreatorId = c.String(),
                        Status = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        ApprovalDate = c.DateTime(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        ActivityName = c.String(),
                        Question = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QuestionModels");
            DropTable("dbo.PollingAndSyrvayModels");
            DropTable("dbo.PerticipentModels");
            //DropTable("dbo.FeedModels");
            DropTable("dbo.AnswerModels");
        }
    }
}
