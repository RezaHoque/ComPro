namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEventMember : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EventMembers", "MemberID", c => c.String(maxLength: 128));
            CreateIndex("dbo.EventMembers", "MemberID");
            AddForeignKey("dbo.EventMembers", "MemberID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventMembers", "MemberID", "dbo.AspNetUsers");
            DropIndex("dbo.EventMembers", new[] { "MemberID" });
            AlterColumn("dbo.EventMembers", "MemberID", c => c.String());
        }
    }
}
