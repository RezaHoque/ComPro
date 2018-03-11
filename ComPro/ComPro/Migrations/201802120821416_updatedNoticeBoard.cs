namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedNoticeBoard : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NoticeBoards", "CreatorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.NoticeBoards", "CreatorId");
            AddForeignKey("dbo.NoticeBoards", "CreatorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoticeBoards", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.NoticeBoards", new[] { "CreatorId" });
            AlterColumn("dbo.NoticeBoards", "CreatorId", c => c.String());
        }
    }
}
