namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoticePinUp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NoticeBoards", "PinUp", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NoticeBoards", "PinUp");
        }
    }
}
