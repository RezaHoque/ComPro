namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Meetings2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meetings_Models", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meetings_Models", "Title");
        }
    }
}
