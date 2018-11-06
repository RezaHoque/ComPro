namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFieldUserInfo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "CurrentStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoes", "CurrentStatus");
        }
    }
}
