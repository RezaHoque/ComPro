namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueUrlToNotice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NoticeBoards", "UniqueUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NoticeBoards", "UniqueUrl");
        }
    }
}
