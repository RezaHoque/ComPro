namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class publicComnt : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.CommentModels", "NoticeId", "dbo.NoticeBoards");
            //DropForeignKey("dbo.CommentModels", "Writer_Id", "dbo.AspNetUsers");
            //DropIndex("dbo.CommentModels", new[] { "NoticeId" });
            //DropIndex("dbo.CommentModels", new[] { "Writer_Id" });
            CreateTable(
                "dbo.PublicComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.String(),
                        CommentDateTime = c.DateTime(nullable: false),
                        NoticeId = c.Int(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        CommentUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CommentUserId)
                .ForeignKey("dbo.NoticeBoards", t => t.NoticeId, cascadeDelete: true)
                .Index(t => t.NoticeId)
                .Index(t => t.CommentUserId);
            
           // DropTable("dbo.CommentModels");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.CommentModels",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Text = c.String(),
            //            CommentDateTime = c.DateTime(nullable: false),
            //            NoticeId = c.Int(nullable: false),
            //            IsBlocked = c.Boolean(nullable: false),
            //            Writer_Id = c.String(maxLength: 128),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.PublicComments", "NoticeId", "dbo.NoticeBoards");
            DropForeignKey("dbo.PublicComments", "CommentUserId", "dbo.AspNetUsers");
            DropIndex("dbo.PublicComments", new[] { "CommentUserId" });
            DropIndex("dbo.PublicComments", new[] { "NoticeId" });
            DropTable("dbo.PublicComments");
            //CreateIndex("dbo.CommentModels", "Writer_Id");
            //CreateIndex("dbo.CommentModels", "NoticeId");
            //AddForeignKey("dbo.CommentModels", "Writer_Id", "dbo.AspNetUsers", "Id");
            //AddForeignKey("dbo.CommentModels", "NoticeId", "dbo.NoticeBoards", "Id", cascadeDelete: true);
        }
    }
}
